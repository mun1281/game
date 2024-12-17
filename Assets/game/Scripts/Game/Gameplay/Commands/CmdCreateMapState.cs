using Assets.game.Scripts.Game.State.cmd;

namespace Assets.game.Scripts.Game.Gameplay.Commands
{
    public class CmdCreateMapState : ICommand
    {
        public readonly int MapId;

        public CmdCreateMapState(int mapId)
        {
            MapId = mapId;
        }
    }
}
