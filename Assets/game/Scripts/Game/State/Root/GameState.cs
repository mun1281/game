using Assets.game.Scripts.Game.State.GameResources;
using Assets.game.Scripts.Game.State.Maps;
using Assets.game.Scripts.GameStateBuildings;
using System;
using System.Collections.Generic;

namespace Assets.game.Scripts.Game.State.Root
{
    [Serializable]
    public class GameState
    {
        public int GlobalEntityId;
        public int CurrentMapId;
        public List<MapState> Maps;
        public List<ResourceData> Resources;

        public int CreateEntityId()
        {
            return GlobalEntityId++;
        }
    }
}
