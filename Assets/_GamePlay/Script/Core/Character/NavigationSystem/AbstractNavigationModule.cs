using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.NavigationSystem
{
    public abstract class AbstractNavigationModule : MonoBehaviour
    {
        protected NavigationData Data;
        protected NavigationParameter Parameter;

        public void Initialize(CharacterNavigationSystem system)
        {
            Data = system.Data;
            Parameter = system.Parameter;
        }

        public abstract void UpdateData();
    }
}