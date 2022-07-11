using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.LogicSystem
{
    public class LogicParameter : ScriptableObject
    {
        public readonly float GRAVITY = -9.81f;

        public float Speed = 4f;
        public Vector3 Velocity;
        public Vector3 MoveDirection;
        public bool IsGrounded = false;
        public bool Jump = false;
        public float JumpVelocity = 70f;   
    }
}