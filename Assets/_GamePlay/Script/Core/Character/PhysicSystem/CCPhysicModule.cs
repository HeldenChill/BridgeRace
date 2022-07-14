using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.PhysicSystem {
    public class CCPhysicModule : AbstractPhysicModule
    {
        public CharacterController controller;       
        public override void SetVelocity(Vector3 velocity)
        {
            controller.Move(velocity * Time.deltaTime);
            Data.Velocity = velocity;
        }

        public override void SetRotation(Quaternion rotation)
        {
            gameObject.transform.rotation = rotation;
        }

        public override void UpdateData()
        {
            if (Parameter.GravityParameter < 0.001f) return;
            else
            {
                Data.Velocity.y += Parameter.GRAVITY * Parameter.GravityParameter * Time.deltaTime;
            }
        }
    }
}
