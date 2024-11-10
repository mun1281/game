namespace Assets.game.Scripts.Game.State.cmd
{
    public interface ICommandProcessor
    {
        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
        public bool Process<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
