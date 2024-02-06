using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Scripts.Utility;

namespace Scripts.Definitions
{
    public static class ItemRegistry
    {
        private static readonly Dictionary<Type, ItemDef> ItemsByType = new();
        public static readonly Dictionary<string, ItemDef> Items = new();
        
        public static T Instance<T>() where T : ItemDef => ItemsByType.GetValueOrDefault(typeof(T)) as T;

        public static void Initialize()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var items = assembly.GetTypes()
                .Where(type => type.IsAssignableTo(typeof(ItemDef)) && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<ItemDef>();

            foreach (var item in items)
            {
                ItemsByType.Add(item.GetType(), item);
                Items.Add(item.Id, item);
            }
        }
    }
}