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
        protected CharacterData Data;
        [SerializeField]
        protected CheckCollide BrickCollide;
        [SerializeField]
        protected SkinnedMeshRenderer mesh;
        [SerializeField]
        public Transform ContainBrick;
        

        public BrickColor Type => type;

        protected virtual void OnEnable()
        {
            ChangeColor(type);
            BrickCollide.OnCollider += OnCollider;
        }

        protected virtual void OnDisable()
        {
            BrickCollide.OnCollider -= OnCollider;
        }

        private void OnCollider(Collider other)
        {
            EatBrick brick = other.GetComponent<EatBrick>();
            if(brick != null)
            {
                AddBrick(brick);
            }
        }

        public EatBrick GetBrick()
        {
            if (Data.Bricks.Count == 0)
            {
                return null;
            }
            else
            {
                EatBrick brick = (EatBrick)Data.Bricks.Pop();
                //TO DO: Push this brick to pool
                return brick;
            }
        }

        public void AddBrick(EatBrick brick)
        {
            if (brick.Color == type || brick.Color == BrickColor.Gray)
            {
                if(brick.Color == BrickColor.Gray)
                {
                    brick.ChangeColor(type);
                }

                Data.Bricks.Push(brick);
                Vector3 pos = Vector3.zero;
                pos.y = (Data.Bricks.Count - 1) * GameConst.BRICK_HEIGHT;
                brick.gameObject.transform.parent = ContainBrick;
                brick.gameObject.transform.localPosition = pos;
                brick.gameObject.transform.localRotation = Quaternion.identity;
            }
        }

        public virtual void ChangeColor(BrickColor color)
        {
            type = color;
            Material mat = GameplayManager.Inst.GetMaterial(color);
            mesh.material = mat;
        }

    }
}
