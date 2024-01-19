using System;
using System.Numerics;

namespace Model
{
    /// <summary>
    /// This is the base object that 
    /// </summary>
    [Serializable]
    public class Entity
    {
        /// <summary>
        /// What level this Entity is currently in
        /// </summary>
        public string level;

        /// <summary>
        /// Current X position of this entity within the level
        /// </summary>
        public float x;

        /// <summary>
        /// Current Y position of this entity within the level
        /// </summary>
        public float y;

        public Vector2 Position
        {
            get => new(x, y);
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
    }
}