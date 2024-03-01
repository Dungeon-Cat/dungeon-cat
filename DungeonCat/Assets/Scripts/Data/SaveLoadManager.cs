using System.IO;
using Newtonsoft.Json;
using Scripts.Utility;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Data
{
    public static class SaveLoadManager
    {
        public static string GetSaveName() => "latest";

        public static string GetSavePath() => Path.Combine(Application.persistentDataPath, GetSaveName() + ".json");

        public static void Save()
        {
            if (JavascriptFunctions.IsBrowser)
            {
                JavascriptFunctions.Alert("Saving not yet supported on browser");
                return;
            }
            
            var state = GameStateManager.CurrentState;

            var output = JsonConvert.SerializeObject(state, JsonSettings.SerializerSettings);

            var path = GetSavePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, output);

            Debug.Log($"Saved to {path}");
        }

        public static void Load()
        {
            if (JavascriptFunctions.IsBrowser)
            {
                JavascriptFunctions.Alert("Loading not yet supported on browser");
                return;
            }
            
            var path = GetSavePath();

            var text = File.ReadAllText(path);

            var state = JsonConvert.DeserializeObject<GameState>(text, JsonSettings.SerializerSettings);

            var output = JsonConvert.SerializeObject(state, JsonSettings.SerializerSettings);
            Assert.AreEqual(text, output);

            GameStateManager.LoadState(state);
        }


    }
}