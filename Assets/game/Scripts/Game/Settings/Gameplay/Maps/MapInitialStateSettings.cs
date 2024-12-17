using Assets.game.Scripts.Game.Settings.Gameplay.Buildings;
using System;
using System.Collections.Generic;

namespace Assets.game.Scripts.Game.Settings.Gameplay.Maps
{
    [Serializable]
    public class MapInitialStateSettings
    {
        public List<BuildingInitialStateSettings> Buildings;
    }
}
