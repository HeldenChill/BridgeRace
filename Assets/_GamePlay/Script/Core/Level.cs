using BridgeRace.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    using Data;
    public class Level : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject Destination;
        [SerializeField]
        private List<LevelData> Datas;
        private int currentIndexLevel = 0;

        private List<AddRoom> rooms = new List<AddRoom>();
        private List<GameObject> players;

        private Dictionary<int, int> playerToRoom = new Dictionary<int, int>();
        private Dictionary<int, float> player2OldHeight = new Dictionary<int, float>();
        private List<int> playerInstanceIDs = new List<int>();
        private bool isEndLevel = false;

        public Transform StaticEnvironment;
        public Transform WinPosition;
        public event Action<int> OnWin;
        public bool IsEndLevel => isEndLevel;
        public int CurrentLevelIndex => currentIndexLevel;

        public event Action OnStart;

        public void Initialize(List<GameObject> players)
        {
            //NOTE: Construct Room Here
            for(int i = 0; i < players.Count; i++)
            {
                playerInstanceIDs.Add(players[i].GetInstanceID());
                this.players = players;
            }
            //ConstructLevel(0);
            //SetPlayerPosition();
        }

        public void SetPlayerPosition()
        {
            float z = -8;
            float x = -6;
            for(int i = 0; i < players.Count; i++)
            {
                CharacterController cc = players[i].GetComponent<CharacterController>();

                cc.enabled = false;
                cc.transform.position = new Vector3(x + i * 4, 2, z);
                cc.enabled = true;

            }
            
            OnStart?.Invoke();
        }
        public void SetPlayerRoom(int playerInstanceID, float heightY)
        {
            if (isEndLevel)
                return;

            if (!player2OldHeight.ContainsKey(playerInstanceID))
            {
                player2OldHeight.Add(playerInstanceID, heightY);
                playerToRoom[playerInstanceID] += 1;
            }
            else
            {
                if (heightY > player2OldHeight[playerInstanceID])
                {
                    playerToRoom[playerInstanceID] += 1;

                }
                else
                {
                    playerToRoom[playerInstanceID] -= 1;
                }
                player2OldHeight[playerInstanceID] = heightY;

                if (playerToRoom[playerInstanceID] >= rooms.Count)
                {
                    OnWin?.Invoke(playerInstanceID);                   
                    isEndLevel = true;
                    if (playerInstanceID == playerInstanceIDs[0])
                    {
                        UIManager.Inst.OpenUI(UIID.UICVictory);
                    }
                    else
                    {
                        UIManager.Inst.OpenUI(UIID.UICFail);
                    }
                    DeconstructLevel();
                }
            }
            //Debug.Log("Room: " + playerToRoom[playerInstanceID]);
        }
        public void ConstructLevel(int index)
        {
            currentIndexLevel = index;
            isEndLevel = false;
            playerToRoom.Clear();
            player2OldHeight.Clear();
            rooms.Clear();

            rooms.Add(new AddRoom(Vector3.zero, Datas[index].RoomSize1));
            rooms[0].ConstructRoom();

            Vector3 nextRoomPos1 = rooms[0].RoomPos + rooms[0].AddNextRoomPos + Vector3.forward * Datas[index].RoomSize2.y;
            rooms.Add(new AddRoom(nextRoomPos1, Datas[index].RoomSize2));
            rooms[1].ConstructRoom();


            Vector3 nextRoomPos2 = rooms[1].RoomPos + rooms[1].AddNextRoomPos + Vector3.forward * Datas[index].RoomSize3.y;
            rooms.Add(new AddRoom(nextRoomPos2, Datas[index].RoomSize3, 1));
            rooms[2].ConstructRoom();


            Vector3 nextRoomPos3 = rooms[2].RoomPos + rooms[2].AddNextRoomPos + Vector3.down * 3 + Vector3.forward * 0.5f;
            Destination.transform.localPosition = nextRoomPos3;

            for (int i = 0; i < playerInstanceIDs.Count; i++)
            {
                playerToRoom.Add(playerInstanceIDs[i], -1);
            }
            SetPlayerPosition();
        }

        
        public void DeconstructLevel()
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                rooms[i].DeconstructRoom();
            }

            //NOTE: Hereh may cause error
            List<GameObject> eatBricks = new List<GameObject>();
            for (int i = 0; i < StaticEnvironment.childCount; i++)
            {
                eatBricks.Add(StaticEnvironment.GetChild(i).gameObject);
            }

            for(int i = 0; i < eatBricks.Count; i++)
            {
                Cache.GetEatBrick(eatBricks[i]).SetActivePhysic(false);
                eatBricks[i].gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                PrefabManager.Inst.PushToPool(eatBricks[i], PrefabManager.EAT_BRICK, false);
            }

         
        }

        public AddRoom GetCurrentRoom(int playerInstanceID)
        {
            if (isEndLevel)
                return null;

            if(playerToRoom[playerInstanceID] >= rooms.Count)
            {
                return null;
            }
            return rooms[playerToRoom[playerInstanceID]];
        }

    }
}