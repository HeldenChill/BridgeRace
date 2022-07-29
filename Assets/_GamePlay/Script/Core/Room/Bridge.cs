using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    using BridgeRace.Core.Brick;
    using Manager;
    using System;

    public class Bridge : MonoBehaviour
    {
        [SerializeField]
        GameObject fence1;
        [SerializeField]
        GameObject fence2;
        public int NumOfBrick = 10;

        private readonly Vector3 FENCE_HORIZONTAL = new Vector3(0.6f, 0, 0);
        private readonly Vector3 FENCE_SCALE_XY = new Vector3(0.2f, 2f, 0);
        private const float FENCE_UP = 0.5f;
        private List<GameObject> bridgeBricks = new List<GameObject>();

        private float length;

        public void ConstructBridge()
        {
            length = GameConst.CROSS_LENGTH_BRIDGE_BRICK * NumOfBrick;
            //Brick
            Vector3 tempPos = Vector3.zero;
            for(int i = 0; i < NumOfBrick; i++)
            {
                GameObject brick = PrefabManager.Inst.PopFromPool(PrefabManager.BRIDGE_BRICK);
                BridgeBrick brickScript = Cache.GetBridgeBrick(brick);
                brickScript.ChangeColor(BrickColor.None);
                brick.transform.parent = gameObject.transform;
                brick.transform.localPosition = tempPos;
                bridgeBricks.Add(brick);

                tempPos.y += GameConst.BRIDGE_BRICK_SIZE.y;
                tempPos.z += GameConst.BRIDGE_BRICK_SIZE.z;
            }

            //Fence
            fence1.transform.localScale = FENCE_SCALE_XY + Vector3.forward * length;
            fence2.transform.localScale = FENCE_SCALE_XY + Vector3.forward * length;

            fence1.transform.localPosition = fence1.transform.forward * (length / 2 - 0.2f) + FENCE_HORIZONTAL + fence1.transform.up * FENCE_UP;
            fence2.transform.localPosition = fence2.transform.forward * (length / 2 - 0.2f) + -FENCE_HORIZONTAL + fence2.transform.up * FENCE_UP;

        }

        public void DeconstructBridge()
        {
            for(int  i = 0; i < bridgeBricks.Count; i++)
            {
                PrefabManager.Inst.PushToPool(bridgeBricks[i], PrefabManager.BRIDGE_BRICK, false);               
            }
            bridgeBricks.Clear();
        }
    }
}
