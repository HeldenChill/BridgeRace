using BridgeRace.Core.Brick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckEatBrickSensor : BaseSensor
    {
        [SerializeField]
        private Transform EatBrickCheck;
        [SerializeField]
        private Vector3 checkRadious;
        [SerializeField]
        private LayerMask layer;

        private List<EatBrick> eatBricksList = new List<EatBrick>();
        private List<EatBrick> oldEatBrick = new List<EatBrick>();
        public override void UpdateData()
        {
            Collider[] eatBricks = Physics.OverlapBox(EatBrickCheck.position, checkRadious, Quaternion.identity,layer);
            eatBricksList.Clear();
            for(int i = 0; i < eatBricks.Length; i++)
            {
                EatBrick brick = Cache.GetEatBrick(eatBricks[i]);
                if (!oldEatBrick.Contains(brick))
                {
                    eatBricksList.Add(brick);
                    oldEatBrick.Add(brick);
                }
                
            }
            Data.EatBricks = eatBricksList;
        }

        private void OnDrawGizmos()
        {
            if (EatBrickCheck != null)
            {
                Gizmos.DrawCube(EatBrickCheck.position, checkRadious * 2);
            }
        }
    }
}