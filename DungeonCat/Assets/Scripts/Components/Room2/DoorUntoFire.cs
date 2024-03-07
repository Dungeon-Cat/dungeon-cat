using System;
using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using Scripts.Data;
using Scripts.Definitions.Items;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class DoorUntoFire : MonoBehaviour, IInteractable
    {
        public DoorEntity door;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TiresiasStatue.DialogueTriggered)
            {
                if (!door.IsOpen)
                {
                    door.SetOpen(true);
                }
            }
        }
        
        public bool CanBeInteractedWith() => !door.IsOpen && TiresiasStatue.DialogueTriggered;

        public void Interact()
        {
            door.SetOpen(true);
        }
    }
}