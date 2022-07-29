
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.NavigationSystem
{
    using BridgeRace.Core.Brick;
    using BridgeRace.Core.Data;
    public class NavigationParameter : AbstractParameterSystem
    {
        public Transform PlayerTF;
        public Transform SensorTF;
        public BrickColor CharacterType;
        public CharacterData CharacterData;
        public int PlayerInstanceID;

        public bool IsGrounded = false;
        public bool IsHaveGround = false;
        public List<EatBrick> EatBricks;
        public List<Vector3> EnemyPositions;
        public int BrickCount;
    }
}