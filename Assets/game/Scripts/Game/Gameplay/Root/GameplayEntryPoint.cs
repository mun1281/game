using System.Collections;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _sceneRootBinder;

        public void Run()
        {
            Debug.Log("Gamplay scene loaded");
        }
    }
}