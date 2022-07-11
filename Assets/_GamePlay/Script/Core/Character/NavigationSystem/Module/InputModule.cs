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

            Vector3 move = (transform.right * x + transform.forward * z).normalized;
            Data.MoveDirection = move;

            if (Input.GetButtonDown("Jump"))
            {
                Data.Jump = true;
                Debug.Log("Jump");
            }
            else
            {
                Data.Jump = false;
            }
        }    
    }
}