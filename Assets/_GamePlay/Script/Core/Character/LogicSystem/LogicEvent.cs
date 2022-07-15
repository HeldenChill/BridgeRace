using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using System;
    public class LogicEvent : ScriptableObject
    {
        public Action<Vector3> SetVelocity;
        public Action<string,Quaternion> SetRotation;
    }
}