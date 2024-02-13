using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Scripts.Utility;

namespace Scripts.Definitions
{
    /// <summary>
    /// Handles registering and keeping track of the Item definitions and Item Combinations
    /// </summary>
    public static class ItemRegistry
    {
        private static bool initialized;

        private static readonly Dictionary<Type, ItemDef> ItemsByType = new();
        public static readonly Dictionary<string, ItemDef> Items = new();
        public static readonly List<ItemCombination> ItemCombinations = new();

        public static T Instance<T>() where T : ItemDef => ItemsByType.GetValueOrDefault(typeof(T)) as T;

        public static void Initialize()
        {
            if (initialized) return;

            initialized = true;

            var assembly = Assembly.GetExecutingAssembly();

            var items = assembly.GetTypes()
                .Where(type => type.IsAssignableTo(typeof(ItemDef)) && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<ItemDef>();

            foreach (var item in items)
            {
                ItemsByType.Add(item.GetType(), item);
                Items.Add(item.Id, item);

                ItemCombinations.AddRange(item.AddCombinations());
            }

            foreach (var item in ItemCombinations.SelectMany(itemCombination => itemCombination.items.Keys))
            {
                Items[item].isMaterial = true;
            }
        }
    }
}