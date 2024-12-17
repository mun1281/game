
using Assets.game.Scripts.Game.State.cmd;
using Assets.game.Scripts.Game.State.GameResources;

namespace Assets.game.Scripts.Game.Gameplay.Commands
{
    public class CmdResourcesAdd : ICommand
    {
        public readonly ResourceType ResourceType;
        public readonly int Amount;

        public CmdResourcesAdd(ResourceType resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}
