using ObservableCollections;
using System.Linq;
using R3;
using Assets.game.Scripts.Game.State.Maps;
using Assets.game.Scripts.Game.State.GameResources;

namespace Assets.game.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public ReactiveProperty<int> CurrentMapId = new();

        public ObservableList<Map> Maps { get; } = new();
        public ObservableList<Resource> Resources { get; } = new();

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;

            InitMaps(gameState);
            InitResources(gameState);

            CurrentMapId.Subscribe(newValue => { gameState.CurrentMapId = newValue; });
        }

        public int CreateEntityId()
        {// Индивидуальный ID объекта.
            return _gameState.CreateEntityId();
        }

        private void InitMaps(GameState gameState)
        {
            // Для каждого оригинального строения делаем прокси и ложем в него это строение, а после прокси кладем в Buildings лист.
            gameState.Maps.ForEach(mapOrigin => Maps.Add(new Map(mapOrigin)));

            // Подписка на добовление.
            Maps.ObserveAdd().Subscribe(e =>
            {
                var addedMap = e.Value;
                gameState.Maps.Add(addedMap.Origin);
            });

            // Подписка на удаление.
            Maps.ObserveRemove().Subscribe(e =>
            {
                var removedMap = e.Value;
                var removedMapState = gameState.Maps.FirstOrDefault(b => b.Id == removedMap.Id);
                gameState.Maps.Remove(removedMapState);
            });
        }

        private void InitResources(GameState gameState)
        {
            // Для каждого оригинального строения делаем прокси и ложем в него это строение, а после прокси кладем в Buildings лист.
            gameState.Resources.ForEach(resourceData => Resources.Add(new Resource(resourceData)));

            // Подписка на добовление.
            Resources.ObserveAdd().Subscribe(e =>
            {
                var addedResource = e.Value;
                gameState.Resources.Add(addedResource.Origin);
            });

            // Подписка на удаление.
            Resources.ObserveRemove().Subscribe(e =>
            {
                var removedResource = e.Value;
                var removedResourceData = gameState.Resources.FirstOrDefault(b => b.ResourceType == removedResource.ResourceType);
                gameState.Resources.Remove(removedResourceData);
            });
        }
    }
}
