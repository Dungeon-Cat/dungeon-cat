using System;
using System.Linq;
using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components
{
    public class UnityState : MonoBehaviour
    {
        public Cat cat;
        public static UnityState Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameStateManager.Init(cat.data);

            // listen to events
            GameStateManager.onItemPickedUp += OnItemPickedUp;
            GameStateManager.onEntityCreated += OnEntityCreated;
            GameStateManager.onEntityDestroyed += OnEntityDestroyed;
        }

        public static DungeonLevel GetScene(string sceneName) =>
            SceneManager.GetSceneByName(sceneName).GetRootGameObjects().First().GetComponentInChildren<DungeonLevel>();

        private static void OnEntityCreated(EntityData entity)
        {
            var scene = GetScene(entity.scene);
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

        private static void OnEntityDestroyed(EntityData entity)
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
            Debug.Log($"Picked up {item.id}");
        }

        private void OnDestroy()
        {
            GameStateManager.onItemPickedUp -= OnItemPickedUp;
            GameStateManager.onEntityCreated -= OnEntityCreated;
            GameStateManager.onEntityDestroyed -= OnEntityDestroyed;
        }
    }
}