using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Data
{
    [CreateAssetMenu(fileName = "levelData", menuName = "Bridge Race/Data/Level")]
    public class LevelData : ScriptableObject
    {
        public Vector2 RoomSize1;
        public Vector2 RoomSize2;
        public Vector2 RoomSize3;
    
    }
}