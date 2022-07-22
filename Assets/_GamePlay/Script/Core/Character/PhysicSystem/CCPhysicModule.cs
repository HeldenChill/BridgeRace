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

        [SerializeField]
        float rotateSpeed = 0.1f;

        Quaternion rotGoal;
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

        public override void SetSmoothRotation(string gameObj, Vector3 direction)
        {
            if (gameObj == GameConst.CHARACTER_ROT)
            {
                rotGoal = Quaternion.LookRotation(direction);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotGoal, rotateSpeed);
            }
            else if (gameObj == GameConst.MODEL_ROT)
            {
                rotGoal = Quaternion.LookRotation(direction);
                charModel.transform.rotation = Quaternion.Slerp(charModel.transform.rotation, rotGoal, rotateSpeed);
            }
            else if (gameObj == GameConst.SENSOR_ROT)
            {
                rotGoal = Quaternion.LookRotation(direction);
                charSensor.transform.rotation = Quaternion.Slerp(charSensor.transform.rotation, rotGoal, rotateSpeed);
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
