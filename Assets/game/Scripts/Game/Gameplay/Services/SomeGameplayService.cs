using Assets.game.Scripts.Game.GameRoot.Services;
using System;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Services
{
    public class SomeGameplayService : IDisposable
    {
        // Сервис общего DI уровня.
        private readonly SomeCommonService _someCommonService;

        public SomeGameplayService(SomeCommonService someCommonService)
        {
            _someCommonService = someCommonService;
            Debug.Log(GetType().Name + " has been created");
        }
        public void Dispose()
        {
            Debug.Log("Подчистить все подписки");
        }
    }
}
