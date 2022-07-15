using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using Utilitys;
    public class PlayerLogicModule : AbstractLogicModule
    {
        private Vector3 velocity;
        private Quaternion rotation;
        private Vector2 direction = Vector2.zero;
        public override void UpdateData()
        {
            CollideBridgeBrickHandle();
            RotationHandle();
            MovementHandle();                 
        }
        private void CollideBridgeBrickHandle()
        {
            if(Parameter.BridgeBrick != null)
            {
                Parameter.BridgeBrick.ChangeColor(Brick.BrickColor.Blue);
            }
        }
        private void RotationHandle()
        {
            Vector2 newDirection = new Vector2(Parameter.MoveDirection.x, Parameter.MoveDirection.z);
            if (!MathHelper.IsApproximately(newDirection, direction) && newDirection.sqrMagnitude > 0.001f)
            {
                float angle = Vector2.SignedAngle(newDirection, Vector2.up);
                rotation = Quaternion.Euler(0, angle, 0);
                Event.SetRotation(GameConst.SENSOR_ROT,rotation);
                Event.SetRotation(GameConst.MODEL_ROT,rotation);
            }
        }
        private void MovementHandle()
        {                       
            if (Parameter.IsGrounded && Parameter.Velocity.y < 0)
            {
                velocity.y = -1f;
            }
            velocity = Parameter.MoveDirection * Parameter.Speed;

            if (Parameter.IsGrounded && Parameter.Jump && Parameter.Velocity.y < 0.01f)
            {
                velocity.y = Parameter.JumpVelocity;
            }
            else
            {
                velocity.y = Parameter.Velocity.y;
            }

            Event.SetVelocity(velocity);
        }

    }
}