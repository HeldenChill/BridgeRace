using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Brick {
    public class EatBrick : AbstractBrick
    {
        [SerializeField]
        Rigidbody rb;
        [SerializeField]
        Collider col;
        Vector3 velocity = new Vector3();
        private void Awake()
        {
            //SetActivePhysic(false);
        }
        public override void Interact(AbstractCharacter containBricks)
        {
            throw new System.NotImplementedException();
        }

        public void BrickFall()
        {
            SetActivePhysic(true);
            int x = Random.Range(-3, 3);
            int z = Random.Range(-3, 3);
            velocity.Set(x, 3.5f, z);
            rb.velocity = velocity;
            rb.angularVelocity = velocity * 10;
        }

        public void SetActivePhysic(bool active)
        {
            if (active)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                col.isTrigger = false;
                
            }
            else
            {
                rb.useGravity = false;
                rb.isKinematic = true;
                col.isTrigger = true;
                rb.velocity = Vector3.zero;
            }
        }
    }
}