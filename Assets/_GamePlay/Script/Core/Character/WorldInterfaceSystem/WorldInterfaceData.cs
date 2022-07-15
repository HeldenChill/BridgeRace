using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    using BridgeRace.Core.Brick;
    using Utilitys;
    public class WorldInterfaceData : AbstractDataSystem<WorldInterfaceData>
    {
        private List<EatBrick> eatBrickClone = new List<EatBrick>();

        public List<EatBrick> EatBricks;
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

            //NOTE: Clone list EatBricks
            if(EatBricks != null)
            {
                eatBrickClone.Clear();
                for (int i = 0; i < EatBricks.Count; i++)
                {
                    eatBrickClone.Add(EatBricks[i]);
                }
            }
            Clone.EatBricks = eatBrickClone;
            
        }
    }
}