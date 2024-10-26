using Assets.game.Scripts.Game.Gameplay.Services;

namespace Assets.game.Scripts.Game.Gameplay.Root.View
{
    public class UIGameplayRootViewModel
    {
        private readonly SomeGameplayService _someGameplayService;
        public UIGameplayRootViewModel(SomeGameplayService someGameplayService)
        {
            _someGameplayService = someGameplayService;
        }
    }
}
