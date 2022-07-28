using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Manager
{
    using BridgeRace.Core;
    using BridgeRace.Core.Brick;
    using Utilitys;
    [DefaultExecutionOrder(-1)]
    public class GameplayManager : Singleton<GameplayManager>
    {
        public readonly List<BrickColor> PLAYER_COLOR = new List<BrickColor>() { BrickColor.Blue, BrickColor.Yellow, BrickColor.Green, BrickColor.Red, };


        [SerializeField]
        private List<Material> BrickMaterial;       
        [SerializeField]
        private List<GameObject> players;
        [HideInInspector]
        public List<BrickColor> PlayerColors = new List<BrickColor>();
        public int NumOfPlayer => players.Count;

        private void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            for (int i = 0; i < players.Count; i++)
            {
                BrickColor color = PLAYER_COLOR[i];
                players[i].GetComponent<AbstractCharacter>().ChangeColor(color);
                PlayerColors.Add(color);
            }
            LevelManager.Inst.SetPlayers(players);
        }
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