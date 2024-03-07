using Scripts.Data;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    public class DoorEntity : OpenableEntityComponent<DoorEntityData>
    {
        public string goesToScene;
        public Vector2 newCatPos;
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (IsOpen && other.gameObject.HasComponent(out Cat _))
            {
                GameStateManager.SwitchScene(goesToScene, newCatPos);
            }
        }
    }
}