namespace Scripts.Data
{
    public static class GameStateManager
    {
        public static GameState CurrentState { get; private set; }

        public static void Init()
        {
            CurrentState = new GameState();
        }

        public static void Register(EntityData entity)
        {
        }
        
    }
}