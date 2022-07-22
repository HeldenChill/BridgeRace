using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.NavigationSystem
{
    using BridgeRace.Core.Brick;
    using BridgeRace.Core.Data;
    using WorldInterfaceSystem;
    public class CharacterNavigationSystem : AbstractCharacterSystem<AbstractNavigationModule,NavigationData,NavigationParameter>
    {
        public CharacterNavigationSystem(AbstractNavigationModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(NavigationData)) as NavigationData;
            Parameter = ScriptableObject.CreateInstance(typeof(NavigationParameter)) as NavigationParameter;
            this.module = module;
            module.Initialize(Data,Parameter);
        }

        public void ReceiveInformation(WorldInterfaceData Data)
        {
            Parameter.IsGrounded = Data.IsGrounded;
            Parameter.EatBricks = Data.VisionEatBricks;
        }

        public void SetCharacterInformation(Transform Player, int PlayerInstanceID)
        {
            Parameter.Player = Player;
            Parameter.PlayerInstanceID = PlayerInstanceID;
        }

        public void SetCharacterInformation(BrickColor CharacterType)
        {
            Parameter.CharacterType = CharacterType;
        }


        public void SetCharacterData(CharacterData CharacterData)
        {
            Parameter.CharacterData = CharacterData;
        }
    }
}