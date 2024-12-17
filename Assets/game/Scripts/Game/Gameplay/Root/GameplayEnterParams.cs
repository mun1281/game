using game.Scripts;

namespace Assets.game.Scripts.Game.Gameplay.Root
{// Входные данные.
    public class GameplayEnterParams : SceneEnterParams
    {
        public int MapId { get; }

        public GameplayEnterParams(int mapId) : base(Scenes.GAMEPLAY)
        {
            MapId = mapId;
        }
    }
}