using Scripts.Components.CommonEntities;
using Scripts.Data;

namespace Scripts.Components.Room3
{
    /// <summary>
    /// Switch in Room 3.
    /// When interacted with, changes global open state for switches in Room 3
    /// </summary>
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