using BridgeRace.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    public class CheckExitSensor : BaseSensor
    {
        [SerializeField]
        Transform checkExitPoint;
        [SerializeField]
        float distance;
        [SerializeField]
        LayerMask layer;
        
        Ray ray;
        //TODO: Need to reset when come to new level
        List<int> oldGround = new List<int>();
        private void Start()
        {
            LevelManager.Inst.CurrentLevel.OnStart += OnStartLevel;
        }

        private void OnDisable()
        {
            LevelManager.Inst.CurrentLevel.OnStart -= OnStartLevel;
        }
        public override void UpdateData()
        {
            RaycastHit hit;
            ray = new Ray(checkExitPoint.position, Vector3.down);
            if(Physics.Raycast(ray, out hit, distance, layer))
            {
                int id = hit.collider.GetInstanceID();
                if (!oldGround.Contains(id))
                {
                    Data.IsExitRoom = true;
                    oldGround.Add(id);
                }
                else
                {
                    Data.IsExitRoom = false;
                }
            }
            
        }

        private void OnStartLevel()
        {
            oldGround.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(checkExitPoint.position, checkExitPoint.position + Vector3.down * distance);
        }
    }
}
