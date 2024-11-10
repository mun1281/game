using Assets.game.Scripts.Game.Gameplay.Commands;
using Assets.game.Scripts.Game.Gameplay.Services;
using Assets.game.Scripts.Game.Settings;
using Assets.game.Scripts.Game.State;
using Assets.game.Scripts.Game.State.cmd;
using Assets.game.Scripts.Game.State.Root;
using BaCon;

namespace Assets.game.Scripts.Game.Gameplay.Root
{
    public static class GameplayRegistrations
    {
        public static void Registar(DIContainer container, GameplayEnterParams gameplayEnterParams)
        {
            // получаем состояния.
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;

            var settingsProvider = container.Resolve<ISettingsProvider>();
            var gameSettings = settingsProvider.GameSettings;

            // Регестрируем cmd.
            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
            container.RegisterInstance<ICommandProcessor>(cmd);

            container.RegisterFactory(_ => new BuildingsService(gameState.Buildings, gameSettings.BuildingsSettings, cmd)).AsSingle();
        }
    }
}