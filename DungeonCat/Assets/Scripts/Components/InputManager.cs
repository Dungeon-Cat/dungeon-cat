using Input;
using UnityEngine;

namespace Components
{
    public class InputManager : MonoBehaviour
    {
        public static InputActions Actions { get; private set; }
        
        private void Awake()
        {
            Actions = new InputActions();
            Actions.Enable();
        }
    }
}