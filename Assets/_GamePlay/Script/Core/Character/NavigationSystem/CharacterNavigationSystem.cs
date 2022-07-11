using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.NavigationSystem
{
    using WorldInterfaceSystem;
    public class CharacterNavigationSystem : AbstractCharacterSystem
    {
        private AbstractNavigationModule module;
        public NavigationData Data;
        public NavigationParameter Parameter;

        public CharacterNavigationSystem(AbstractNavigationModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(NavigationData)) as NavigationData;
            Parameter = ScriptableObject.CreateInstance(typeof(NavigationParameter)) as NavigationParameter;
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

        public void ReceiveInformation(WorldInterfaceData Data)
        {
            Parameter.IsGrounded = Data.IsGrounded;
        }
    }
}