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

        bool goToBrick = false;
        bool goToEntrance = false;
        bool isHaveDestination = false;
        bool isReachEntrance = false;
        Vector3 nextDestination;
        
        public override void UpdateData()
        {
            if (!isReachEntrance)
            {
                if (Parameter.CharacterData.Bricks.Count > 10)
                {
                    GoToEntrance();
                }
                else
                {
                    if (!goToEntrance)
                    {
                        GetEatBrickDestination();
                    }
                }
                CheckDestination();
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
                            nextDestination = Parameter.EatBricks[i].gameObject.transform.position;
                            isHaveDestination = true;
                            break;
                        }
                    }
                }

                if (!isHaveDestination)
                {
                    int index = Random.Range(0, Parameter.EatBricks.Count);
                    nextDestination = Parameter.EatBricks[index].gameObject.transform.position;
                    isHaveDestination = true;
                }
                
                goToBrick = true;
                
                //Debug.Log("Destination: "+ nextDestination);
            }
            
        }
        void GoToEntrance()
        {
            if (!goToEntrance)
            {
                List<Vector3> entrances = LevelManager.Inst.CurrentLevel.GetCurrentRoom(Parameter.PlayerInstanceID).Entrance1;
                int index = Random.Range(0, entrances.Count);

                nextDestination = entrances[index];
                //Debug.Log(nextDestination);
                goToEntrance = true;

                isHaveDestination = true;
            }
            
        }

        void CheckPlayer()
        {

        }

        void CheckDestination()
        {
            if (isHaveDestination)
            {
                Vector3 dir = nextDestination - Parameter.Player.position;
                dir.y = 0;
                if (dir.sqrMagnitude < 0.01f) //NOTE: When Reach Destination
                {
                    //NOTE: Check reach destination case
                    if (goToEntrance)
                    {
                        Debug.Log("Go To Entrance");
                        Data.MoveDirection = Vector2.zero;
                        isReachEntrance = true;
                    }
                    else if (goToBrick)
                    {
                        Debug.Log("Go To Brick");
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
            goToEntrance = false;
            Data.MoveDirection = Vector3.zero;
            isHaveDestination = false;
        }

        void Wandering()
        {

        }
    }
}