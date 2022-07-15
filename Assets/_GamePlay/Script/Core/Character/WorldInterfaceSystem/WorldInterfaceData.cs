using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    using BridgeRace.Core.Brick;
    using Utilitys;
    public class WorldInterfaceData : AbstractDataSystem<WorldInterfaceData>
    {        
        public bool IsGrounded = false;
        public BridgeBrick BridgeBrick; 
        protected override void UpdateDataClone()
        {
            if(Clone == null)
            {
                Clone = CreateInstance(typeof(WorldInterfaceData)) as WorldInterfaceData;
            }
            Clone.IsGrounded = IsGrounded;
            Clone.BridgeBrick = BridgeBrick;
        }
    }
}