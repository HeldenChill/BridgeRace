using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.PhysicSystem
{
    public abstract class AbstractPhysicModule : MonoBehaviour
    {
        protected PhysicData Data;
        protected PhysicParameter Parameter;
        public void Initialize(CharacterPhysicSystem system)
        {
            Data = system.Data;
            Parameter = system.Parameter;
        }
        public abstract void SetVelocity(Vector3 velocity);
    }
}
