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
        private bool isWinPlayer = false;
        STimer fallTimer = new STimer();

        private void OnEnable()
        {
            fallTimer.TimeOut1 += TimerUpdate;        
        }
        private void Start()
        {
            LevelManager.Inst.CurrentLevel.OnWin += OnWinLevel;
            LevelManager.Inst.CurrentLevel.OnStart += OnStartLevel;
        }

        private void OnDisable()
        {
            fallTimer.TimeOut1 -= TimerUpdate;
            LevelManager.Inst.CurrentLevel.OnWin -= OnWinLevel;
            LevelManager.Inst.CurrentLevel.OnStart -= OnStartLevel;
        }
        public override void UpdateData()
        {
            if (!GameManager.Inst.GameIsRun)
            {
                return;
            }

            if (LevelManager.Inst.CurrentLevel.IsEndLevel)
            {
                if (isWinPlayer)
                {
                    gameObject.transform.position = LevelManager.Inst.CurrentLevel.WinPosition.position;
                    Event.SetSmoothRotation(GameConst.SENSOR_ROT, Vector3.back);
                    Event.SetSmoothRotation(GameConst.MODEL_ROT, Vector3.back);
                }
                return;
            }

            if (isFall)
                return;

            CheckPlayer();            
            CheckExitRoom();
            disableMovement = !CollideBridgeBrickHandle() || (!Parameter.IsHaveGround && Parameter.IsGrounded);
            CollideEatBrickHandle();
            RotationHandle();
            if (!disableMovement)
            {              
                MovementHandle();
            }
            else
            {
                Event.SetVelocity(-Parameter.SensorTF.forward * 0.1f);
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
            Event.SetBool_Anim(AnimationModule.ANIM_FALL, true);
            //Debug.Log("FALL");
        }

        private void CheckExitRoom()
        {
            if (Parameter.IsExitRoom)
            {
                LevelManager.Inst.CurrentLevel.SetPlayerRoom(Parameter.PlayerInstanceID, Parameter.ContainBrick.position.y);
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
            if (!MathHelper.IsApproximately(newDirection, direction) && newDirection.sqrMagnitude > 0.01f)
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
            velocity.y = 0;
            Event.SetFloat_Anim(AnimationModule.ANIM_VELOCITY, velocity.sqrMagnitude);
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
                room.AteEatBrick(brick.gameObject, brick.Color);
                //Debug.Log("Character " + Parameter.EatBricks.Count + ": "+ brick.GetInstanceID());
            }
        }
        
        private void TimerUpdate(int code)
        {
            if(code == 0)
            {
                isFall = false;
                Event.SetBool_Anim(AnimationModule.ANIM_FALL, false);
            }
        }

        private void OnWinLevel(int playerInstanceID)
        {
            while(Data.CharacterData.Bricks.Count > 0)
            {
                EatBrick brick = Data.CharacterData.Bricks.Pop();
                brick.transform.rotation = Quaternion.Euler(Vector3.zero);
                PrefabManager.Inst.PushToPool(brick.gameObject, PrefabManager.EAT_BRICK, false);
            }
            if(Parameter.PlayerInstanceID == playerInstanceID)
            {
                Event.SetInt_Anim(AnimationModule.ANIM_RESULT, 2);
                isWinPlayer = true;
            }
            else
            {
                Event.SetInt_Anim(AnimationModule.ANIM_RESULT, 1);
            }
        }
        private void OnStartLevel()
        {
            //Debug.Log(gameObject.name + " Start level");
            isWinPlayer = false;
            Event.SetInt_Anim(AnimationModule.ANIM_RESULT, 0);
        }


    }
}