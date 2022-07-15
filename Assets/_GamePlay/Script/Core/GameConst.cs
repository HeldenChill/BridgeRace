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
    }
}