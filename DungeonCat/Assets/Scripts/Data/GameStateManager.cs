using System.Collections.Generic;
using Newtonsoft.Json;
using Scripts.Definitions;
using Scripts.Utility;
using UnityEngine;

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
        /// Ensures that the Data for an entity is properly unregistered within its scene
        /// </summary>
        /// <param name="entity"></param>
        public static void Unregister(EntityData entity)
        {
            var scene = entity.scene;

            if (!CurrentState.scenes.TryGetValue(scene, out var sceneData))
            {
                throw new KeyNotFoundException($"Scene {scene} was not yet registered in the game state");
            }

            if (entity.isDefaultInScene)
            {
                entity.destroyed = true;
            }
            else
            {
                sceneData.entities.Remove(entity.id);
            }
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

        public static void SwitchScene(string newScene, Vector2 newCatPos)
        {
            Debug.Log(JsonConvert.SerializeObject(CurrentState.CurrentScene, JsonSettings.SerializerSettings));
            if (!CurrentState.scenes.ContainsKey(newScene))
            {
                CurrentState.scenes[newScene] = new SceneData();
            }
            var oldScene = CurrentState.currentScene;
            CurrentState.currentScene = newScene;
            CurrentState.cat.position = newCatPos;
            onSceneSwitched?.Invoke(oldScene, newScene);
        }

        #region Events

        public static EventHandler<CatData, ItemData> onItemPickedUp;
        public static EventHandler<EntityData> onEntityCreated;
        public static EventHandler<EntityData> onEntityDestroyed;
        public static EventHandler<string, string> onSceneSwitched;

        #endregion
    }
}