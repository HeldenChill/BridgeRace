using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BridgeRace.Core.Character.LogicSystem
{
    using BridgeRace.Core.Brick;
    using Utilitys;
    public class CharacterLogicModule : AbstractLogicModule
    {
        private Vector3 velocity;
        private Quaternion rotation;
        private Vector2 direction = Vector2.zero;
        private bool disableMovement = false;
        public override void UpdateData()
        {
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
                float angle = Vector2.SignedAngle(newDirection, Vector2.up);
                rotation = Quaternion.Euler(0, angle, 0);
                Event.SetRotation(GameConst.SENSOR_ROT,rotation);
                Event.SetRotation(GameConst.MODEL_ROT,rotation);
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
            else
            {
                velocity.y = Parameter.Velocity.y;
            }

            Event.SetVelocity(velocity);
        }

        public EatBrick GetBrick()
        {
            if (Data.Bricks.Count == 0)
            {
                return null;
            }
            else
            {
                EatBrick brick = (EatBrick)Data.Bricks.Pop();
                //TO DO: Push this brick to pool
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

                Data.Bricks.Push(brick);
                Vector3 pos = Vector3.zero;
                pos.y = (Data.Bricks.Count - 1) * GameConst.EAT_BRICK_HEIGHT;
                brick.gameObject.transform.parent = Parameter.ContainBrick;
                brick.gameObject.transform.localPosition = pos;
                brick.gameObject.transform.localRotation = Quaternion.identity;

                //Debug.Log("Character " + Parameter.EatBricks.Count + ": "+ brick.GetInstanceID());
            }
        }

    }
}