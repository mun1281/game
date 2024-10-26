using Assets.game.Scripts.Game.Gameplay.Root;
using Assets.game.Scripts.Game.MainMenu.Root;
using game.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using BaCon;
using Assets.game.Scripts.Game.GameRoot.Services;

namespace game.Scripts
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachedSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            // Системные настройки:
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
 
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            // Создаем карутину.
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad( _coroutines.gameObject);

            // Создаем объект экран загрузки.
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            // Регистрация DI.
            _rootContainer.RegisterInstance(_uiRoot);
            _rootContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();
        }

        private void RunGame()
        {
#if UNITY_EDITOR // Не будет в билде.
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                var enterParams = new GameplayEnterParams("ddd.save", 1);
                _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            }

            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
        {
            _uiRoot.ShowLoadingScreen();// Включаем экран загрузки.
            _cachedSceneContainer?.Dispose();// Очищаем контейнер перед созданием нового.

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(1);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();// Поиск по типу.

            // Контейнер сцены.
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            // Подписались на событие.
            sceneEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParams => 
            {// При сробатывание вызывает карутину и передаёт в неё данные.
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
            });

            _uiRoot.HideLoadingScreen();// Выключаем экран загрузки.
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();// Включаем экран загрузки.
            _cachedSceneContainer?.Dispose();// Очищаем контейнер перед созданием нового.

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(1);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();// Поиск по типу.

            // Контейнер сцены.
            var mainMenuContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
            {
                // Имя сцены для перехода.
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Scenes.GAMEPLAY)
                {// Передача данных через специальный абстрактный метод.
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });

            _uiRoot.HideLoadingScreen();// Выключаем экран загрузки.
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}