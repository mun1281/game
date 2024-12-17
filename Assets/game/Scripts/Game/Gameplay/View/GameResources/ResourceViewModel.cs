using Assets.game.Scripts.Game.State.GameResources;
using R3;

namespace Assets.game.Scripts.Game.Gameplay.View.GameResources
{
    public class ResourceViewModel
    {
        public readonly ResourceType ResourceType;
        public readonly ReadOnlyReactiveProperty<int> Amaint;

        public ResourceViewModel(Resource resource)
        {
            ResourceType = resource.ResourceType;
            Amaint = resource.Amount;
        }
    }
}
