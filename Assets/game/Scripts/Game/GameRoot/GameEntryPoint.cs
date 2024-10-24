using Assets.game.Scripts.Game.Gameplay.Root;
using game.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.Scripts
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRoot;

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

            // Создание контейнера и подгрузка настроек игры.
        }

        private void RunGame()
        {
#if UNITY_EDITOR // Не будет в билде.
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay());
                return;
            }

            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadAndStartGameplay());
        }

        private IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();// Включаем экран загрузки.

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(2);

            // Контейнер

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();// Поиск по типу.
            sceneEntryPoint.Run();

            _uiRoot.HideLoadingScreen();// Выключаем экран загрузки.
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}