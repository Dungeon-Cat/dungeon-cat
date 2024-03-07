using System.Transactions;
using Scripts.Components.CommonEntities;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.Room3
{
    public class Switch : OpenableEntityComponent<OpenableEntityData>, IInteractable
    {
        public Room3DungeonLevel dungeonLevel;
        
        public bool CanBeInteractedWith()
        {
            return true;
        }
        
        public void Interact()
        {
            dungeonLevel.OnSwitchChange();
        }
    }
}