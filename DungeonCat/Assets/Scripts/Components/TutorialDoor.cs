using Scripts.Data;
using Scripts.Definitions.Items;
using Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components
{
    /// <summary>
    /// Controls the behavior of the door in the tutorial, currently opening if the player is nearby
    /// or closing if not.
    /// </summary>
    public class TutorialDoor : MonoBehaviour
    {
        public DoorEntity door;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.HasComponent(out ItemEntity itemEntity) && itemEntity.data.item.id == nameof(TutorialKey))
            {
                GameStateManager.RemoveEntity(itemEntity.data);
                if (!door.IsOpen)
                {
                    door.SetOpen(true);
                }
            }

            if (door.IsOpen && other.gameObject.HasComponent(out Cat cat))
            {
                SceneManager.UnloadSceneAsync(door.data.scene);
            }
        }

        /*private void FixedUpdate()
        {
            var cat = UnityState.Instance.cat;

            var touching = cat.collider2d.Distance(door.collider2d).distance < 5;

            if (touching != door.IsOpen)
            {
                door.SetOpen(touching);
            }
        }*/
    }
}