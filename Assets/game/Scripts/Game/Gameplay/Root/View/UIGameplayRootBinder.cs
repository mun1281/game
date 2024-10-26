using UnityEngine;
using System;
using R3;

namespace Assets.game.Scripts.Game.Gameplay.Root
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        //
        private Subject<Unit> _exitSceneSignalSubj;

        public void HandleGoToMainMenuButtonClick()
        {//
            _exitSceneSignalSubj?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalSubj)
        {
            _exitSceneSignalSubj = exitSceneSignalSubj;
        }
    }
}