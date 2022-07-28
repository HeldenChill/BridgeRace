using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.NavigationSystem
{
    using BridgeRace.Manager;
    using Utilitys;
    public class EnemyAI : AbstractNavigationModule
    {
        public const float EAT_BRICK_PRECISION = 20;

        private void Start()
        {
            LevelManager.Inst.CurrentLevel.OnStart += OnStartLevel;
        }

        private void OnDisable()
        {
            LevelManager.Inst.CurrentLevel.OnStart -= OnStartLevel;
        }

        [SerializeField]
        int numBrickToBridge = 10;
        bool goToBrick = false;
        bool goToEntrance1 = false;
        bool goToEntrance2 = false;
        bool isHaveDestination = false;
        //bool isReachEntrance = false;
        bool isOnBridge = false;
        Vector3 nextDestination;
        Vector3 NextDestination
        {
            get => nextDestination;
            set
            {
                nextDestination = value;
                isHaveDestination = true;
            }
        }
        Vector3 entrance1;
        Vector3 entrance2;
        
        public override void UpdateData()
        {
            if (isOnBridge) //NOTE: When in bridge
            {
                OnBridge();
            }
            else  // NOTE: When in add room situation
            {
                if (Parameter.CharacterData.Bricks.Count > numBrickToBridge) //NOTE: Collect enough bricks
                {
                    GoToEntrance1();
                }
                else //NOTE: Not collect enough bricks
                {
                    if (!goToEntrance1)
                    {
                        GetEatBrickDestination();
                    }
                }               
            }
            CheckDestination();

        }
        void OnBridge()
        {
            if (!goToEntrance2) //NOTE: When have bricks
            {
                goToEntrance2 = true;
                NextDestination = entrance2;
            }
            else
            {
                if (Parameter.CharacterData.Bricks.Count == 0) //NOTE: When run out of bricks
                {
                    goToEntrance1 = false;
                    NextDestination = entrance1;
                }
            }

        }
        void GetEatBrickDestination()
        {
            if (!goToBrick && Parameter.EatBricks.Count > 0)
            {
                for(int i = 0; i < Parameter.EatBricks.Count; i++)
                {
                    if(Parameter.EatBricks[i].Color == Parameter.CharacterType)
                    {
                        if (MathHelper.GetRandomPercent(EAT_BRICK_PRECISION))
                        {
                            NextDestination = Parameter.EatBricks[i].gameObject.transform.position;
                            break;
                        }
                    }
                }

                if (!isHaveDestination)
                {
                    int index = Random.Range(0, Parameter.EatBricks.Count);
                    NextDestination = Parameter.EatBricks[index].gameObject.transform.position;
                }
                
                goToBrick = true;
                
                //Debug.Log("Destination: "+ nextDestination);
            }
            
        }
        void GoToEntrance1()
        {

            if (!goToEntrance1)
            {
                List<Vector3> entrances = LevelManager.Inst.CurrentLevel.GetCurrentRoom(Parameter.PlayerInstanceID).Entrance1;
                List<Vector3> entrances2 = LevelManager.Inst.CurrentLevel.GetCurrentRoom(Parameter.PlayerInstanceID).Entrance2;
                int index = Random.Range(0, entrances.Count);

                NextDestination = entrances[index];
                entrance1 = entrances[index];
                entrance2 = entrances2[index];
                //Debug.Log(nextDestination);
                goToEntrance1 = true;
            }
            
        }

        

        void CheckDestination()
        {
            //NOTE: Can Inprove performance by save dir;
            if (isHaveDestination)
            {
                Vector3 dir = NextDestination - Parameter.Player.position;
                dir.y = 0;
                if (dir.sqrMagnitude < 0.01f) //NOTE: When Reach Destination
                {
                    //NOTE: Check reach destination case
                    
                    if (goToEntrance2)
                    {
                        isOnBridge = false;                       
                        //Debug.Log("Go To Entrance2");
                    }
                    else if (goToEntrance1)
                    {
                        //Debug.Log("Go To Entrance1");      
                        isOnBridge = true;
                    }
                    else if (goToBrick)
                    {
                        //Debug.Log("Go To Brick");
                    }
                    ReachDestination();
                }
                else 
                {
                    Data.MoveDirection = dir.normalized;
                }
            }
        }

        void ReachDestination()
        {
            goToBrick = false;
            goToEntrance1 = false;
            goToEntrance2 = false;
            Data.MoveDirection = Vector3.zero;
            isHaveDestination = false;
        }

        void Wandering()
        {

        }
        void CheckPlayer()
        {

        }

        private void OnStartLevel()
        {
            goToBrick = false;
            goToEntrance1 = false;
            goToEntrance2 = false;
            isHaveDestination = false;
            isOnBridge = false;
        }
    }
}