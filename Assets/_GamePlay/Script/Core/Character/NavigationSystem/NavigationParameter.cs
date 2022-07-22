using BridgeRace.Core.Brick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.NavigationSystem
{
    public class NavigationParameter : AbstractParameterSystem
    {
        public Transform Player;

        public bool IsGrounded = false;
        public List<EatBrick> EatBricks;
        public List<Vector3> EnemyPositions;

    }
}