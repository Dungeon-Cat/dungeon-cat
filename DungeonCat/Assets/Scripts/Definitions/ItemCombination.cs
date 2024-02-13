using System.Collections.Generic;
using System.Linq;
namespace Scripts.Definitions
{
    /// <summary>
    /// A class storing a way that a set of items can be combined together to create a new item
    /// </summary>
    public class ItemCombination
    {
        public readonly string result;

        public readonly ushort amount;

        public readonly Dictionary<string, int> items;

        public ItemCombination(string result, ushort amount, params string[] items)
        {
            this.result = result;
            this.amount = amount;
            this.items = items.GroupBy(id => id).ToDictionary(group => group.Key, group => group.Count());
        }

        public ItemCombination(ItemDef result, ushort amount, params string[] items) : this(result.Id, amount, items)
        {
        }

    }
}