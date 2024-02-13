using System;
using System.Linq;
using Scripts.Definitions;
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

                DropItem(itemData);
            }

            inventory.Clear();
        }

        public void DropItem(ItemData itemData)
        {
            GameStateManager.CreateEntity(new ItemEntityData
            {
                item = itemData,
                id = itemData.id + idCounter++,
                scene = GameStateManager.CurrentScene,
                position = position + facing * 10
            });
        }

        /// <summary>
        /// Tries to combine the items in the listed slots
        /// </summary>
        /// <param name="slotIds"></param>
        public bool TryCombine(params int[] slotIds)
        {
            var itemDatas = slotIds.Select(i => inventory.items[i]).ToArray();

            if (itemDatas.Any(data => data.IsEmpty())) return false;

            var itemsToCombine = itemDatas.GroupBy(data => data.id).ToDictionary(group => group.Key, group => group.Sum(data => data.count));

            var combination = ItemRegistry.ItemCombinations.FirstOrDefault(combo =>
                combo.items.Count == itemsToCombine.Count &&
                combo.items.All(i => itemsToCombine.TryGetValue(i.Key, out var count) && count >= i.Value)
            );

            if (combination == null) return false;

            var result = new ItemData
            {
                id = combination.result,
                count = combination.amount
            };
            
            foreach (var (itemId, count) in itemsToCombine)
            {
                for (var i = 0; i < count; i++)
                {
                    itemDatas.First(data => !data.IsEmpty() && data.id == itemId).count--;
                }
            }

            if (!inventory.TryAdd(result))
            {
                DropItem(result);
            }

            return true;
        }
    }
}