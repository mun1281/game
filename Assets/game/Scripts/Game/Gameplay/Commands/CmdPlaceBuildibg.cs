using Assets.game.Scripts.Game.State.cmd;
using System.Numerics;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.Commands
{
    public class CmdPlaceBuildibg : ICommand
    {
        public readonly string BuildingTypeId;
        public readonly Vector3Int Position;

        public CmdPlaceBuildibg(string buildingTypeId, Vector3Int position)
        {
            BuildingTypeId = buildingTypeId;
            Position = position;
        }
    }
}
