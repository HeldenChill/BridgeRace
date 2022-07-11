using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CharacterWorldInterfaceSystem : AbstractCharacterSystem
    {
        private WorldInterfaceModule module;
        public WorldInterfaceData Data;
        public WorldInterfaceParameter Parameter;
        
        public CharacterWorldInterfaceSystem(WorldInterfaceModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(WorldInterfaceData)) as WorldInterfaceData;
            Parameter = ScriptableObject.CreateInstance(typeof(WorldInterfaceParameter)) as WorldInterfaceParameter;
            this.module = module;
            module.Initialize(this);
        }

        protected override void UpdateData()
        {
            module.UpdateData();
        }

        protected override void InvokeOnUpdateData()
        {
            Data.InvokeOnUpdateData();
        }
    }
}