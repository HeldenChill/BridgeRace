using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.Core.Character.WorldInterfaceSystem
{
    using System;
    using Utilitys;
    public class CheckCharacterSensor : BaseSensor
    {
        [SerializeField]
        Transform positionCheck;
        [SerializeField]
        float height;
        [SerializeField]
        float radius;
        [SerializeField]
        LayerMask layer;
        [SerializeField]
        Collider parentCollider;

        private List<AbstractCharacter> charactersList = new List<AbstractCharacter>();
        private Collider[] charactersTemp = new Collider[4];

        private Queue<Collider> oldCharacters = new Queue<Collider>(); //NOTE: OnTriggerEnter,but can filter by layer first
        public override void UpdateData()
        {
            Array.Clear(charactersTemp, 0, charactersTemp.Length);
            Physics.OverlapCapsuleNonAlloc(positionCheck.position + Vector3.up * height / 2, positionCheck.position - Vector3.up * height / 2, radius, charactersTemp, layer);
            ColliderCheck(charactersTemp);
            Data.Characters = charactersList;
        }

        private void ColliderCheck(Collider[] characters)
        {
            charactersList.Clear();
            int oldCount = oldCharacters.Count;
            for(int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == null || characters[i] == parentCollider)
                    continue;
                if (!oldCharacters.Contains(characters[i]))
                {
                    AbstractCharacter character = Cache.GetCharacter(characters[i]);
                    charactersList.Add(character);
                    //Debug.Log("WorldInterface: " + characters[i].gameObject.name);
                }
                oldCharacters.Enqueue(characters[i]);                               
            }

            for (int i = 0; i < oldCount; i++)
            {
                oldCharacters.Dequeue();
            }
        }

        private void OnDrawGizmos()
        {
            if (positionCheck != null)
            {
                Gizmos.DrawSphere(positionCheck.position + Vector3.up * height / 2, radius);
                Gizmos.DrawSphere(positionCheck.position - Vector3.up * height / 2, radius);
            }
        }
    }
}