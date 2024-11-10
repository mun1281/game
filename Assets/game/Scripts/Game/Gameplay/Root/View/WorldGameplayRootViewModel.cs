using Assets.game.Scripts.Game.Gameplay.Services;
using Assets.game.Scripts.Game.Gameplay.View;
using ObservableCollections;

namespace Assets.game.Scripts.Game.Gameplay.Root.View
{
    public class WorldGameplayRootViewModel
    {
        public readonly IObservableCollection<BuildingViewModel> AllBuildings;

        public WorldGameplayRootViewModel(BuildingsService buildingsService)
        {
            AllBuildings = buildingsService._AllBuildings;
        }
    }
}
