using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character
{
    public abstract class AbstractCharacterSystem
    {
        protected abstract void UpdateData();
        protected abstract void InvokeOnUpdateData();
        public void Run()
        {
            UpdateData();
            InvokeOnUpdateData();
        }
    }
}