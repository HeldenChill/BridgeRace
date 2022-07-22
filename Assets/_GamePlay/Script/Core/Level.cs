using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public class Level : MonoBehaviour
    {
        public Transform StaticEnvironment;
        [SerializeField]
        private GameObject Destination;
        private List<AddRoom> rooms = new List<AddRoom>();
        private Dictionary<int, int> playerToRoom = new Dictionary<int, int>();

        Vector2 roomSize1 = Vector2.one * 12;
        Vector2 roomSize2 = Vector2.one * 10;
        Vector2 roomSize3 = Vector2.one * 8;
        public void Initialize(List<int> playerInstanceID)
        {
            //NOTE: Construct Room Here

            rooms.Add(new AddRoom(Vector3.zero, roomSize1));
            rooms[0].ConstructRoom();

            Vector3 nextRoomPos1 = rooms[0].RoomPos + rooms[0].AddNextRoomPos + Vector3.forward * roomSize2.y;
            rooms.Add(new AddRoom(nextRoomPos1, roomSize2));
            rooms[1].ConstructRoom();

            Vector3 nextRoomPos2 = rooms[1].RoomPos + rooms[1].AddNextRoomPos + Vector3.forward * roomSize3.y;
            rooms.Add(new AddRoom(nextRoomPos2, roomSize3));
            rooms[2].ConstructRoom();

            Vector3 nextRoomPos3 = rooms[2].RoomPos + rooms[2].AddNextRoomPos + Vector3.down * 3 + Vector3.forward;
            Destination.transform.localPosition = nextRoomPos3;

            for (int i = 0; i < playerInstanceID.Count; i++)
            {
                playerToRoom.Add(playerInstanceID[i], -1);
            }
        }

        public void SetPlayerRoom(int playerInstanceID,int level)
        {
            playerToRoom[playerInstanceID] += level;
        }
        public void NextPlayerRoom(int playerInstanceID)
        {
            playerToRoom[playerInstanceID] += 1;
            if(playerToRoom[playerInstanceID] >= rooms.Count)
            {
                //TODO: GameWin
            }
        }

        public AddRoom GetCurrentRoom(int playerInstanceID)
        {
            return rooms[playerToRoom[playerInstanceID]];
        }

    }
}