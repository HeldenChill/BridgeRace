using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.PhysicSystem
{
    public class PhysicParameter : AbstractParameterSystem
    {
        public readonly float GRAVITY = -9.81f;
        public float GravityParameter = 1;

        public float JumpHeight = 2f;
    }
}