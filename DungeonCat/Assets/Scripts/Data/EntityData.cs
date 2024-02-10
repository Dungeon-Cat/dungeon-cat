using System;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts.Data
{
    /// <summary>
    /// An entity is any object within the game that data that needs to be saved
    /// </summary>
    [Serializable]
    public class EntityData
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
        
        public List<string> tags = new();
        
        public static int idCounter;
    }
}