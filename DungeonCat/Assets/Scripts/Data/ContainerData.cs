using System;
using System.Linq;
using Newtonsoft.Json;
using Scripts.Utility;
namespace Scripts.Data
{
    /// <summary>
    /// Stored data for a container or collection of items that will be attached to an entity
    /// </summary>
    [Serializable]
    public class ContainerData
    {
        public int slots;

        public ItemData[] items;

        [JsonIgnore]
        public int TotalCount => items.Where(item => !item.IsEmpty()).Sum(item => item.count);

        [JsonIgnore]
        public int FreeSlots => items.Count(item => item.IsEmpty());

        [JsonIgnore]
        public int UsedSlots => items.Count(item => !item.IsEmpty());

        public ContainerData(int slots)
        {
            this.slots = slots;
            items = new ItemData[slots];
        }

        /// <summary>
        /// Tries to add an item to a specific slot of this container
        /// </summary>
        /// <param name="item">item to add</param>
        /// <param name="slot">slot index</param>
        /// <returns>whether the item was successfully added</returns>
        public bool TryAddToSlot(ItemData item, int slot)
        {
            if (slot >= slots || slot < 0) return false;

            if (items[slot].IsEmpty())
            {
                items[slot] = item;
                return true;
            }

            if (items[slot].id == item.id && items[slot].count + item.count <= item.GetItemDef().StackSize)
            {
                items[slot].count += item.count;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to add an Item to this container at the first available slot
        /// </summary>
        /// <param name="item">item to add</param>
        /// <returns>whether the item was successfully added</returns>
        public bool TryAdd(ItemData item)
        {
            for (var i = 0; i < slots; i++)
            {
                if (TryAddToSlot(item, i))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clears a specific slot in the container
        /// </summary>
        /// <param name="slot">the slot index to clear</param>
        /// <returns>The item that was in the slot, possibly null</returns>
        public ItemData ClearSlot(int slot)
        {
            if (slot >= slots || slot < 0) return null;

            var item = items[slot];

            items[slot] = null;

            return item;
        }

        /// <summary>
        /// Clear all items from the container
        /// </summary>
        public void Clear()
        {
            items = new ItemData[slots];
        }
    }
}