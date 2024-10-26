using game.Scripts;

namespace Assets.game.Scripts.Game.Gameplay.Root
{// Входные данные.
    public class GameplayEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; }

        public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}