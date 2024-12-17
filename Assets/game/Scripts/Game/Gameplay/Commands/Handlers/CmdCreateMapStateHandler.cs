using Assets.game.Scripts.Game.Settings;
using Assets.game.Scripts.Game.State.cmd;
using Assets.game.Scripts.Game.State.Maps;
using Assets.game.Scripts.Game.State.Root;
using Assets.game.Scripts.GameStateBuildings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Commands
{
    public class CmdCreateMapStateHandler : ICommandHandler<CmdCreateMapState>
    {
        private readonly GameStateProxy _gameState;
        private readonly GameSettings _gameSettings;

        public CmdCreateMapStateHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            _gameState = gameState;
            _gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateMapState command)
        {
            var isMapAlreadyExisted = _gameState.Maps.Any(m => m.Id == command.MapId);

            if (isMapAlreadyExisted)
            {
                Debug.LogError($"Map with Id = {command.MapId} already exists");
                return false;
            }

            var newMapSettings = _gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);
            var newMapInitialStateSettings = newMapSettings.InitialStateSettings;

            var initialBuildings = new List<BuildingEntity>();
            foreach (var buildingSettings in newMapInitialStateSettings.Buildings)
            {
                var initialBuilding = new BuildingEntity
                {
                    Id = _gameState.CreateEntityId(),
                    TypeId = buildingSettings.TypeId,
                    Position = buildingSettings.Position,
                    Level = buildingSettings.Level
                };

                initialBuildings.Add(initialBuilding);
            }

            var newMapState = new MapState
            {
                Id = command.MapId,
                Buildings = initialBuildings
            };

            var newMapStateProxy = new Map(newMapState);

            _gameState.Maps.Add(newMapStateProxy);

            return true;
        }
    }
}
