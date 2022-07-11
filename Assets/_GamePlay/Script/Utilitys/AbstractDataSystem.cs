using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys
{
    using System;
    public abstract class AbstractDataSystem<T> : ScriptableObject
    {
        public event Action<T> OnUpdateData;

        protected T Clone;
        public void InvokeOnUpdateData()
        {
            UpdateDataClone();
            OnUpdateData?.Invoke(Clone);
        }

        protected abstract void UpdateDataClone();
    }
}
