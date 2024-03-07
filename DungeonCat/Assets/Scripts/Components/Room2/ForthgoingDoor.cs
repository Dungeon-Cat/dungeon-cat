using Scripts.Components.CommonEntities;
using Scripts.Data;
using Scripts.Definitions.Items;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class ForthgoingDoor : MonoBehaviour, IInteractable
    {
        public DoorEntity door;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TiresiasStatue.Instance.dialogueTriggered)
            {
                if (!door.IsOpen)
                {
                    door.SetOpen(true);
                }
            }
        }
        
        public bool CanBeInteractedWith() => !door.IsOpen && TiresiasStatue.Instance.dialogueTriggered;

        public void Interact()
        {
            door.SetOpen(true);
        }
    }
}