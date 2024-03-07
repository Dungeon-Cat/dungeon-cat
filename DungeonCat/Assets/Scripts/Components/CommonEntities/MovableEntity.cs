using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Entity that can be moved by the player character
    /// </summary>
    public class MovableEntity : EntityComponent<EntityData>
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            SyncToData();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            SyncToData();
        }
    }
}