﻿using Assets.game.Scripts.Game.Settings.Gameplay.Buildings;
using UnityEngine;

namespace Assets.game.Scripts.Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/New Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public BuildingsSettings BuildingsSettings;
    }
}
