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

        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
        {
            GameplayRegistrations.Registar(gameplayContainer, enterParams);
            var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);
            // Регестрация завершина. Теперь можно достовать из этих контейнеров.

            // Тест.
            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
            gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>();

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
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);//Когда будет сработано событие он передаст exitParams

            return exitToMainMenuSceneSignal;
        }
    }
}