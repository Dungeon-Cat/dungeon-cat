using Input;
using UnityEngine;
namespace Scripts.Components
{
    public class InputManager : MonoBehaviour
    {
        private static InputActions actions;

        public static InputActions Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new InputActions();
                    actions.Enable();
                }
                return actions;
            }
        }

        private void FixedUpdate()
        {
            if (Actions.Player.Move.IsPressed())
            {
                var input = Actions.Player.Move.ReadValue<Vector2>();
                var cat = UnityState.Instance.cat;

                cat.transform.Translate(input.x, input.y, 0);
                cat.data.facing = input;
                cat.SyncToData();
            }

        }
    }
}