using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.LogicSystem
{
    public abstract class AbstractLogicModule : AbstractModuleSystem<LogicData,LogicParameter>
    {
        protected LogicParameter Parameter;
        protected LogicData Data;
        protected LogicEvent Event;
        public override void Initialize(LogicData Data,LogicParameter Parameter)
        {
            this.Parameter = Parameter;
            this.Data = Data;
        }

        public void Initialize(LogicData Data, LogicParameter Parameter, LogicEvent Event)
        {
            this.Event = Event;
            Initialize(Data, Parameter);
        }
    }
}