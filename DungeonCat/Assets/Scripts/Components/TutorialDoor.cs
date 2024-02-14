using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Controls the behavior of the door in the tutorial, currently opening if the player is nearby
    /// or closing if not.
    /// </summary>
    public class TutorialDoor : MonoBehaviour
    {
        public DoorEntity door;

        private void FixedUpdate()
        {
            var cat = UnityState.Instance.cat;

            var touching = cat.collider.Distance(door.collider).distance < 5;

            if (touching != door.IsOpen)
            {
                door.SetOpen(touching);
            }
        }
    }
}