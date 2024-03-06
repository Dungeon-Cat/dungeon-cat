using System;
using System.IO;
using Newtonsoft.Json;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Data
{
    public static class SaveLoadManager
    {
        private static string GetSaveName() => "latest";

        private static string GetSavePath() => Path.Combine(Application.persistentDataPath, GetSaveName() + ".json");

        public static bool HasSaveToLoad(out string text)
        {
            text = null;
            try
            {
                var path = GetSavePath();

                if (JavascriptFunctions.IsBrowser)
                {
                    text = JavascriptFunctions.Load();
                }
                else if (File.Exists(path))
                {
                    text = File.ReadAllText(path);
                }

                return !string.IsNullOrEmpty(text);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Save()
        {
            try
            {
                var state = GameStateManager.CurrentState;

                var output = JsonConvert.SerializeObject(state, JsonSettings.SerializerSettings);

                if (JavascriptFunctions.IsBrowser)
                {
                    JavascriptFunctions.Save(output);
                }
                else
                {
                    var path = GetSavePath();
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    File.WriteAllText(path, output);
                    Debug.Log($"Saved to {path}");
                }

            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void Load()
        {
            try
            {
                if (!HasSaveToLoad(out var text))
                {
                    GameStateManager.onLoadFailed?.Invoke();
                    return;
                }

                var state = JsonConvert.DeserializeObject<GameState>(text, JsonSettings.SerializerSettings);

                GameStateManager.LoadState(state);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}