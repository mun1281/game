using Assets.game.Scripts.GameStateBuildings;
using R3;
using System.Numerics;
using UnityEngine;

namespace Assets.game.Scripts.Game.State.Buildings
{
    public class BuildingEntityProxy
    {
        public int Id { get; }
        public string TypeId { get; }

        public BuildingEntity Origin { get; }
        public ReactiveProperty<Vector3Int> Position { get; }
        public ReactiveProperty<int> Level { get; }

        public BuildingEntityProxy(BuildingEntity buildingEntity)
        {
            Origin = buildingEntity;
            Id = buildingEntity.Id;
            TypeId = buildingEntity.TypeId;
            // Инициализируем реактивные свойства с начальными значениями из сущности buildingEntity.
            Position = new ReactiveProperty<Vector3Int>(buildingEntity.Position);
            Level = new ReactiveProperty<int>(buildingEntity.Level);

            // Подписываемся на изменения позиции и уровня.
            // Skip(1) пропускает начальное значение, чтобы избежать лишнего обновления при инициализации.
            Position.Skip(1).Subscribe(value => buildingEntity.Position = value);
            Level.Skip(1).Subscribe(value => buildingEntity.Level = value);
        }
    }
}
