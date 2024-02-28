using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using Scripts.Definitions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// The reverse of <see cref="Type.IsAssignableFrom"/> that .NET Framework 4.8 doesn't have yet
        /// </summary>
        public static bool IsAssignableTo(this Type type, Type other) => other.IsAssignableFrom(type);

        /// <summary>
        /// Checks whether an item is null/empty
        /// </summary>
        /// <param name="item">the possibly empty ItemData</param>
        /// <returns>whether it's empty</returns>
        public static bool IsEmpty(this ItemData item) => item == null || string.IsNullOrEmpty(item.id) || item.count < 1;

        /// <summary>
        /// Checks whether an ItemData is non empty and matches a specific ItemDef
        /// </summary>
        /// <param name="item">The item data</param>
        /// <typeparam name="T">The ItemDef type</typeparam>
        /// <returns>whether it's nonempty and matches</returns>
        public static bool Is<T>(this ItemData item) where T : ItemDef => !item.IsEmpty() && item.id == ItemRegistry.Instance<T>().Id;

        public static bool HasComponent<T>(this GameObject gameObject) => gameObject.GetComponent<T>() != null;

        public static bool HasComponent<T>(this GameObject gameObject, out T component) => (component = gameObject.GetComponent<T>()) != null;

        public static TResult FirstNonNull<TSource, TResult>(this IEnumerable<TSource> sequence, Func<TSource, TResult> mapper) => sequence.Select(mapper).First(arg => arg != null);

        public static List<RaycastResult> RaycastAll(this EventSystem eventSystem, PointerEventData data)
        {
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(data, results);
            return results;
        }
    }
}

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Allows init only properties pre net5
    /// </summary>
    internal static class IsExternalInit
    {
    }
}