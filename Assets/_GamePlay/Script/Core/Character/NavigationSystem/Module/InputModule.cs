using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.NavigationSystem
{
    using Utilitys.Input;
    public class InputModule : AbstractNavigationModule
    {
        [SerializeField]
        JoyStick joyStick;
        Vector2 moveDirection = Vector2.zero;
        private void Awake()
        {
            joyStick.OnMove += UpdateMoveDirection;
        }
        public override void UpdateData()
        {
            //Debug.Log("Move Direction " + moveDirection);
            Vector3 move = (Vector3.right * moveDirection.x + Vector3.forward * moveDirection.y).normalized;
            Data.MoveDirection = move;

            //if (Input.GetButtonDown("Jump"))
            //{
            //    Data.Jump = true;
            //}
            //else
            //{
            //    Data.Jump = false;
            //}
        }    

        private void UpdateMoveDirection(Vector2 moveDirection)
        {
            this.moveDirection = moveDirection;
        }
    }
}