using System;
namespace Scripts.Utility
{
    public static class Extensions
    {
        public static bool IsAssignableTo(this Type type, Type other) => other.IsAssignableFrom(type);
        
        
    }
}