using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.NavigationSystem
{
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

        public void SetCharacterInformation(Transform Player)
        {
            Parameter.Player = Player;
        }
    }
}