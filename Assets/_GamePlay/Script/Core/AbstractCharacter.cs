using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    using Brick;
    using BridgeRace.Core.Data;
    using Utilitys;
    using Manager;

    public class AbstractCharacter : MonoBehaviour
    {
        [SerializeField]
        protected BrickColor type;
        [SerializeField]
        protected SkinnedMeshRenderer mesh;
        [SerializeField]
        public Transform ContainBrick;
        

        public BrickColor Type => type;

        private void Start()
        {
            ChangeColor(type);
        }
        protected virtual void OnEnable()
        {            
        //    BrickCollide.OnCollider += OnCollider;
        }

        protected virtual void OnDisable()
        {
            //BrickCollide.OnCollider -= OnCollider;
        }

        public virtual void ChangeColor(BrickColor color)
        {
            type = color;
            Material mat = GameplayManager.Inst.GetMaterial(color);
            mesh.material = mat;
        }

    }
}
