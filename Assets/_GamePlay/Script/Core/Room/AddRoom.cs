
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
        //NOTE: Reuse the room
        private const int MARGIN = 2;
        private const float SIZE_Y_ROOM = 1f;
        private readonly float EAT_BRICK_HEIGHT = SIZE_Y_ROOM / 2 + GameConst.EAT_BRICK_HEIGHT / 2;
        private readonly Vector3 EAT_BRICK_DISTANCE = new Vector3(1.25f, 0, 1.25f);
        private Vector2 roomSize;

        private GameObject ground;
        private List<GameObject> bridges = new List<GameObject>();
        private List<GameObject> eatBricks = new List<GameObject>();

        private List<Vector3> entrance1 = new List<Vector3>();
        private List<Vector3> entrance2 = new List<Vector3>();
        private Vector3 roomPos;
        private Vector3 addNextRoomPos = default;


        private Dictionary<int, Vector2Int> eatBrickToPos = new Dictionary<int, Vector2Int>();
        private Dictionary<Vector2Int, STimer> posToTimer = new Dictionary<Vector2Int, STimer>();
        private List<Vector2Int> posCanGenerate = new List<Vector2Int>();
        private List<BrickColor> colorCanGenerate = new List<BrickColor>();
        private List<int> oldPlayerInstanceID = new List<int>();

        private int xCount;
        private int zCount;
        private float timeGenerate = 3f;
        private int maxNumberColorBrick;
        private int numOfBridge;

        public Vector3 AddNextRoomPos => addNextRoomPos + Vector3.forward * roomSize.y;
        public Vector3 RoomPos => roomPos;
        public Vector2 RoomSize => roomSize;
        public List<Vector3> Entrance1 => entrance1;
        public List<Vector3> Entrance2 => entrance2;

        public AddRoom(Vector3 roomPos,Vector2 roomSize, int numOfBridge = -1)
        {
            this.roomPos = roomPos;
            this.roomSize = roomSize;
            this.numOfBridge = numOfBridge;
        }

        public void ConstructRoom()
        {
            oldPlayerInstanceID.Clear();
            ground = PrefabManager.Inst.PopFromPool(PrefabManager.GROUND);
            ground.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
            ground.transform.localPosition = roomPos;
            ground.transform.localScale = new Vector3(roomSize.x * 2, SIZE_Y_ROOM, roomSize.y * 2);
            InitializeBridge();
            InitializeEatBricksData();
        }
        public int GetGroundInstancedID() => ground.GetInstanceID();
        public void AddColorAndBrick(int playerInstanceID, BrickColor color)
        {
            if (oldPlayerInstanceID.Contains(playerInstanceID))
                return;

            int valueToGenerate;
            if(oldPlayerInstanceID.Count < GameplayManager.Inst.NumOfPlayer)
            {
                valueToGenerate = maxNumberColorBrick;
            }
            else
            {
                valueToGenerate = xCount * zCount - maxNumberColorBrick * (oldPlayerInstanceID.Count - 1);
            }

            for (int i = 0; i < valueToGenerate; i++)
            {
                colorCanGenerate.Add(color);
            }

            int x = 0;
            while (x < valueToGenerate)
            {
                BrickColor colorIndex = GetBrickColor();
                Vector2Int pos = GetBrickPosition();

                //NOTE: Create EatBrick
                GameObject EatBrick = PrefabManager.Inst.PopFromPool(PrefabManager.EAT_BRICK);
                EatBrick.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                EatBrick.transform.localPosition = GetEatBrickPosition(pos);
                eatBricks.Add(EatBrick);

                eatBrickToPos.Add(EatBrick.GetInstanceID(), pos);

                //NOTE: Change Color depends on player color

                Cache.GetEatBrick(EatBrick).ChangeColor(colorIndex);
                x++;
            }

        }
        public void AteEatBrick(GameObject eatBrick, BrickColor color)
        {
            int instanceID = eatBrick.GetInstanceID();
            if (eatBrickToPos.ContainsKey(instanceID))
            {
                Vector2Int pos = eatBrickToPos[instanceID];
                eatBrickToPos.Remove(instanceID);
                posToTimer[pos].Start(timeGenerate, pos);
                colorCanGenerate.Add(color);
            }
            eatBricks.Remove(eatBrick);
        }

        public void DeconstructRoom()
        {
            PrefabManager.Inst.PushToPool(ground, PrefabManager.GROUND, false);
            for(int i = 0; i < bridges.Count; i++)
            {
                Bridge bridgeScript = Cache.GetBridge(bridges[i]);
                bridgeScript.DeconstructBridge();
                PrefabManager.Inst.PushToPool(bridges[i], PrefabManager.BRIDGE, false);
            }
            for(int i = 0; i < eatBricks.Count; i++)
            {
                PrefabManager.Inst.PushToPool(eatBricks[i], PrefabManager.EAT_BRICK, false);
            }

            foreach(var i in posToTimer)
            {
                i.Value.Stop();
            }
        }
        private void InitializeBridge()
        {
            GetBridgePosition();
            for(int i = 0; i < entrance1.Count; i++)
            {
                GameObject bridge = PrefabManager.Inst.PopFromPool(PrefabManager.BRIDGE);
                bridge.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                bridge.transform.localPosition = entrance1[i];
                entrance1[i] = bridge.transform.position + Vector3.back;

                Bridge bridgeScript = Cache.GetBridge(bridge);
                bridgeScript.ConstructBridge();
                bridges.Add(bridge);

                if (addNextRoomPos == default)
                {
                    Vector3 temp = GameConst.BRIDGE_BRICK_SIZE;
                    temp.x = 0;
                    addNextRoomPos = temp * bridgeScript.NumOfBrick;
                }
                
                entrance2.Add(entrance1[i] + addNextRoomPos + Vector3.forward);
            }
            
        }
        private void GetBridgePosition()
        {
            if(numOfBridge == -1)
            {
                numOfBridge = GameplayManager.Inst.NumOfPlayer;
            }
            Vector3 add = new Vector3(0, (SIZE_Y_ROOM - GameConst.BRIDGE_BRICK_SIZE.y) / 2, GameConst.BRIDGE_BRICK_SIZE.z / 2);
            float value = roomSize.x * 2/ (numOfBridge + 1);
            float index = -roomSize.x + value;

            for(int i = 0; i < numOfBridge; i++)
            {
                Vector3 v = new Vector3(roomPos.x + index, roomPos.y, roomPos.z + roomSize.y) + add;
                entrance1.Add(v);

                index += value;
            }
        }
        //private void InitializeEatBricks()
        //{
        //    InitializeEatBricksData();            
        //}

        private Vector2Int GetBrickPosition()
        {
            int posindex = Random.Range(0, posCanGenerate.Count);
            Vector2Int pos = posCanGenerate[posindex];
            posCanGenerate.Remove(pos);
            return pos;
        }

        private BrickColor GetBrickColor()
        {
            int colorIndex = Random.Range(0, colorCanGenerate.Count);
            BrickColor color = colorCanGenerate[colorIndex];
            colorCanGenerate.Remove(color);
            return color;
        }

        private void InitializeEatBricksData()
        {
            xCount = Mathf.RoundToInt(roomSize.x * 2 / EAT_BRICK_DISTANCE.x) + 1 - 2 * MARGIN;
            zCount = Mathf.RoundToInt(roomSize.y * 2 / EAT_BRICK_DISTANCE.z) + 1 - 2 * MARGIN;
            maxNumberColorBrick = (xCount * zCount) / GameplayManager.Inst.NumOfPlayer;

            for(int z = 0; z < zCount; z++)
            {
                for(int x = 0; x < xCount; x++)
                {
                    //NOTE: Save essential data  
                    Vector2Int pos = new Vector2Int(x, z);
                    posCanGenerate.Add(pos);
                    STimer timer = new STimer();
                    timer.TimeOutVector2Int += UpdateEventTimer;
                    posToTimer.Add(pos, timer);
                  
                }   
            }
            //for(int i = 0; i < GameplayManager.Inst.PlayerColors.Count; i++)
            //{
            //    if(i == GameplayManager.Inst.PlayerColors.Count - 1)
            //    {
            //        for (int j = 0; j < xCount * zCount - maxNumberColorBrick * i; j++)
            //        {
            //            BrickColor color = GameplayManager.Inst.PlayerColors[i];
            //            colorCanGenerate.Add(color);
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 0; j < maxNumberColorBrick; j++)
            //        {
            //            BrickColor color = GameplayManager.Inst.PlayerColors[i];
            //            colorCanGenerate.Add(color);
            //        }
            //    }
                
            //}
            
        }
        private Vector3 GetEatBrickPosition(Vector2Int index)
        {
            float xValue = -roomSize.x + EAT_BRICK_DISTANCE.x * MARGIN;
            float zValue = -roomSize.y + EAT_BRICK_DISTANCE.z * MARGIN;

            xValue += index.x * EAT_BRICK_DISTANCE.x;
            zValue += index.y * EAT_BRICK_DISTANCE.z;
            return new Vector3(roomPos.x + xValue, roomPos.y + EAT_BRICK_HEIGHT,roomPos.z + zValue);
        }
        
        private void UpdateEventTimer(Vector2Int pos)
        {
            if (!LevelManager.Inst.CurrentLevel.IsEndLevel)
            {
                GameObject EatBrick = PrefabManager.Inst.PopFromPool(PrefabManager.EAT_BRICK);
                EatBrick.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                EatBrick.transform.localPosition = GetEatBrickPosition(pos);
                eatBricks.Add(EatBrick);

                BrickColor color = GetBrickColor();
                Cache.GetEatBrick(EatBrick).ChangeColor(color);

                eatBrickToPos.Add(EatBrick.GetInstanceID(), pos);
            }
                      
        }


        ~AddRoom()
        {
            foreach(var timer in posToTimer)
            {
                timer.Value.TimeOutVector2Int -= UpdateEventTimer;
                MotherTimer.Inst.Push(timer.Value);
            }
        }
    }
}