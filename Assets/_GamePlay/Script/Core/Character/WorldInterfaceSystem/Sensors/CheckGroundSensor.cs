using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckGroundSensor : BaseSensor
    {
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        public override void UpdateData()
        {
            Data.IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        private void OnDrawGizmos()
        {
            if(groundCheck != null)
            {
                Gizmos.DrawSphere(groundCheck.position, groundDistance);
            }       
        }
    }
}