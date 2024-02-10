using System.Collections.Generic;
using Scripts.Definitions;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Data
{
    public static class GameStateManager
    {
        public static GameState CurrentState { get; private set; }

        public static string CurrentScene { get; private set; } = "Root";

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
                    {"Root", new SceneData()}
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

        public static void CreateEntity(EntityData entity)
        {
            Register(entity);
            onEntityCreated?.Invoke(entity);
        }

        #region Events

        public static EventHandler<CatData, ItemData> onItemPickedUp;
        public static EventHandler<EntityData> onEntityCreated;

        #endregion
    }
}