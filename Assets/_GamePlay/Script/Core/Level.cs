using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public class Level : MonoBehaviour
    {
        public Transform StaticEnvironment;
        private List<AddRoom> rooms = new List<AddRoom>();
        private Dictionary<int, int> playerToRoom = new Dictionary<int, int>();

        public void Initialize(List<int> playerInstanceID)
        {
            rooms.Add(new AddRoom(Vector3.zero, Vector2.one * 7));
            rooms[0].ConstructRoom();
            for(int i = 0; i < playerInstanceID.Count; i++)
            {
                playerToRoom.Add(playerInstanceID[i], 0);
            }
        }

        public void NextRoom(int playerInstanceID)
        {
            playerToRoom[playerInstanceID] += 1;
        }

        public AddRoom GetCurrentRoom(int playerInstanceID)
        {
            return rooms[playerToRoom[playerInstanceID]];
        }

    }
}