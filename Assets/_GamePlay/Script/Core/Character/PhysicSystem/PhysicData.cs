using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.PhysicSystem
{
    using Utilitys;
    public class PhysicData : AbstractDataSystem<PhysicData>
    {
        public Vector3 Velocity;

        protected override void UpdateDataClone()
        {
            if(Clone == null)
            {
                Clone = CreateInstance(typeof(PhysicData)) as PhysicData;
            }
            Clone.Velocity = Velocity;
        }
    }
}