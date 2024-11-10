using Assets.game.Scripts.Game.State.Entities;
using System;
using UnityEngine;

namespace Assets.game.Scripts.GameStateBuildings
{
    [Serializable]
    public class BuildingEntity : Entity
    {
        public string TypeId;
        public Vector3Int Position;
        public int Level;
    }
}
