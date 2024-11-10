using NUnit.Framework;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.game.Scripts.Game.Settings.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "BuildingSettings", menuName = "Game Settings/Buildings/New Building Settings")]
    public class BuildingSettings : ScriptableObject
    {
        public string TypeId;
        public string TitleLID;// Название
        public string DescriptionLID;// Описание
        public List<BuildingLevelSettings> LevelSettings;
    }
}
