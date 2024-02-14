using System.Collections.Generic;
using Scripts.Definitions;
using Scripts.Utility;

namespace Scripts.Data
{
    public static class GameStateManager
    {
        public static GameState CurrentState { get; private set; }

        public static string CurrentScene { get; private set; } = GameState.DefaultRoom;

        /// <summary>
        /// Initializes the GameState
        /// </summary>
        /// <param name="initialCatData">Existing data for the cat, if any</param>
        public static void Init(CatData initialCatData = null)
        {
            CurrentState = new GameState
            {
                cat = initialCatData ?? new CatData(),
                scenes = new Dictionary<string, SceneData>
                {
                    {"Root", new SceneData()},
                    {GameState.DefaultRoom, new SceneData()}
                }
            };

            ItemRegistry.Initialize();
        }

        /// <summary>
        /// Ensures that the Data for an entity is properly registered within its scene
        /// </summary>
        /// <param name="entity"></param>
        public static void Register(EntityData entity)
        {
            var scene = entity.scene;

            if (!CurrentState.scenes.TryGetValue(scene, out var sceneData))
            {
                throw new KeyNotFoundException($"Scene {scene} was not yet registered in the game state");
            }

            sceneData.entities.TryAdd(entity.id, entity);
        }
        
        /// <summary>
        /// Ensures that the Data for an entity is properly registered within its scene
        /// </summary>
        /// <param name="entity"></param>
        public static void Unregister(EntityData entity)
        {
            var scene = entity.scene;

            if (!CurrentState.scenes.TryGetValue(scene, out var sceneData))
            {
                throw new KeyNotFoundException($"Scene {scene} was not yet registered in the game state");
            }

            sceneData.entities.Remove(entity.id);
        }

        public static void CreateEntity(EntityData entity)
        {
            Register(entity);
            onEntityCreated?.Invoke(entity);
        }

        public static void RemoveEntity(EntityData entity)
        {
            Unregister(entity);
            onEntityDestroyed?.Invoke(entity);
        }

        #region Events

        public static EventHandler<CatData, ItemData> onItemPickedUp;
        public static EventHandler<EntityData> onEntityCreated;
        public static EventHandler<EntityData> onEntityDestroyed;

        #endregion
    }
}