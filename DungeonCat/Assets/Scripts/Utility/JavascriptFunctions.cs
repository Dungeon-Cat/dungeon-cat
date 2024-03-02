using System.Runtime.InteropServices;
using UnityEngine;
namespace Scripts.Utility
{
    public static class JavascriptFunctions
    {
        public static bool IsBrowser => Application.platform == RuntimePlatform.WebGLPlayer;

        [DllImport("__Internal")]
        public static extern void Alert(string str);

        [DllImport("__Internal")]
        public static extern void Save(string saveJson);
        
        [DllImport("__Internal")]
        public static extern string Load();
    }
}