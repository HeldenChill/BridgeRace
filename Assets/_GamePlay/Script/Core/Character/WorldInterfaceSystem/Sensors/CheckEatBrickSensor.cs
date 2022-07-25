using BridgeRace.Core.Brick;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckEatBrickSensor : BaseSensor
    {
        [SerializeField]
        private Transform eatBrickCheck;
        [SerializeField]
        private Vector3 checkRadious;
        [SerializeField]
        private LayerMask layer;

        private List<EatBrick> eatBricksList = new List<EatBrick>();
        private Queue<EatBrick> oldEatBrick = new Queue<EatBrick>();
        private Collider[] eatBricksTemp = new Collider[4];
        public override void UpdateData()
        {

            //Collider[] eatBricks = Physics.OverlapBox(EatBrickCheck.position, checkRadious, Quaternion.identity,layer);
            Array.Clear(eatBricksTemp, 0, eatBricksTemp.Length);
            Physics.OverlapBoxNonAlloc(eatBrickCheck.position, checkRadious, eatBricksTemp, Quaternion.identity, layer);
            ColliderCheck(eatBricksTemp);
            Data.EatBricks = eatBricksList;

        }

        private void ColliderCheck(Collider[] eatBricks)
        {
            eatBricksList.Clear();
            int oldCount = oldEatBrick.Count;
            for (int i = 0; i < eatBricks.Length; i++)
            {
                if (eatBricks[i] == null)
                    continue;

                EatBrick brick = Cache.GetEatBrick(eatBricks[i]);

                if (!oldEatBrick.Contains(brick))
                {
                    eatBricksList.Add(brick);
                }
                oldEatBrick.Enqueue(brick);

            }

            for (int i = 0; i < oldCount; i++)
            {
                oldEatBrick.Dequeue();
            }
        }

        //NOTE: When check collide by math => collide may be duplicate (Dont know why)

        private void OnDrawGizmos()
        {
            if (eatBrickCheck != null)
            {
                Gizmos.DrawCube(eatBrickCheck.position, checkRadious * 2);
            }
        }
    }
}