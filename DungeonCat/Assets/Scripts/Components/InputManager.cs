using Input;
using UnityEngine;
namespace Scripts.Components
{
    public class InputManager : MonoBehaviour
    {
        public static InputActions Actions { get; private set; }
        
        private void Awake()
        {
            Actions = new InputActions();
            Actions.Enable();
        }

        private void FixedUpdate()
        {
            if (Actions.Player.Move.IsPressed())
            {
                var input = Actions.Player.Move.ReadValue<Vector2>();
                var cat = UnityState.Instance.cat;
            
                cat.transform.Translate(input.x, input.y, 0);
                cat.SyncToData();
            }

        }
    }
}