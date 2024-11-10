using System.Threading.Tasks;

namespace Assets.game.Scripts.Game.Settings
{
    public interface ISettingsProvider
    {
        GameSettings GameSettings { get; }
        ApplicationSettings ApplicationSettings { get; }

        Task<GameSettings> LoadGameSettings();
    }
}
