using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    /// <summary>
    /// An entity is any object within the game that data that needs to be saved
    /// </summary>
    [Serializable]
    public class EntityData : Data
    {
        /// <summary>
        /// Unique identifier for this entity
        /// </summary>
        public string id;

        /// <summary>
        /// Whether this entity has been destroyed and should no longer be active in the scene
        /// </summary>
        public bool destroyed;

        /// <summary>
        /// What scene this Entity is currently in
        /// </summary>
        public string scene;

        /// <summary>
        /// Current Y position of this entity within the scene
        /// </summary>
        public Vector2 position;

        public bool isDefaultInScene;
        
        public List<string> tags = new();

        public static int idCounter;

        public void AddTag(string tag)
        {
            tags.Add(tag);
            GameStateManager.onTagAdded?.Invoke(this, tag);
        }

        public void RemoveTag(string tag)
        {
            tags.Remove(tag);
            GameStateManager.onTagRemoved?.Invoke(this, tag);
        }
    }
}