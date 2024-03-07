using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class ForthgoingDoor : MonoBehaviour, IInteractable
    {
        public DoorEntity door;
        
        public bool CanBeInteractedWith() => !door.IsOpen && TiresiasStatue.DialogueTriggered;

        public void Interact()
        {
            door.SetOpen(true);
        }
    }
}