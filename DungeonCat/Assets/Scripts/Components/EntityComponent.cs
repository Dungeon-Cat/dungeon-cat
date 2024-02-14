using Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Components
{
    public abstract class EntityComponent<T> : ComponentWithData<T>, IEntityComponent where T : EntityData
    {
        [FormerlySerializedAs("collider")]
        public Collider2D collider2d;
        
        public string Id => data.id;
        
        protected virtual void Start()
        {
            GameStateManager.Register(data);
        }

        public override void SyncFromData()
        {
            transform.position = data.position;
        }

        public override void SyncToData()
        {
            data.position = transform.position;
            data.scene = gameObject.scene.name;
        }
    }
}