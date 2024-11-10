using Assets.game.Scripts.Game.Gameplay.Commands;
using Assets.game.Scripts.Game.Gameplay.Root.View;
using Assets.game.Scripts.Game.Gameplay.Services;
using Assets.game.Scripts.Game.MainMenu.Root;
using Assets.game.Scripts.Game.State;
using Assets.game.Scripts.Game.State.cmd;
using BaCon;
using game.Scripts;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Root
{// Точка входа в сцену геймплея.
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldRootBinder;

        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
        {
            GameplayRegistrations.Registar(gameplayContainer, enterParams);// регестрируем команды, настройки и сервесы.
            var gameplayViewModelsContainer = new DIContainer(gameplayContainer);// Создание контейнера для View моделей (Может брать данные из: главного DI и DI сцены).
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);
            

            /// тест
            var gameStateProvider = gameplayContainer.Resolve<IGameStateProvider>();
            gameStateProvider.GameState.Buildings.ObserveAdd().Subscribe(e =>
            {
                var building = e.Value;
                Debug.Log("Building placed. Type id: " +
                    building.TypeId
                    + " Id: " + building.Id
                    + ", Position: " +
                    building.Position.Value);
            });
            ///

            // В будущем будем передовать этот сервис во вью а он будет реагировать что создать.
            var buildingsService = gameplayContainer.Resolve<BuildingsService>();

            buildingsService.PlaceBuilding("dummy", GetRandomPosition());
            buildingsService.PlaceBuilding("dummy", GetRandomPosition());
            buildingsService.PlaceBuilding("dummy", GetRandomPosition());

            // Тест.
            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();

            _worldRootBinder.Bind(gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>());

            // Создаём UI сцены геймплея.
            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            // Создаём Subject для сигнализации о выходе из сцены.
            var exitSceneSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubj);// Передаём его кнопке.

            Debug.Log($"GAMEPLAY ENTRY POINT: save file name = {enterParams.SaveFileName}, level to load = {enterParams.LevelNumber}");

            // Создаём объект параметров для выхода в главное меню, используя переданный Subject.
            var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);

            //Когда будет сработано событие он передаст exitParams.
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

            return exitToMainMenuSceneSignal;
        }

        // Удалиться!
        private Vector3Int GetRandomPosition()
        {
            var rX = Random.Range(-10, 10);
            var rY = Random.Range(-10, 10);
            var rPosition = new Vector3Int(rX, rY, 0);

            return rPosition;
        }
    }
}