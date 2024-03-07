using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Component that has unique behavior or interactions
    /// </summary>
    /// <typeparam name="T">The associated EntityData class</typeparam>
    public abstract class EntityComponent<T> : ComponentWithData<T>, IEntityComponent where T : EntityData
    {
        [HideInInspector]
        public Collider2D collider2d;

        [HideInInspector]
        public Rigidbody2D body;
        
        public string Id => data.id;

        protected virtual void Start()
        {
            collider2d = GetComponent<Collider2D>();
            body = GetComponent<Rigidbody2D>();
            GameStateManager.Register(data);
        }

        protected override void OnValidateInEditor()
        {
            base.OnValidateInEditor();
            data.isDefaultInScene = true;
        }

        public override void SyncFromData()
        {
            transform.position = data.position;
            if (data.destroyed)
            {
                UnityState.OnEntityDestroyed(data);
            }
        }

        public override void SyncToData()
        {
            data.position = transform.position;
            data.scene = gameObject.scene.name;
        }
    }
}