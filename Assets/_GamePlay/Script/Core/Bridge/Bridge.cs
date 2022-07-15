using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public class Bridge : MonoBehaviour
    {
        [SerializeField]
        protected GameObject BridgeBrick;
        public int NumOfBrick = 10;

        private void Start()
        {
            ConstructBridge();
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
