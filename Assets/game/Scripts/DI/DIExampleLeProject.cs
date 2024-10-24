using System.Collections;
using UnityEngine;

namespace DI
{
    // Сервис уровня проекта.
    public class MyAwesomeProjectService { }

    // Сервис уровня сцены.
    public class MySceneService
    {
        private MyAwesomeProjectService _myAwesomeProjectService;

        public MySceneService(MyAwesomeProjectService myAwesomeProjectService)
        {
            _myAwesomeProjectService = myAwesomeProjectService;
        }
    }

    // Фабрика.
    public class MyAwesomeFactory
    {
        public MyAwesomeObject CreateInstance(string id, int par1)
        {
            return new MyAwesomeObject(id, par1);
        }
    }

    // Класс объект для фабрики.
    public class MyAwesomeObject
    {
        private readonly string _id;
        private readonly int _par1;

        public MyAwesomeObject(string id, int par1)
        {
            _id = id;
            _par1 = par1;
        }
    }

    public class DIExampleLeProject : MonoBehaviour
    {
        private void Awake()
        {
            var projectContainer = new DIContainer();
            projectContainer.RegisterSingleton(_ => new MyAwesomeProjectService());

            var sceneRoot = FindObjectOfType<DIExampleScene>();
            sceneRoot.Init(projectContainer);
        }
    }
}