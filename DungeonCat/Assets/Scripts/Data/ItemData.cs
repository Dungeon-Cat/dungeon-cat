using System;
using System.Collections.Generic;
using Scripts.Definitions;
using Scripts.Definitions.Items;

namespace Scripts.Data
{
    /// <summary>
    /// Saved state of an item being stored in a container / inventory
    /// </summary>
    [Serializable]
    public class ItemData
    {
        /// <summary>
        /// The id of the Item definition this corresponds to
        /// <br/>
        /// <seealso cref="ItemDef.Id"/>
        /// </summary>
        public string id;

        /// <summary>
        /// How many of the item are being stored
        /// </summary>
        public ushort count;

        /// <summary>
        /// Create a new ItemData for the given ItemDef
        /// </summary>
        /// <param name="count">How many of the item to create, default 1</param>
        /// <typeparam name="T">The <see cref="ItemDef"/></typeparam>
        /// <returns>new ItemData</returns>
        public static ItemData Create<T>(ushort count = 1) where T : ItemDef => new()
        {
            id = ItemRegistry.Instance<T>().Id,
            count = count
        };

        public ItemDef GetItemDef()
        {
            // TODO replace null with NullItem (will have to have a static reference in ItemRegistry?)
            return ItemRegistry.Items.GetValueOrDefault(id, ItemRegistry.Instance<NullItem>());
        }
    }
}