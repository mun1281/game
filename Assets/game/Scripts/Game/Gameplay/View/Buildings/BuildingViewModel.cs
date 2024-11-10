using Assets.game.Scripts.Game.Gameplay.Services;
using Assets.game.Scripts.Game.Settings.Gameplay.Buildings;
using Assets.game.Scripts.Game.State.Buildings;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.game.Scripts.Game.Gameplay.View
{
    public class BuildingViewModel
    {
        private readonly BuildingEntityProxy _buildingEntity;
        private readonly BuildingSettings _buildingSettings;
        private readonly BuildingsService _buildingsService;

        private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap = new();

        public readonly int BuildingEntityId;

        public ReadOnlyReactiveProperty<Vector3Int> Position {  get; }
        public readonly string TypeId;

        public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingSettings buildingSettings, BuildingsService buildingsService)
        {
            TypeId = buildingSettings.TitleLID;
            BuildingEntityId = buildingEntity.Id;

            _buildingEntity = buildingEntity;
            _buildingSettings = buildingSettings;
            _buildingsService = buildingsService;

            foreach (var buildingLevelSettings in buildingSettings.LevelSettings)
            {
                _levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
            }

            Position = buildingEntity.Position;
        }

        public BuildingLevelSettings GetLevelSettings(int level)
        {
            return _levelSettingsMap[level];
        }
    }
}
