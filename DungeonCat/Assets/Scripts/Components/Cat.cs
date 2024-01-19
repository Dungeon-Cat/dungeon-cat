using Data;
using UnityEngine;

namespace Components
{
    public class Cat : Component<CatData>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            var input = InputManager.Actions.Player.Move.ReadValue<Vector2>();
            
            transform.Translate(input.x, input.y, 0);
        }
    }
}