using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public static class GameConst
    {
        public static readonly string CHARACTER_ROT = "CharacterRotation";
        public static readonly string SENSOR_ROT = "SensorRotation";
        public static readonly string MODEL_ROT = "ModelRotation";

        public const float EAT_BRICK_HEIGHT = 0.22f;
        public static readonly Vector3 BRIDGE_BRICK_SIZE = new Vector3(1, 0.3f, 0.5f);

        private static float crossLengthBridgeBrick = 0;
        public static float CROSS_LENGTH_BRIDGE_BRICK
        {
            get
            {
                if(crossLengthBridgeBrick < 0.001f)
                {
                    crossLengthBridgeBrick = new Vector2(BRIDGE_BRICK_SIZE.y, BRIDGE_BRICK_SIZE.z).magnitude;
                }
                return crossLengthBridgeBrick;
            }
        }

        public static readonly Quaternion BRIDGE_BRICK_ANGLE = Quaternion.Euler(-(Mathf.Atan(BRIDGE_BRICK_SIZE.y/BRIDGE_BRICK_SIZE.z) * Mathf.Rad2Deg), 0, 0);

            
    }
}