using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Brick
{
    public class BridgeBrick : AbstractBrick
    {
        [SerializeField]
        GameObject model;
        protected override void Start()
        {
            base.Start();
            if (type == BrickColor.None)
            {
                model.SetActive(false);
            }
        }
        public override void Interact(AbstractCharacter containBricks)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeColor(BrickColor color)
        {
            if(color == BrickColor.None)
            {
                type = color;
                model.SetActive(false);
                return;
            }

            if(type == BrickColor.None && color != type)
            {
                model.SetActive(true);
            }
            base.ChangeColor(color);           
        }
    }
}
