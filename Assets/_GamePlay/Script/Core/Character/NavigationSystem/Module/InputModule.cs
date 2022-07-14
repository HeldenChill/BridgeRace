using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.NavigationSystem
{
    public class InputModule : AbstractNavigationModule
    {
        public override void UpdateData()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 move = (Vector3.right * x + Vector3.forward * z).normalized;
            Data.MoveDirection = move;

            if (Input.GetButtonDown("Jump"))
            {
                Data.Jump = true;
            }
            else
            {
                Data.Jump = false;
            }
        }    
    }
}