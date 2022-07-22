
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Data {
    using BridgeRace.Core.Brick;
    public class CharacterData : ScriptableObject
    {
        public Stack<EatBrick> Bricks = new Stack<EatBrick>();
    }
}