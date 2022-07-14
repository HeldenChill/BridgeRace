using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Manager
{
    using BridgeRace.Core.Brick;
    using Utilitys;
    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        List<Material> BrickMaterial;
        public Material GetMaterial(BrickColor color)
        {
            if(color == BrickColor.None)
            {
                return null;
            }

            return BrickMaterial[(int)color];
        }
    }
}