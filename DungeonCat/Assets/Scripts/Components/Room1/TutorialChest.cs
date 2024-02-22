using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room1
{
    public class TutorialChest : MonoBehaviour
    {
        public ChestEntity chest;

        private void FixedUpdate()
        {
            var cat = UnityState.Instance.cat;

            if (!chest.IsOpen &&
                cat.collider2d.Distance(chest.collider2d).distance < 5 &&
                InputManager.Actions.Player.Interact.IsPressed())
            {
                chest.SetOpen(true);
                chest.data.DropContents();
            }
        }
    }
}