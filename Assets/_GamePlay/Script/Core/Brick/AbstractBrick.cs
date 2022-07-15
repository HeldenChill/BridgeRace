using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Brick
{
    using BridgeRace.Manager;
    public enum BrickColor
    {      
        Red = 0,
        Blue = 1,
        Green = 2,
        Yellow = 3,
        Fence = 4,  
        Plane = 5,
        Gray = 6,
        None = 7,
    }

    public abstract class AbstractBrick : MonoBehaviour
    {
        [SerializeField]
        protected BrickColor type;
        public BrickColor Color => type;
        [SerializeField]
        protected MeshRenderer mesh;
       
        public abstract void Interact(AbstractCharacter character);
        protected virtual void Start()
        {
            ChangeColor(type);
        }
        public virtual void ChangeColor(BrickColor color)
        {
            if(type == color)
            {
                return;
            }
            type = color;
            Material mat = GameplayManager.Inst.GetMaterial(color);
            if(mat != null)
            {
                mesh.material = mat;
            }           
        }
    }
}