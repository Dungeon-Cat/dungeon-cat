using System;
using Scripts.Utility;

namespace Scripts.Data
{
    /// <summary>
    /// The stored data for the player controlled character
    /// </summary>
    [Serializable]
    public class CatData : CharacterData
    {
        public const int MaxInventorySlots = 10;

        public ContainerData inventory = new(MaxInventorySlots);

        public bool TryPickupItem(ItemData item)
        {
            if (inventory.TryAdd(item))
            {
                GameStateManager.onItemPickedUp?.Invoke(this, item);
                item.GetItemDef().OnPickup(this);
                return true;
            }

            return false;
        }
        
        public bool TryPickupItem(ItemEntityData item)
        {
            if (!TryPickupItem(item.item)) return false;
            
            GameStateManager.RemoveEntity(item);
            return true;
        }

        public void DropAllItems()
        {
            foreach (var itemData in inventory.items)
            {
                if (itemData.IsEmpty()) continue;

                GameStateManager.CreateEntity(new ItemEntityData
                {
                    item = itemData,
                    id = itemData.id + idCounter++,
                    scene = GameStateManager.CurrentScene,
                    position = position + facing * 10
                });
            }

            inventory.Clear();
        }
    }
}