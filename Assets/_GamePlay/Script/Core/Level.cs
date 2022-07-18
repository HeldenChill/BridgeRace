using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core
{
    public class Level : MonoBehaviour
    {
        public Transform StaticEnvironment;
        AddRoom room;
        private void Start()
        {
            room = new AddRoom(Vector3.zero, Vector2.one * 5);
            room.ConstructRoom();
        }
    }
}