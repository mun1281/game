using game.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.game.Scripts.Game.MainMenu.Root
{
    public class MainMenuExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public MainMenuExitParams(SceneEnterParams targetSceneEnterParams) 
        {  
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}