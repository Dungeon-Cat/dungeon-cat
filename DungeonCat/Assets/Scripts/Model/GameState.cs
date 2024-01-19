using System;
namespace Model
{
    /// <summary>
    /// Serializable class that holds all of the current information about the game that needs to be saved
    /// </summary>
    [Serializable]
    public class GameState
    {
        public Entity[] entities;
    }
}