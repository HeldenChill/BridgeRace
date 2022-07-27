using BridgeRace.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public class Level : MonoBehaviour
    {
        public Transform StaticEnvironment;
        public Transform WinPosition;

        public event Action<int> OnWin;
        [SerializeField]
        private GameObject Destination;
        private List<AddRoom> rooms = new List<AddRoom>();
        private Dictionary<int, int> playerToRoom = new Dictionary<int, int>();
        private Dictionary<int, float> player2OldHeight = new Dictionary<int, float>();
        private List<int> playerInstanceIDs;
        

        Vector2 roomSize1 = Vector2.one * 12;
        Vector2 roomSize2 = Vector2.one * 10;
        Vector2 roomSize3 = Vector2.one * 8;
        public void Initialize(List<int> playerInstanceID)
        {
            //NOTE: Construct Room Here
            this.playerInstanceIDs = playerInstanceID;

            rooms.Add(new AddRoom(Vector3.zero, roomSize1));
            rooms[0].ConstructRoom();

            Vector3 nextRoomPos1 = rooms[0].RoomPos + rooms[0].AddNextRoomPos + Vector3.forward * roomSize2.y;
            rooms.Add(new AddRoom(nextRoomPos1, roomSize2));
            rooms[1].ConstructRoom();


            Vector3 nextRoomPos2 = rooms[1].RoomPos + rooms[1].AddNextRoomPos + Vector3.forward * roomSize3.y;
            rooms.Add(new AddRoom(nextRoomPos2, roomSize3, 1));
            rooms[2].ConstructRoom();


            Vector3 nextRoomPos3 = rooms[2].RoomPos + rooms[2].AddNextRoomPos + Vector3.down * 3 + Vector3.forward * 0.5f;
            Destination.transform.localPosition = nextRoomPos3;

            for (int i = 0; i < playerInstanceID.Count; i++)
            {
                playerToRoom.Add(playerInstanceID[i], -1);                     
            }
        }

        public void SetPlayerRoom(int playerInstanceID,float heightY)
        {
            if (!player2OldHeight.ContainsKey(playerInstanceID))
            {
                player2OldHeight.Add(playerInstanceID, heightY);
                playerToRoom[playerInstanceID] += 1;
            }
            else
            {
                if(heightY > player2OldHeight[playerInstanceID])
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
                    if (playerInstanceID == playerInstanceIDs[0])
                    {
                        UIManager.Inst.OpenUI(UIID.UICVictory);
                    }
                    else
                    {
                        UIManager.Inst.OpenUI(UIID.UICFail);
                    }
                }
            }
            //Debug.Log("Room: " + playerToRoom[playerInstanceID]);
        }

        public AddRoom GetCurrentRoom(int playerInstanceID)
        {
            if(playerToRoom[playerInstanceID] >= rooms.Count)
            {
                return null;
            }
            return rooms[playerToRoom[playerInstanceID]];
        }

    }
}