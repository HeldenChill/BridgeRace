using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class HaveGroundCheckSensor : BaseSensor
    {
        [SerializeField]
        Transform checkHaveGroundPoint;
        [SerializeField]
        float distance;
        [SerializeField]
        LayerMask layer;
        Ray ray = new Ray(Vector3.zero,Vector3.down);
        public override void UpdateData()
        {
            ray.origin = checkHaveGroundPoint.position;
            Data.IsHaveGround = Physics.Raycast(ray, distance, layer);
        }

        private void OnDrawGizmos()
        {
            if(checkHaveGroundPoint != null)
            {
                Gizmos.DrawLine(checkHaveGroundPoint.position, checkHaveGroundPoint.position + Vector3.down * distance);
            }
            
        }
    }
}