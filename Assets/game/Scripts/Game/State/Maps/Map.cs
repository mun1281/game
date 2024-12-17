using Assets.game.Scripts.Game.State.Buildings;
using R3;
using ObservableCollections;
using System.Linq;

namespace Assets.game.Scripts.Game.State.Maps
{
    public class Map
    {
        public int Id => Origin.Id;

        public ObservableList<BuildingEntityProxy> Buildings { get; } = new();

        public MapState Origin { get; }

        public Map(MapState mapState)
        {
            Origin = mapState;

            // Для каждого оригинального строения делаем прокси и ложем в него это строение, а после прокси кладем в Buildings лист.
            mapState.Buildings.ForEach(buildingOrigin => Buildings.Add(new BuildingEntityProxy(buildingOrigin)));

            // Подписка на добовление.
            Buildings.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingEntity = e.Value;
                mapState.Buildings.Add(addedBuildingEntity.Origin);
            });

            // Подписка на удаление.
            Buildings.ObserveRemove().Subscribe(e =>
            {
                var removedBuildingEntityProxy = e.Value;
                var removedBuildingEntity = mapState.Buildings.FirstOrDefault(b => b.Id == removedBuildingEntityProxy.Id);
                mapState.Buildings.Remove(removedBuildingEntity);
            });
        }
    }
}
