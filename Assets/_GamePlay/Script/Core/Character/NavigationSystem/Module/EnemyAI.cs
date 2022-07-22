using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.NavigationSystem
{
    public class EnemyAI : AbstractNavigationModule
    {
        bool goToBrick = false;
        bool isHaveDestination = false;
        Vector3 nextDestination;
        public override void UpdateData()
        {
            CheckDestination();
            GetEatBrickDestination();
        }

        void GetEatBrickDestination()
        {
            if (!goToBrick && Parameter.EatBricks.Count > 0)
            {
                int index = Random.Range(0, Parameter.EatBricks.Count);
                nextDestination = Parameter.EatBricks[index].gameObject.transform.position;
                goToBrick = true;
                isHaveDestination = true;
                Debug.Log("Destination: "+ nextDestination);
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
                Debug.Log(dir);
                if (dir.sqrMagnitude < 0.01f) //NOTE: When Reach Destination
                {
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
            Data.MoveDirection = Vector3.zero;
            isHaveDestination = false;
        }

        void Wandering()
        {

        }
    }
}