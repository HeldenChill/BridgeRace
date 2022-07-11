using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using WorldInterfaceSystem;
    using NavigationSystem;
    using PhysicSystem;
    public class CharacterLogicSystem : AbstractCharacterSystem
    {
        private AbstractLogicModule module;
        public LogicData Data;
        public LogicEvent Event;
        public LogicParameter Parameter;

        public CharacterLogicSystem(AbstractLogicModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(LogicData)) as LogicData;
            Parameter = ScriptableObject.CreateInstance(typeof(LogicParameter)) as LogicParameter;
            Event = ScriptableObject.CreateInstance(typeof(LogicEvent)) as LogicEvent;
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

        public void ReceiveInformation(NavigationData Data)
        {
            Parameter.MoveDirection = Data.MoveDirection;
            Parameter.Jump = Data.Jump;
        }

        public void ReceiveInformation(PhysicData Data)
        {
            Parameter.Velocity = Data.Velocity;
        }
    }
}