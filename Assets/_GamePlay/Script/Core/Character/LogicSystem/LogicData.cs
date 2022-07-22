using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using BridgeRace.Core.Data;
    using Utilitys;
    public class LogicData : AbstractDataSystem<LogicData>
    {
        public CharacterData CharacterData;
        protected override void UpdateDataClone()
        {
            if(Clone == null)
            {
                Clone = CreateInstance(typeof(LogicData)) as LogicData;
            }          
        }
    }
}