using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using BridgeRace.Core.Brick;
    using BridgeRace.Manager;
    using Utilitys;
    using Utilitys.Timer;
    public class CharacterLogicModule : AbstractLogicModule
    {
        private Vector3 velocity;
        private Quaternion rotation;
        private Vector2 direction = Vector2.zero;
        private bool disableMovement = false;
        private bool isFall = false;
        STimer fallTimer = new STimer();

        private void OnEnable()
        {
            fallTimer.TimeOut1 += TimerUpdate;
        }
        public override void UpdateData()
        {
            if (isFall)
                return;

            CheckPlayer();            
            CheckExitRoom();
            disableMovement = !CollideBridgeBrickHandle();
            CollideEatBrickHandle();
            RotationHandle();
            if (!disableMovement)
            {              
                MovementHandle();
            }
            else
            {
                Event.SetVelocity(Vector3.back * 5f);
            }
        }
        public void CheckPlayer()
        {

            if (!Parameter.IsGrounded)
            {
                return;
            }

            if (Parameter.Characters != null)
            {               
                for (int i = 0; i < Parameter.Characters.Count; i++)
                {
                    int numOfBrick = Parameter.Characters[i].CharacterCollide();
                    if(numOfBrick > Data.CharacterData.Bricks.Count)
                    {
                        Fall();
                    }
                }
            }
        }

        private void Fall()
        {
            isFall = true;
            fallTimer.Start(Parameter.TimeFall, 0);
            while(Data.CharacterData.Bricks.Count > 0)
            {
                EatBrick brick = Data.CharacterData.Bricks.Pop();
                brick.ChangeColor(BrickColor.Gray);
                brick.transform.parent = LevelManager.Inst.CurrentLevel.StaticEnvironment;
                brick.BrickFall();
            }
            Debug.Log("FALL");
            //TODO: Fall Here
        }

        private void CheckExitRoom()
        {
            if (Parameter.IsExitRoom)
            {
                LevelManager.Inst.CurrentLevel.SetPlayerRoom(Parameter.PlayerInstanceID,Parameter.ContainBrick.position.y);
                AddRoom room = LevelManager.Inst.CurrentLevel.GetCurrentRoom(Parameter.PlayerInstanceID);               
                room?.AddColorAndBrick(Parameter.PlayerInstanceID, Parameter.CharacterType);
            }
        }
        private void CollideEatBrickHandle()
        {
            if(Parameter.ContainBrick != null)
            {
                if(Parameter.EatBricks != null)
                {
                    for(int i = 0; i < Parameter.EatBricks.Count; i++)
                    {
                        AddBrick(Parameter.EatBricks[i]);
                    }
                }
            }
        }
        private bool CollideBridgeBrickHandle()
        {
            if(Parameter.BridgeBrick != null)
            {
                if (Parameter.BridgeBrick.Color != Parameter.CharacterType)
                {
                    EatBrick brick = GetBrick();
                    if(Parameter.MoveDirection.z > 0 && brick == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (brick != null)
                        {
                            Parameter.BridgeBrick.ChangeColor(Parameter.CharacterType);
                            return true;
                        }
                    }
                }                                                                                        
            }
            return true;
            
        }

        private void RotationHandle()
        {
            Vector2 newDirection = new Vector2(Parameter.MoveDirection.x, Parameter.MoveDirection.z);
            if (!MathHelper.IsApproximately(newDirection, direction) && newDirection.sqrMagnitude > 0.001f)
            {
                float angle = Mathf.Atan2(newDirection.x, newDirection.y) * Mathf.Rad2Deg;
                    //Vector2.SignedAngle(newDirection, Vector2.up);              
                rotation = Quaternion.Euler(0, angle, 0);
                Event.SetSmoothRotation(GameConst.SENSOR_ROT,Parameter.MoveDirection);
                Event.SetSmoothRotation(GameConst.MODEL_ROT,Parameter.MoveDirection);
            }
        }
        private void MovementHandle()
        {                       
            if (Parameter.IsGrounded && Parameter.Velocity.y < 0)
            {
                velocity.y = -1f;
            }
            velocity = Parameter.MoveDirection * Parameter.Speed;

            if (Parameter.IsGrounded && Parameter.Jump && Parameter.Velocity.y < 0.01f)
            {
                velocity.y = Parameter.JumpVelocity;
            }
            else if(Parameter.IsGrounded)
            {
                velocity.y = -0.5f;
            }
            else
            {
                velocity.y = Parameter.Velocity.y;
            }

            Event.SetVelocity(velocity);
        }

        public EatBrick GetBrick()
        {
            if (Data.CharacterData.Bricks.Count == 0)
            {
                return null;
            }
            else
            {
                EatBrick brick = Data.CharacterData.Bricks.Pop();
                //TO DO: Push this brick to pool
                brick.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                PrefabManager.Inst.PushToPool(brick.gameObject, PrefabManager.EAT_BRICK);
                return brick;
            }
        }

        public void AddBrick(EatBrick brick)
        {
            if (brick.Color == Parameter.CharacterType || brick.Color == BrickColor.Gray)
            {
                if (brick.Color == BrickColor.Gray)
                {
                    brick.ChangeColor(Parameter.CharacterType);
                }

                //if (Data.CharacterData.Bricks.Contains(brick))
                //    return;

                Data.CharacterData.Bricks.Push(brick);
                Vector3 pos = Vector3.zero;
                pos.y = (Data.CharacterData.Bricks.Count - 1) * GameConst.EAT_BRICK_HEIGHT;
                brick.SetActivePhysic(false);
                brick.gameObject.transform.parent = Parameter.ContainBrick;
                brick.gameObject.transform.localPosition = pos;
                brick.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                

                AddRoom room = LevelManager.Inst.CurrentLevel.GetCurrentRoom(Parameter.PlayerInstanceID);
                room.AteEatBrick(brick.gameObject.GetInstanceID(), brick.Color);
                //Debug.Log("Character " + Parameter.EatBricks.Count + ": "+ brick.GetInstanceID());
            }
        }
        
        private void TimerUpdate(int code)
        {
            if(code == 0)
            {
                isFall = false;
            }
        }

        private void OnDisable()
        {
            fallTimer.TimeOut1 -= TimerUpdate;
        }

    }
}