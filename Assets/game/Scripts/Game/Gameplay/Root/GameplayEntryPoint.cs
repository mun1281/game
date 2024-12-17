using Assets.game.Scripts.Game.Gameplay.Root.View;
using Assets.game.Scripts.Game.MainMenu.Root;
using BaCon;
using game.Scripts;
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

            Debug.Log($"GAMEPLAY ENTRY POINT: level to load = {enterParams.MapId}");

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