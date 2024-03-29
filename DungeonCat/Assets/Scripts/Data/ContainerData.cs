﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Scripts.Definitions;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Data
{
    /// <summary>
    /// Stored data for a container or collection of items that will be attached to an entity
    /// </summary>
    [Serializable]
    public class ContainerData : Data
    {
        public int slots;

        public ItemData[] items;

        [JsonIgnore]
        public bool isDirty;

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
                isDirty = true;
                return true;
            }

            if (items[slot].id == item.id && items[slot].count + item.count <= item.GetItemDef().StackSize)
            {
                items[slot].count += item.count;
                isDirty = true;
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

            isDirty = true;

            return item;
        }

        /// <summary>
        /// Clear all items from the container
        /// </summary>
        public void Clear()
        {
            items = new ItemData[slots];
            isDirty = true;
        }


        /// <summary>
        /// Drops all items in the cats inventory to the ground
        /// </summary>
        public void DropAllItems(Vector2 pos)
        {
            foreach (var itemData in items)
            {
                if (itemData.IsEmpty()) continue;

                DropItem(itemData, pos);
            }

            Clear();
        }

        /// <summary>
        /// Drops an item to the ground in front of the cat
        /// </summary>
        public void DropItem(ItemData itemData, Vector2 pos)
        {
            GameStateManager.CreateEntity(new ItemEntityData
            {
                item = itemData,
                id = itemData.id + EntityData.idCounter++,
                scene = GameStateManager.CurrentState.currentScene,
                position = pos
            });
        }

        /// <summary>
        /// Drops the item at the particular slot in the cat's inventory to the ground
        /// </summary>
        public void DropItem(int slot, Vector2 pos)
        {
            DropItem(ClearSlot(slot), pos);
        }

        /// <summary>
        /// Whether this inventory contains at least a certain amount of a specific ItemDef
        /// </summary>
        /// <param name="amount">Minimum amount needed</param>
        /// <typeparam name="T">The ItemDef</typeparam>
        /// <returns>Whether at least that many are contained</returns>
        public bool Contains<T>(int amount = 1) where T : ItemDef => items.Where(item => item.Is<T>()).Sum(data => data.count) >= amount;

        /// <summary>
        /// Returns the first slot that contains the specified ItemDef
        /// </summary>
        /// <typeparam name="T">The ItemDef</typeparam>
        /// <returns>The first index containing it, or -1 if it doesn't</returns>
        public int Find<T>() where T : ItemDef
        {
            for (var i = 0; i < items.Length; i++)
            {
                if (items[i].Is<T>()) return i;
            }
            
            return -1;
        }
    }
}