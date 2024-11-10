using Assets.game.Scripts.Game.Gameplay.Root;
using Assets.game.Scripts.Game.Gameplay.Root.View;
using Assets.game.Scripts.Game.MainMenu.Root.View;
using Assets.game.Scripts.Game.MainMenu.Services;
using BaCon;
using game.Scripts;
using R3;
using System;
using UnityEngine;

namespace Assets.game.Scripts.Game.MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        public Observable<MainMenuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)
        {
            MainMenuRegistrations.Registar(mainMenuContainer, enterParams);
            var mainMenuViewModelsContainer = new DIContainer(mainMenuContainer);
            MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

            // Тест.
            mainMenuViewModelsContainer.Resolve<UIMainMenuRootViewModel>();
            mainMenuContainer.Resolve<SomeMainMenuService>();

            var uiRoot = mainMenuContainer.Resolve<UIRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSignalSubj);

            Debug.Log($"Results: {enterParams?.Result}");

            var saveFileName = "ololo.save";
            var levelNumber = UnityEngine.Random.Range(0, 300);
            var gameplayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitToGameplaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

            return exitToGameplaySceneSignal;
        }
    }
}