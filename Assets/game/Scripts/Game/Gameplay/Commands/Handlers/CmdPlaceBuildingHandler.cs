using Assets.game.Scripts.Game.State.Buildings;
using Assets.game.Scripts.Game.State.cmd;
using Assets.game.Scripts.Game.State.Root;
using Assets.game.Scripts.GameStateBuildings;
using System.Linq;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Commands
{
    public class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuildibg>
    {
        private readonly GameStateProxy _gameState;

        public CmdPlaceBuildingHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdPlaceBuildibg command)
        {
            var currentMap = _gameState.Maps.FirstOrDefault(m => m.Id == _gameState.CurrentMapId.CurrentValue);
            if (currentMap == null)
            {
                Debug.LogError($"Couldn't find MapState for id: {_gameState.CurrentMapId.CurrentValue}");
                return false;
            }

            var entityId = _gameState.CreateEntityId();
            var newBuildingEntity = new BuildingEntity
            {
                Id = entityId,
                Position = command.Position,
                TypeId = command.BuildingTypeId
            };

            var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);
            
            currentMap.Buildings.Add(newBuildingEntityProxy);

            return true;
        }
    }
}
