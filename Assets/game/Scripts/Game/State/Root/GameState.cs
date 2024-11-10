using Assets.game.Scripts.GameStateBuildings;
using System;
using System.Collections.Generic;

namespace Assets.game.Scripts.Game.State.Root
{
    [Serializable]
    public class GameState
    {
        public int GlobalEntityId;
        public List<BuildingEntity> Buildings;
    }
}
