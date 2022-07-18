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
        public readonly List<BrickColor> PlayerColors = new List<BrickColor>() {BrickColor.Red, BrickColor.Blue, BrickColor.Yellow , BrickColor.Green };
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