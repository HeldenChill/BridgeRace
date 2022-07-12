using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character
{
    public abstract class AbstractModuleSystem<D, P> : MonoBehaviour
        where D : AbstractDataSystem<D>
        where P : AbstractParameterSystem
    {
        public abstract void Initialize(D Data, P Parameter);
        public abstract void UpdateData();

    }
}