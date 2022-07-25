using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using WorldInterfaceSystem;
    using NavigationSystem;
    using PhysicSystem;
    using BridgeRace.Core.Brick;
    using BridgeRace.Core.Data;

    public class CharacterLogicSystem : AbstractCharacterSystem<AbstractLogicModule,LogicData,LogicParameter>
    {
        public LogicEvent Event;
        public CharacterLogicSystem(AbstractLogicModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(LogicData)) as LogicData;
            Parameter = ScriptableObject.CreateInstance(typeof(LogicParameter)) as LogicParameter;
            Event = ScriptableObject.CreateInstance(typeof(LogicEvent)) as LogicEvent;
            this.module = module;
            module.Initialize(Data,Parameter,Event);
        }

        public void SetCharacterInformation(Transform ContainBrick,int PlayerInstanceID)
        {           
            Parameter.ContainBrick = ContainBrick;
            Parameter.PlayerInstanceID = PlayerInstanceID;
        }
        public void SetCharacterInformation(BrickColor CharacterType)
        {
            Parameter.CharacterType = CharacterType;
        }
        public void SetCharacterData(CharacterData CharacterData)
        {
            Data.CharacterData = CharacterData;
        }
        public void ReceiveInformation(WorldInterfaceData Data)
        {
            Parameter.IsGrounded = Data.IsGrounded;
            Parameter.BridgeBrick = Data.BridgeBrick;
            Parameter.EatBricks = Data.EatBricks;
            Parameter.IsExitRoom = Data.IsExitRoom;
            Parameter.Characters = Data.Characters;
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

        public void Fall() //TODO: Temp,need to convert to combat system // TEST
        {
            ((CharacterLogicModule)module).Fall();
        }
    }
}