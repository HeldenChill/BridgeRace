using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public class Bridge : MonoBehaviour
    {
        [SerializeField]
        protected GameObject BridgeBrick;
        [SerializeField]
        GameObject fence1;
        [SerializeField]
        GameObject fence2;
        public int NumOfBrick = 10;

        private float length;
        private void Start()
        {
            length = GameConst.CROSS_LENGTH_BRIDGE_BRICK * NumOfBrick;
            ConstructBridge();
            fence1.transform.localScale = new Vector3(0.2f, 2f, length);
            fence2.transform.localScale = new Vector3(0.2f, 2f, length);

            fence1.transform.localPosition = fence1.transform.forward * (length / 2 - 0.15f) + new Vector3(0.6f, 0, 0) + fence1.transform.up * 0.5f;
            fence2.transform.localPosition = fence2.transform.forward * (length / 2 - 0.15f) + new Vector3(-0.6f, 0, 0) + fence2.transform.up * 0.5f;
        }
        public void ConstructBridge()
        {
            Vector3 tempPos = Vector3.zero;
            for(int i = 0; i < NumOfBrick; i++)
            {
                GameObject brick = PrefabManager.Inst.PopFromPool(PrefabManager.Inst.BRIDGE_BRICK);
                brick.transform.parent = gameObject.transform;
                brick.transform.localPosition = tempPos;
                tempPos.y += GameConst.BRIDGE_BRICK_SIZE.y;
                tempPos.z += GameConst.BRIDGE_BRICK_SIZE.z;
            }
        }
    }
}
