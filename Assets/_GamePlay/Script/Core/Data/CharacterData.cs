using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Data
{
    public class CharacterData : ScriptableObject
    {
        public Stack Bricks = new Stack();
        public float Score = 0;
    }
}