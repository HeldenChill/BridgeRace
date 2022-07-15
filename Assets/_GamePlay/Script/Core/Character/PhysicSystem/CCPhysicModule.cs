using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.PhysicSystem {
    public class CCPhysicModule : AbstractPhysicModule
    {
        public CharacterController controller;
        [SerializeField]
        GameObject charSensor;
        [SerializeField]
        GameObject charModel;     
        public override void SetVelocity(Vector3 velocity)
        {
            controller.Move(velocity * Time.deltaTime);
            Data.Velocity = velocity;
        }

        public override void SetRotation(string gameObj,Quaternion rotation)
        {
            if(gameObj == GameConst.CHARACTER_ROT)
            {
                gameObject.transform.rotation = rotation;
            }
            else if(gameObj == GameConst.MODEL_ROT)
            {
                charModel.transform.rotation = rotation;
            }
            else if(gameObj == GameConst.SENSOR_ROT)
            {
                charSensor.transform.rotation = rotation;
            }
            
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
