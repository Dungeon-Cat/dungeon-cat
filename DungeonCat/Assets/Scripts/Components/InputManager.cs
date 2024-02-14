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

        private void Update()
        {
            if (Actions.Player.Move.IsPressed())
            {
                var input = Actions.Player.Move.ReadValue<Vector2>();
                var cat = UnityState.Instance.cat;
                cat.data.facing = input;

                var movement = input * (Time.deltaTime * 50);

                cat.transform.Translate(movement.x, movement.y, 0);
                cat.SyncToData();
            }

        }
    }
}