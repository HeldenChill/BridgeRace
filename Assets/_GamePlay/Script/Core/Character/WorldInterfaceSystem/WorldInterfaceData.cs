using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    using BridgeRace.Core.Brick;
    using Utilitys;
    public class WorldInterfaceData : AbstractDataSystem<WorldInterfaceData>
    {
        private List<EatBrick> eatBrickClone = new List<EatBrick>(); //TODO: Need to change
        private List<EatBrick> visionEatBrickClone = new List<EatBrick>(); //TODO: Need to change
        private List<AbstractCharacter> charactersClone = new List<AbstractCharacter>(); //TODO: Need to change

        public List<EatBrick> VisionEatBricks;

        public List<EatBrick> EatBricks;
        public List<AbstractCharacter> Characters;
        public BridgeBrick BridgeBrick;

        public bool IsHaveGround = false;
        public bool IsGrounded = false;
        public bool IsExitRoom = false;
        public int CurrentRoomID = 0;

        protected override void UpdateDataClone()
        {
            if(Clone == null)
            {
                Clone = CreateInstance(typeof(WorldInterfaceData)) as WorldInterfaceData;
            }
            Clone.IsHaveGround = IsHaveGround;
            Clone.IsGrounded = IsGrounded;
            Clone.IsExitRoom = IsExitRoom;
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
            
            if (VisionEatBricks != null)
            {
                visionEatBrickClone.Clear();
                for(int i = 0; i < VisionEatBricks.Count; i++)
                {
                    visionEatBrickClone.Add(VisionEatBricks[i]);
                }
            }

            if(Characters != null)
            {
                charactersClone.Clear();
                for(int i = 0; i < Characters.Count; i++)
                {
                    charactersClone.Add(Characters[i]);
                }
            }

            Clone.Characters = charactersClone;
            Clone.EatBricks = eatBrickClone;
            Clone.VisionEatBricks = visionEatBrickClone;
            Clone.CurrentRoomID = CurrentRoomID;
        }
    }
}