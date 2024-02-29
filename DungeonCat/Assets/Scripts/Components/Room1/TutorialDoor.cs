using Scripts.Components.CommonEntities;
using Scripts.Data;
using Scripts.Definitions.Items;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.Room1
{
    /// <summary>
    /// Controls the behavior of the door in the tutorial.
    /// Currently opens if key dropped on door.
    /// </summary>
    public class TutorialDoor : MonoBehaviour, IInteractable
    {
        public DoorEntity door;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.HasComponent(out ItemEntity itemEntity) && itemEntity.data.item.id == nameof(TutorialKey))
            {
                GameStateManager.RemoveEntity(itemEntity.data);
                if (!door.IsOpen)
                {
                    door.SetOpen(true);
                }
            }
        }
        
        public bool CanBeInteractedWith() => !door.IsOpen && UnityState.Instance.cat.data.inventory.Contains<TutorialKey>();

        public void Interact()
        {
            door.SetOpen(true);
            var cat = UnityState.Instance.cat.data;
            cat.inventory.ClearSlot(cat.inventory.Find<TutorialKey>());
        }
    }
}