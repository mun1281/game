using Assets.game.Scripts.Game.Gameplay.Commands;
using Assets.game.Scripts.Game.Gameplay.View;
using Assets.game.Scripts.Game.State.Buildings;
using Assets.game.Scripts.Game.State.cmd;
using ObservableCollections;
using UnityEngine;
using R3;
using System.Collections.Generic;
using Assets.game.Scripts.Game.Settings.Gameplay.Buildings;

namespace Assets.game.Scripts.Game.Gameplay.Services
{
    public class BuildingsService
    {
        private readonly ICommandProcessor _cmd;

        private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();
        public readonly ObservableList<BuildingViewModel> _allBuildings = new();
        private readonly Dictionary<string, BuildingSettings> _buildingSettingsMap = new();

        public IObservableCollection<BuildingViewModel> _AllBuildings => _allBuildings;//Хранит список вью моделей.

        public BuildingsService(
            IObservableCollection<BuildingEntityProxy> buildings,//прилитает список состояний.
            BuildingsSettings buildingsSettings,
            ICommandProcessor cmd) 
        {
            _cmd = cmd;

            foreach (var buildingSettings in buildingsSettings.AllBuildings)
            {
                _buildingSettingsMap[buildingSettings.TypeId] = buildingSettings;// Кэшируем список настроек зданий.
            }

            foreach (var buildingEntity in buildings)
            {// Создаем на каждое актуальное строение вью модель.
                CreateBuildingViewModel(buildingEntity);
            }

            buildings.ObserveAdd().Subscribe(e =>
            {// Подписываемся на добовление в колекцию. таким образм вью модель создается если в колекцию добавилось здание.
                CreateBuildingViewModel(e.Value);
            });

            buildings.ObserveRemove().Subscribe(e =>
            {
                RemoveBuildingViewModel(e.Value);
            });
        }

        public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
        {
            var command = new CmdPlaceBuildibg(buildingTypeId, position);
            var result = _cmd.Process(command);

            return result; 
        }

        public bool MoveBuilding(int builingEntityId, Vector3Int newPosition)
        {
            return true;
        }

        public bool DeleteBuilding(int builingEntityId)
        {
            return true;
        }

        private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
        {
            var buildingSettings = _buildingSettingsMap[buildingEntity.TypeId];
            var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);

            _allBuildings.Add(buildingViewModel);
            _buildingsMap[buildingEntity.Id] = buildingViewModel;
        }

        private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
        {
            if (_buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
            {
                _allBuildings.Remove(buildingViewModel);
                _buildingsMap.Remove(buildingEntity.Id);
            }
        }
    }
}
