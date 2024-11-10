using Assets.game.Scripts.Game.State.Buildings;
using Assets.game.Scripts.GameStateBuildings;
using ObservableCollections;
using System.Linq;
using R3;

namespace Assets.game.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;

        public ObservableList<BuildingEntityProxy> Buildings { get; } = new();

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;

            // Для каждого оригинального строения делаем прокси и ложем в него это строение, а после прокси кладем в Buildings лист.
            gameState.Buildings.ForEach(buildingOrigin => Buildings.Add(new BuildingEntityProxy(buildingOrigin)));

            // Подписка на добовление.
            Buildings.ObserveAdd().Subscribe(e =>
            {
                var addedBuildingEntity = e.Value;
                gameState.Buildings.Add(addedBuildingEntity.Origin);
            });

            // Подписка на удаление.
            Buildings.ObserveRemove().Subscribe(e =>
            {
                var removedBuildingEntityProxy = e.Value;
                var removedBuildingEntity = gameState.Buildings.FirstOrDefault(b => b.Id == removedBuildingEntityProxy.Id);
                gameState.Buildings.Remove(removedBuildingEntity);
            });
        }

        public int GetEntityId()
        {// Индивидуальный ID объекта.
            return _gameState.GlobalEntityId++;
        }
    }
}
