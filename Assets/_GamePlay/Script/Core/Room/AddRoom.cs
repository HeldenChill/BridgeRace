
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    using BridgeRace.Core.Brick;
    using BridgeRace.Manager;
    using Utilitys.Timer;
    public class AddRoom 
    {
        private const int MARGIN = 2;
        private const float SIZE_Y_ROOM = 1f;
        private readonly float EAT_BRICK_HEIGHT = SIZE_Y_ROOM / 2 + GameConst.EAT_BRICK_HEIGHT / 2;
        private readonly Vector3 EAT_BRICK_DISTANCE = new Vector3(1, 0, 1);
        private Vector2 roomSize;
        GameObject ground;
        private List<Vector3> entrance = new List<Vector3>();
        private Vector3 roomPos;

        private int xCount;
        private int zCount;
        STimer test = new STimer();

        public AddRoom(Vector3 roomPos,Vector2 roomSize)
        {
            this.roomPos = roomPos;
            this.roomSize = roomSize;     
        }

        public void ConstructRoom()
        {
            ground = PrefabManager.Inst.PopFromPool(PrefabManager.GROUND);
            ground.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
            ground.transform.localPosition = roomPos;
            ground.transform.localScale = new Vector3(roomSize.x * 2, SIZE_Y_ROOM, roomSize.y * 2);
            InitializeBridge();
            InitializeEatBricks();
        }
        private void InitializeBridge()
        {
            GetBridgePosition();
            for(int i = 0; i < entrance.Count; i++)
            {
                GameObject bridge = PrefabManager.Inst.PopFromPool(PrefabManager.BRIDGE);
                bridge.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                bridge.transform.localPosition = entrance[i];
                Cache.GetBridge(bridge).ConstructBridge();
            }
            
        }
        private void GetBridgePosition()
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
        private void InitializeEatBricks()
        {
            xCount = (int)(roomSize.x * 2 / EAT_BRICK_DISTANCE.x) + 1 - 2 * MARGIN;
            zCount = (int)(roomSize.y * 2 / EAT_BRICK_DISTANCE.z) + 1 - 2 * MARGIN;

            float xValue = -roomSize.x + EAT_BRICK_DISTANCE.x * MARGIN;
            float zValue = -roomSize.y + EAT_BRICK_DISTANCE.z * MARGIN;

            for(int z = 0; z < zCount; z++)
            {
                for(int x = 0; x < xCount; x++)
                {
                    //NOTE: Create EatBrick

                    GameObject EatBrick = PrefabManager.Inst.PopFromPool(PrefabManager.EAT_BRICK);
                    EatBrick.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                    EatBrick.transform.localPosition = new Vector3(xValue, EAT_BRICK_HEIGHT, zValue);

                    //NOTE: Change Color depends on player color
                    int index = Random.Range(0, GameplayManager.Inst.PlayerColors.Count);
                    BrickColor color = GameplayManager.Inst.PlayerColors[index];
                    Cache.GetEatBrick(EatBrick).ChangeColor(color);
                    xValue += EAT_BRICK_DISTANCE.x;        
                }   
                zValue += EAT_BRICK_DISTANCE.z;
                xValue = -roomSize.x + EAT_BRICK_DISTANCE.x * MARGIN;
            }
        }

        private void UpdateEventTimer(int code)
        {
            
        }

        ~AddRoom()
        {
            
        }
    }
}