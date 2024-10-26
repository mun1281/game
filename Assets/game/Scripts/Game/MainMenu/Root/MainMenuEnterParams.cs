using UnityEditor;
using UnityEngine;

namespace Assets.game.Scripts.Game.MainMenu.Root
{
    public class MainMenuEnterParams
    {
        public string Result { get; }

        public MainMenuEnterParams(string result)
        {
            Result = result;
        }
    }
}