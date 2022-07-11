using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class WorldInterfaceModule : MonoBehaviour
    {
        protected CharacterWorldInterfaceSystem system;
        [SerializeField]
        List<BaseSensor> sensors;
        public virtual void Initialize(CharacterWorldInterfaceSystem system)
        {
            this.system = system;
            for(int i = 0; i < sensors.Count; i++)
            {
                sensors[i].Initialize(system.Data);
            }
        }
        public virtual void UpdateData()
        {
            for (int i = 0; i < sensors.Count; i++)
            {
                sensors[i].UpdateData();
            }
        }
    }
}