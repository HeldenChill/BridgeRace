using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    public class PlayerLogicModule : AbstractLogicModule
    {
        private Vector3 velocity;
        public override void UpdateData()
        {
            
            if (Parameter.IsGrounded && Parameter.Velocity.y < 0)
            {
                velocity.y = -2f;
            }
            velocity = Parameter.MoveDirection * Parameter.Speed;
            Event.SetVelocity(velocity);

            if (Parameter.IsGrounded && Parameter.Jump && Parameter.Velocity.y < 0.01f)
            {
                velocity.y = Parameter.JumpVelocity;
            }
            velocity.y += Parameter.GRAVITY * Time.deltaTime;
            Event.SetVelocity(velocity);
        }
    }
}