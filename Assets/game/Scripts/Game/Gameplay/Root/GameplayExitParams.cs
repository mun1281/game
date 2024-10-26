using Assets.game.Scripts.Game.MainMenu.Root;
using UnityEditor;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Root
{// Выходные данные.
    public class GameplayExitParams
    {
        public MainMenuEnterParams MainMenuEnterParams { get; }

        public GameplayExitParams(MainMenuEnterParams mainMenuEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
        }
    }
}