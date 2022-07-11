using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.LogicSystem
{
    public abstract class AbstractLogicModule : MonoBehaviour
    {
        protected LogicParameter Parameter;
        protected LogicData Data;
        protected LogicEvent Event;
        public void Initialize(CharacterLogicSystem system)
        {
            Parameter = system.Parameter;
            Data = system.Data;
            Event = system.Event;
        }
        public abstract void UpdateData();
    }
}