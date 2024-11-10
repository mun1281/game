using Assets.game.Scripts.Game.State.Buildings;
using Assets.game.Scripts.Game.State.cmd;
using Assets.game.Scripts.Game.State.Root;
using Assets.game.Scripts.GameStateBuildings;

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
            var entityId = _gameState.GetEntityId();
            var newBuildingEntity = new BuildingEntity
            {
                Id = entityId,
                Position = command.Position,
                TypeId = command.BuildingTypeId
            };

            var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);
            _gameState.Buildings.Add(newBuildingEntityProxy);

            return true;
        }
    }
}
