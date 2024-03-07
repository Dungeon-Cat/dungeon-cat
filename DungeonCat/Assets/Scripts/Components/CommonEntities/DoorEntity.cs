using Scripts.Data;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Openable Entity that transitions the game between scenes
    /// </summary>
    public class DoorEntity : OpenableEntityComponent<DoorEntityData>
    {
        public string goesToScene;
        public Vector2 newCatPos;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (IsOpen && other.gameObject.HasComponent(out Cat _))
            {
                GameStateManager.SwitchScene(goesToScene, newCatPos);
            }
        }
    }
}