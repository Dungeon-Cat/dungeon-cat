using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Scripts.Data
{
    /// <summary>
    /// Serializable class that holds all of the current information about the game that needs to be saved
    /// </summary>
    [Serializable]
    public class GameState
    {
        public CatData cat;

        public string currentScene = "Root";

        public Dictionary<string, SceneData> scenes;

        [JsonIgnore]
        public SceneData CurrentScene => scenes[currentScene];
    }
}