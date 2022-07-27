using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Manager
{
    using Utilitys;
    public class PrefabManager : Singleton<PrefabManager>
    {
        //NOTE:Specific for game,remove to reuse
        public static readonly string BRIDGE_BRICK = "BridgeBrick";
        public static readonly string EAT_BRICK = "EatBrick";
        public static readonly string BRIDGE = "Bridge";
        public static readonly string GROUND = "Ground";

        private readonly int INITNUMBER_POOL_OBJECT = 50;

        //-----

        public GameObject pool;
        [SerializeField]
        GameObject BridgeBrick;
        [SerializeField]
        GameObject EatBrick;
        [SerializeField]
        GameObject Bridge;
        [SerializeField]
        GameObject Ground;

        Dictionary<string, Pool> poolData = new Dictionary<string, Pool>();
        protected override void Awake()
        {
            base.Awake();
            CreatePool(BridgeBrick, BRIDGE_BRICK, Quaternion.identity, INITNUMBER_POOL_OBJECT);
            CreatePool(EatBrick, EAT_BRICK, Quaternion.identity, INITNUMBER_POOL_OBJECT);
            CreatePool(Bridge, BRIDGE, Quaternion.identity);
            CreatePool(Ground, GROUND, Quaternion.identity, 3);
        }


        public void CreatePool(GameObject obj, string namePool, Quaternion quaternion = default, int numObj = 10)
        {
            if (!poolData.ContainsKey(namePool))
            {
                GameObject newPool = Instantiate(pool, Vector3.zero, Quaternion.identity);
                Pool poolScript = newPool.GetComponent<Pool>();
                newPool.name = namePool;
                poolScript.Initialize(obj, quaternion, numObj);
                poolData.Add(namePool, poolScript);
            }
        }

        public void PushToPool(GameObject obj, string namePool, bool checkContain = true)
        {
            if (!poolData.ContainsKey(namePool))
            {
                CreatePool(obj, namePool);
            }

            poolData[namePool].Push(obj, checkContain);
        }

        public GameObject PopFromPool(string namePool, GameObject obj = null)
        {
            if (!poolData.ContainsKey(namePool))
            {
                if (obj == null)
                {
                    Debug.LogError("No pool name " + namePool + " was found!!!");
                    return null;
                }
            }

            return poolData[namePool].Pop();
        }

    }
}