using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character
{
    using System;
    public abstract class AbstractCharacterSystem<M,D,P>
        where M : AbstractModuleSystem<D,P>
        where D : AbstractDataSystem<D>
        where P : AbstractParameterSystem
    {
        public event Action<D> OnUpdateData;
        protected M module;
        protected D Data;
        protected P Parameter;
        protected virtual void UpdateData()
        {
            module.UpdateData();
        }
        protected virtual void InvokeOnUpdateData()
        {
            D data = Data.OnUpdateData();
            OnUpdateData?.Invoke(data);
        }
        public void Run()
        {
            UpdateData();
            InvokeOnUpdateData();
        }
    }
}