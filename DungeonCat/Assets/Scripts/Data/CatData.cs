using System;
using System.Linq;
using Scripts.Definitions;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Data
{
    /// <summary>
    /// The stored data for the player controlled character
    /// </summary>
    [Serializable]
    public class CatData : CharacterData
    {
        public const int MaxInventorySlots = 10;
        public const float Reach = 10f;

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

        /// <summary>
        /// Tries to make the cat pick up an item entity from the ground to its inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryPickupItem(ItemEntityData item)
        {
            if (!TryPickupItem(item.item)) return false;

            GameStateManager.RemoveEntity(item);
            return true;
        }

        private Vector2 ItemDropPos(Vector2? desiredPos = null)
        {
            var direction = desiredPos.HasValue ? (desiredPos.Value - position).normalized : facing;
            var pos = position + direction * Reach;

            return pos;
        }

        /// <summary>
        /// Drops all items in the cats inventory to the ground
        /// </summary>
        public void DropAllItems()
        {
            inventory.DropAllItems(ItemDropPos());
        }

        /// <summary>
        /// Drops an item to the ground in front of the cat
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="desiredPos"></param>
        public void DropItem(ItemData itemData, Vector2? desiredPos = null)
        {
            inventory.DropItem(itemData, ItemDropPos(desiredPos));
        }

        /// <summary>
        /// Drops the item at the particular slot in the cat's inventory to the ground
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="desiredPos"></param>
        public void DropItem(int slot, Vector2? desiredPos = null)
        {
            inventory.DropItem(slot, ItemDropPos(desiredPos));
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