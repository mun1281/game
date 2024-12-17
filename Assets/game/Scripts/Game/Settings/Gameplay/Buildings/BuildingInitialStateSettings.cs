using System;
using UnityEngine;

namespace Assets.game.Scripts.Game.Settings.Gameplay.Buildings
{
    [Serializable]
    public class BuildingInitialStateSettings
    {
        public string TypeId;
        public int Level;
        public Vector3Int Position;
    }
}
