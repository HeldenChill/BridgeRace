
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    using BridgeRace.Core.Brick;
    public class VisionEatBrickSensor : BaseSensor
    {
        [SerializeField]
        Transform visionEatBrickCheck;
        [SerializeField]
        LayerMask layer;
        [SerializeField]
        Vector3 checkRadious;

        private List<EatBrick> eatBricksList = new List<EatBrick>();
        public override void UpdateData()
        {
            Collider[] eatBricks = new Collider[20];
            Physics.OverlapBoxNonAlloc(visionEatBrickCheck.position, checkRadious, eatBricks, Quaternion.identity, layer);
            ColliderCheck(eatBricks);

            Data.VisionEatBricks = eatBricksList;
        }

        private void ColliderCheck(Collider[] eatBricks)
        {
            eatBricksList.Clear();
            for (int i = 0; i < eatBricks.Length; i++)
            {
                if (eatBricks[i] == null)
                    continue;

                EatBrick brick = Cache.GetEatBrick(eatBricks[i]);
                eatBricksList.Add(brick);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (visionEatBrickCheck != null)
            {
                Gizmos.DrawCube(visionEatBrickCheck.position, checkRadious * 2);
            }
        }
    }
}