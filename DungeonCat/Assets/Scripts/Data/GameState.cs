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
        public const string RootScene = "Root";
        public const string DefaultRoom = "Room1";

        public CatData cat;

        public string currentScene = DefaultRoom;

        public Dictionary<string, SceneData> scenes;

        [JsonIgnore]
        public SceneData CurrentScene => scenes[currentScene];

        public static GameState Create(CatData initialCatData) => new()
        {
            cat = initialCatData ?? new CatData(),
            scenes = new Dictionary<string, SceneData>
            {
                {RootScene, new SceneData()},
                {DefaultRoom, new SceneData()},
            }
        };
    }
}