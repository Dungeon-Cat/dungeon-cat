using System;
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
        /// What scene this Entity is currently in
        /// </summary>
        public string scene;
        
        /// <summary>
        /// Current Y position of this entity within the scene
        /// </summary>
        public Vector2 position;
    }
}