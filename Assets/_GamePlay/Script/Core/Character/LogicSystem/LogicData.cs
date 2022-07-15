using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using Utilitys;
    public class LogicData : AbstractDataSystem<LogicData>
    {
        public Stack Bricks = new Stack();
        protected override void UpdateDataClone()
        {
            if(Clone == null)
            {
                Clone = CreateInstance(typeof(LogicData)) as LogicData;
            }          
        }
    }
}