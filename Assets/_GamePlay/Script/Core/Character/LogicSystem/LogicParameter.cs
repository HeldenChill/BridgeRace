using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.LogicSystem
{
    using BridgeRace.Core.Brick;
    public class LogicParameter : AbstractParameterSystem
    {
        public BrickColor CharacterType;
        public Transform ContainBrick;

        public float Speed = 4f;
        public Vector3 Velocity;
        public Vector3 MoveDirection;
        public bool IsGrounded = false;
        public bool Jump = false;
        public float JumpVelocity = 10f;
        public BridgeBrick BridgeBrick;
        public List<EatBrick> EatBricks;
    }
}