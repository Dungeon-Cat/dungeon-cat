using System;
using Scripts.Data;
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
    }
}