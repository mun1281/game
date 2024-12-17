using Assets.game.Scripts.GameStateBuildings;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Assets.game.Scripts.Game.State.Maps
{
    [Serializable]
    public class MapState
    {
        public int Id;
        public List<BuildingEntity> Buildings;
    }
}
