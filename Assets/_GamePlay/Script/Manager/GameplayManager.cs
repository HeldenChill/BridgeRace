using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Manager
{
    using BridgeRace.Core.Brick;
    using Utilitys;
    [DefaultExecutionOrder(-1)]
    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        List<Material> BrickMaterial;
        static int numOfPlayer = 4;

        public static int NumOfPlayer => numOfPlayer;

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