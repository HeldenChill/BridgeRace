using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.LogicSystem
{
    using BridgeRace.Core.Brick;
    public class LogicParameter : AbstractParameterSystem
    {
        public BrickColor CharacterType;
        public Transform SensorTF;
        public Transform ContainBrick;
        public int PlayerInstanceID;

        public float Speed = 4f;
        public float TimeFall = 3f;

        public Vector3 Velocity;
        public Vector3 MoveDirection;

        public bool IsHaveGround = false;
        public bool IsGrounded = false;
        public bool Jump = false;
        public float JumpVelocity = 10f;

        public BridgeBrick BridgeBrick;
        public List<EatBrick> EatBricks;
        public List<AbstractCharacter> Characters;

        public bool IsExitRoom = false;

        
    }
}