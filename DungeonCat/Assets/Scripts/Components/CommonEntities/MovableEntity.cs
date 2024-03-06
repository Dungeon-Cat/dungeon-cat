using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    public class MovableEntity : EntityComponent<EntityData>
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            SyncToData();
        }
    }
}