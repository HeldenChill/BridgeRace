using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckGroundSensor : BaseSensor
    {
        public Transform groundCheck;
        public float distance = 0.4f;
        public LayerMask layer;
        public override void UpdateData()
        {
            Data.IsGrounded = Physics.CheckSphere(groundCheck.position, distance, layer);
        }

        private void OnDrawGizmos()
        {
            if(groundCheck != null)
            {
                Gizmos.DrawSphere(groundCheck.position, distance);
            }       
        }
    }
}