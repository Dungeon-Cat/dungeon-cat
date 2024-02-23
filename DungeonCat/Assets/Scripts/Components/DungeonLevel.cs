using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Scripts.Components.CommonEntities;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Script attached to the root object of each scene that's a Dungeon Level
    /// </summary>
    public class DungeonLevel : ComponentWithData<SceneData>
    {
        [HideInInspector]
        public bool started;
        
        public Dictionary<string, GameObject> entities = new();

        public IEnumerable<IEntityComponent> AllEntities => entities.Values.Select(o => o.GetComponent<IEntityComponent>());
        
        public void Start()
        {
            if (started) return;
            started = true;
            
            GameStateManager.CurrentState.scenes.TryAdd(gameObject.scene.name, new SceneData());
            
            entities = gameObject.scene
                .GetRootGameObjects()
                .SelectMany(o => o.GetComponentsInChildren<IEntityComponent>())
                .ToDictionary(e => e.Id, e => e.GameObject);

            Debug.Log($"Scene {name} start");
        }

        public override void SyncFromData()
        {
            foreach (var (id, entity) in entities.ToList())
            {
                var entityComponent = entity.GetComponent<IEntityComponent>();
                if (GameStateManager.CurrentState.CurrentScene.entities.TryGetValue(id, out var entityData))
                {
                    entityComponent.SetData(entityData);
                    entityComponent.SyncFromData();
                }
            }

            foreach (var (id, entityData) in GameStateManager.CurrentState.CurrentScene.entities)
            {
                if (!entities.ContainsKey(id) && !entityData.destroyed)
                {
                    UnityState.OnEntityCreated(entityData);
                }
            }
        }
    }
}