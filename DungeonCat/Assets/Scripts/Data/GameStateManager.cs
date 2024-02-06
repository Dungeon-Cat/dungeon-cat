using Scripts.Definitions;
using Scripts.Utility;

namespace Scripts.Data
{
    public static class GameStateManager
    {
        public static GameState CurrentState { get; private set; }

        /// <summary>
        /// Initializes the GameState
        /// </summary>
        /// <param name="initialCatData">Existing data for the cat, if any</param>
        public static void Init(CatData initialCatData = null)
        {
            CurrentState = new GameState
            {
                cat = initialCatData ??= new CatData()
            };
            
            ItemRegistry.Initialize();
        }

        public static void Register(EntityData entity)
        {
        }
        
    }
}