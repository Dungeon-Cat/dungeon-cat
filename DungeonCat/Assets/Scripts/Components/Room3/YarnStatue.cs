using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room3
{
    public class YarnStatue : MonoBehaviour, IInteractable
    {
        public Room3DungeonLevel dungeonLevel;
        public bool CanBeInteractedWith()
        {
            return true;
        }

        public void Interact()
        {
            dungeonLevel.OnYarnStatueInteract();
        }
    }
}