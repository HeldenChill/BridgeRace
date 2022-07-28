using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Manager
{
    using Utilitys;
    using BridgeRace.Core;
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        private Level currentLevel;
        public Level CurrentLevel => currentLevel;
        public void SetPlayers(List<GameObject> players)
        {
            CurrentLevel.Initialize(players);
        }
    }
}