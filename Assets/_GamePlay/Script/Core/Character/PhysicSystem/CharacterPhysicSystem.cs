using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.PhysicSystem
{
    using WorldInterfaceSystem;
    public class CharacterPhysicSystem 
    {
        private AbstractPhysicModule module;
        public PhysicData Data;
        public PhysicParameter Parameter;
        public CharacterPhysicSystem(AbstractPhysicModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(PhysicData)) as PhysicData;
            Parameter = ScriptableObject.CreateInstance(typeof(PhysicParameter)) as PhysicParameter;
            this.module = module;
            module.Initialize(this);
        }

        public void SetVelocity(Vector3 velocity)
        {
            module.SetVelocity(velocity);
        }
    }
}