using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    using Brick;
    using BridgeRace.Core.Data;
    using Utilitys;
    using Manager;
    using System;
    using BridgeRace.Core.Character.WorldInterfaceSystem;
    using BridgeRace.Core.Character.NavigationSystem;
    using BridgeRace.Core.Character.PhysicSystem;
    using BridgeRace.Core.Character.LogicSystem;

    public class AbstractCharacter : MonoBehaviour
    {

        [SerializeField]
        protected BrickColor type;
        [SerializeField]
        protected SkinnedMeshRenderer mesh;
        [SerializeField]
        public Transform ContainBrick;
        protected CharacterData Data;

        protected CharacterWorldInterfaceSystem WorldInterfaceSystem;
        protected CharacterNavigationSystem NavigationSystem;
        protected CharacterLogicSystem LogicSystem;
        protected CharacterPhysicSystem PhysicSystem;

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

        //TODO: Combat Function(Covert to a system)
        internal int CharacterCollide()
        {
            //TODO: Return num of brick
            return Data.Bricks.Count;
        }
    }
}
