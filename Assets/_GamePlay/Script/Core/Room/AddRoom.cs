
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
        private GameObject ground;
        private List<Vector3> entrance = new List<Vector3>();
        private Vector3 roomPos;
        private Dictionary<int, Vector2Int> eatBrickToPos = new Dictionary<int, Vector2Int>();
        private Dictionary<Vector2Int, STimer> posToTimer = new Dictionary<Vector2Int, STimer>();
        private List<Vector2Int> posCanGenerate = new List<Vector2Int>();
        private List<BrickColor> colorCanGenerate = new List<BrickColor>();

        private int xCount;
        private int zCount;
        private float timeGenerate = 3f;
        private int maxNumberColorBrick;

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
        public void AteEatBrick(int instanceID, BrickColor color)
        {
            Vector2Int pos = eatBrickToPos[instanceID];
            posToTimer[pos].Start(timeGenerate, instanceID);
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
            float value = roomSize.x * 2/ (GameplayManager.Inst.NumOfPlayer + 1);
            float index = -roomSize.x + value;

            for(int i = 0; i < GameplayManager.Inst.NumOfPlayer; i++)
            {
                entrance.Add(new Vector3(roomPos.x + index, roomPos.y, roomPos.z + roomSize.y) + add);
                index += value;
            }
        }
        private void InitializeEatBricks()
        {
            InitializeEatBricksData();
            while(colorCanGenerate.Count > 0)
            {
                int colorIndex = Random.Range(0, colorCanGenerate.Count);
                int posindex = Random.Range(0, posCanGenerate.Count);

                BrickColor color = colorCanGenerate[colorIndex];
                Vector2Int pos = posCanGenerate[posindex];
                colorCanGenerate.Remove(color);
                posCanGenerate.Remove(pos);

                //NOTE: Create EatBrick
                GameObject EatBrick = PrefabManager.Inst.PopFromPool(PrefabManager.EAT_BRICK);
                EatBrick.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                EatBrick.transform.localPosition = GetEatBrickPosition(pos);

                eatBrickToPos.Add(EatBrick.GetInstanceID(), pos);

                //NOTE: Change Color depends on player color

                Cache.GetEatBrick(EatBrick).ChangeColor(color);
            }
            
        }
        private void InitializeEatBricksData()
        {
            xCount = (int)(roomSize.x * 2 / EAT_BRICK_DISTANCE.x) + 1 - 2 * MARGIN;
            zCount = (int)(roomSize.y * 2 / EAT_BRICK_DISTANCE.z) + 1 - 2 * MARGIN;
            maxNumberColorBrick = xCount * zCount - GameplayManager.Inst.NumOfPlayer;

            for(int z = 0; z < zCount; z++)
            {
                for(int x = 0; x < xCount; x++)
                {
                    //NOTE: Save essential data  
                    Vector2Int pos = new Vector2Int(x, z);
                    posCanGenerate.Add(pos);
                    STimer timer = new STimer();
                    timer.TimeOut1 += UpdateEventTimer;
                    posToTimer.Add(pos, timer);
                  
                }   
            }
            for(int i = 0; i < GameplayManager.Inst.PlayerColors.Count; i++)
            {
                if(i == GameplayManager.Inst.PlayerColors.Count - 1)
                {
                    for (int j = 0; j < xCount * zCount - maxNumberColorBrick * i; j++)
                    {
                        BrickColor color = GameplayManager.Inst.PlayerColors[i];
                        colorCanGenerate.Add(color);
                    }
                }
                else
                {
                    for (int j = 0; j < maxNumberColorBrick; j++)
                    {
                        BrickColor color = GameplayManager.Inst.PlayerColors[i];
                        colorCanGenerate.Add(color);
                    }
                }
                
            }
            
        }
        private Vector3 GetEatBrickPosition(Vector2Int index)
        {
            float xValue = -roomSize.x + EAT_BRICK_DISTANCE.x * MARGIN;
            float zValue = -roomSize.y + EAT_BRICK_DISTANCE.z * MARGIN;

            xValue += index.x * EAT_BRICK_DISTANCE.x;
            zValue += index.y * EAT_BRICK_DISTANCE.z;
            return new Vector3(xValue, EAT_BRICK_HEIGHT, zValue);
        }
        
        private void UpdateEventTimer(int code)
        {
            Vector2Int pos = eatBrickToPos[code];
            GameObject EatBrick = PrefabManager.Inst.PopFromPool(PrefabManager.EAT_BRICK);
            EatBrick.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
            EatBrick.transform.localPosition = GetEatBrickPosition(pos);

            int index = Random.Range(0, GameplayManager.Inst.PlayerColors.Count);
            BrickColor color = GameplayManager.Inst.PlayerColors[index];
            Cache.GetEatBrick(EatBrick).ChangeColor(color);

            eatBrickToPos.Remove(code);
            eatBrickToPos.Add(EatBrick.GetInstanceID(), pos);
            
        }

        ~AddRoom()
        {
            foreach(var timer in posToTimer)
            {
                timer.Value.TimeOut1 -= UpdateEventTimer;
                MotherTimer.Inst.Push(timer.Value);
            }
        }
    }
}