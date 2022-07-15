using BridgeRace.Core.Brick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckBridgeBrickSensor : BaseSensor
    {
        [SerializeField]
        Transform bridgeBrickCheck;
        [SerializeField]
        float maxDistance = 0.3f;
        [SerializeField]
        LayerMask layer;
        Ray ray;

        int oldColliderInstanceId;

        public override void UpdateData()
        {
            RaycastHit hit;
            ray = new Ray(bridgeBrickCheck.position, Vector3.down);
            if (Physics.Raycast(ray,out hit,maxDistance,layer))
            {
                BridgeBrick brick = GetBridgeBrickInstance(hit.collider);
                if(brick != null)
                {
                    Data.BridgeBrick = brick;
                    //Debug.Log(brick.GetInstanceID());
                }
            }
            else
            {
                Data.BridgeBrick = null;
            }
            
        }

        private BridgeBrick GetBridgeBrickInstance(Collider col)
        {
            if(Data.BridgeBrick == null)
            {
                BridgeBrick brick = Cache.GetBridgeBrick(col);
                oldColliderInstanceId = col.GetInstanceID();
                return brick;
            }
            else
            {
                if(col.GetInstanceID() == oldColliderInstanceId)
                {
                    return null;
                }
                else
                {
                    BridgeBrick brick = Cache.GetBridgeBrick(col);
                    oldColliderInstanceId = col.GetInstanceID();
                    return brick;
                }
            }
            
        }

        private void OnDrawGizmos()
        {
            if (bridgeBrickCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(bridgeBrickCheck.position, bridgeBrickCheck.position + maxDistance * Vector3.down);
            }
        }
    }
}