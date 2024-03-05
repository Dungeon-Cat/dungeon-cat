using System;
using System.Collections;
using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using Scripts.Data;
using Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components
{
    public class UnityState : MonoBehaviour
    {
        public Cat cat;
        public static UnityState Instance { get; private set; }

        public Dialogue dialogue;
        
        public bool IsSwitchingScenes { get; private set; }

        private void Awake()
        {
            Instance = this;
#if !UNITY_EDITOR
            if (!SceneManager.GetSceneByName(GameState.DefaultRoom).IsValid())
            {
                SceneManager.LoadScene(GameState.DefaultRoom, LoadSceneMode.Additive);
            }
#endif
            GameStateManager.Init(cat.data);

            // listen to events
            GameStateManager.onItemPickedUp += OnItemPickedUp;
            GameStateManager.onEntityCreated += OnEntityCreated;
            GameStateManager.onEntityDestroyed += OnEntityDestroyed;
            GameStateManager.onSceneSwitched += OnSceneSwitched;
            GameStateManager.onSaveLoaded += OnSaveLoaded;
            GameStateManager.onLoadFailed += OnLoadFailed;
        }

        public static DungeonLevel CurrentScene => GetScene(GameStateManager.CurrentState.currentScene);

        public static DungeonLevel GetScene(string sceneName) =>
            SceneManager.GetSceneByName(sceneName).GetRootGameObjects().FirstNonNull(o => o.GetComponent<DungeonLevel>());

        /// <summary>
        /// Callback for when an entity is created within the game data to create the corresponding entity game object
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static void OnEntityCreated(EntityData entity)
        {
            var scene = GetScene(entity.scene);
            if (scene == null)
            {
                Debug.LogError($"Scene was not found for {entity.scene}");
            }

            var prefab = Resources.Load<GameObject>(entity switch
            {
                CatData => throw new InvalidOperationException("Creating a new Cat entity is not allowed"),
                ItemEntityData => "Prefabs/ItemPrefab",
                _ => throw new NotImplementedException(),
            });
            var newGameObject = Instantiate(prefab, scene.transform);
            newGameObject.name = entity.id;
            var newEntity = newGameObject.GetComponent<IEntityComponent>();
            newEntity.SetData(entity);
            newEntity.SyncFromData();

            scene.entities.Add(newEntity.Id, newGameObject);
        }

        /// <summary>
        /// Callback for when an entity is destroyed within the game data to destroy the corresponding entity game object
        /// </summary>
        /// <param name="entity"></param>
        public static void OnEntityDestroyed(EntityData entity)
        {
            var scene = GetScene(entity.scene);

            if (scene.entities.TryGetValue(entity.id, out var gameObject))
            {
                Destroy(gameObject);
                scene.entities.Remove(entity.id);
            }
        }

        private void OnItemPickedUp(CatData _, ItemData item)
        {
            // Debug.Log($"Picked up {item.id}");
        }

        private void OnDestroy()
        {
            Instance = null;
            GameStateManager.onItemPickedUp -= OnItemPickedUp;
            GameStateManager.onEntityCreated -= OnEntityCreated;
            GameStateManager.onEntityDestroyed -= OnEntityDestroyed;
            GameStateManager.onSceneSwitched -= OnSceneSwitched;
            GameStateManager.onSaveLoaded -= OnSaveLoaded;
            GameStateManager.onLoadFailed -= OnLoadFailed;
        }

        private void OnSceneSwitched(string oldScene, string newScene)
        {
            StartCoroutine(SwitchScene(oldScene, newScene));
        }

        private IEnumerator SwitchScene(string oldScene, string newScene)
        {
            IsSwitchingScenes = true;
            cat.SyncFromData();

            var oldLevel = GetScene(oldScene);
            oldLevel.SyncToData();

            yield return SceneManager.UnloadSceneAsync(oldScene);
            yield return SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

            var newLevel = GetScene(newScene);
            newLevel.Start();
            newLevel.data = GameStateManager.CurrentState.CurrentScene;
            newLevel.SyncFromData();
            IsSwitchingScenes = false;
        }

        private void OnSaveLoaded(GameState oldState, GameState newGameState)
        {
            cat.data = newGameState.cat;
            var oldLevel = GetScene(oldState.currentScene);
            oldLevel.data = oldState.CurrentScene;
            StartCoroutine(SwitchScene(oldState.currentScene, newGameState.currentScene));
        }

        public void Save() => SaveLoadManager.Save();

        public void Load() => SaveLoadManager.Load();

        private void OnLoadFailed()
        {
            dialogue.StartInteraction(
                new Interaction(new DialogueLine("System", "No Save Found").AuthorColor(Color.blue).TextColor(Color.white))
            );
        }

    }
}