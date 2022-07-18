
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    using BridgeRace.Manager;
    public class AddRoom : MonoBehaviour
    {
        private const float SIZE_Y_ROOM = 1f;
        [SerializeField]
        private Vector2 roomSize;
        GameObject Ground;
        private List<Vector3> entrance = new List<Vector3>();
        private Vector3 roomPos;

        public void Initialize(Vector3 roomPos,Vector2 roomSize)
        {
            this.roomPos = roomPos;
            this.roomSize = roomSize;
            Ground.transform.localPosition = roomPos;
            Ground.transform.localScale = new Vector3(roomSize.x * 2, SIZE_Y_ROOM, roomSize.y * 2);
        }
        private void InitializeBridgePosition()
        {
            Vector3 add = new Vector3(0, (SIZE_Y_ROOM - GameConst.BRIDGE_BRICK_SIZE.y) / 2, GameConst.BRIDGE_BRICK_SIZE.z / 2);
            float value = roomSize.x * 2/ (GameplayManager.NumOfPlayer + 1);
            float index = -roomSize.x + value;

            for(int i = 0; i < GameplayManager.NumOfPlayer; i++)
            {
                entrance.Add(new Vector3(roomPos.x + index, roomPos.y, roomPos.z + roomSize.y) + add);
                index += value;
            }
        }
    }
}