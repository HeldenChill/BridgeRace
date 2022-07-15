using BridgeRace.Core.Brick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckBridgeBrickSensor : BaseSensor
    {
        [SerializeField]
        List<Transform> bridgeBrickChecks;
        [SerializeField]
        float maxDistance = 0.3f;
        [SerializeField]
        LayerMask layer;
        Ray ray;

        int oldColliderInstanceId;

        public override void UpdateData()
        {
            for(int i = 0; i < bridgeBrickChecks.Count; i++)
            {
                RaycastHit hit;
                ray = new Ray(bridgeBrickChecks[i].position, Vector3.down);
                if (Physics.Raycast(ray, out hit, maxDistance, layer))
                {
                    BridgeBrick brick = GetBridgeBrickInstance(hit.collider);
                    if (brick != null)
                    {
                        Data.BridgeBrick = brick;
                        break;
                        //Debug.Log(brick.GetInstanceID());
                    }
                }
                else
                {
                    Data.BridgeBrick = null;
                }
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
            if (bridgeBrickChecks != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(bridgeBrickChecks[0].position, bridgeBrickChecks[0].position + maxDistance * Vector3.down);

                Gizmos.color = Color.green;
                for(int i = 1; i < bridgeBrickChecks.Count; i++)
                {
                    Gizmos.DrawLine(bridgeBrickChecks[i].position, bridgeBrickChecks[i].position + maxDistance * Vector3.down);
                }
            }
        }
    }
}