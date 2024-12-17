using Assets.game.Scripts.Game.Gameplay.Services;
using BaCon;

namespace Assets.game.Scripts.Game.Gameplay.Root.View
{
    public static class GameplayViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
            container.RegisterFactory(c => new WorldGameplayRootViewModel(c.Resolve<BuildingsService>(), c.Resolve<ResourcesService>())).AsSingle();
        }
    }
}
