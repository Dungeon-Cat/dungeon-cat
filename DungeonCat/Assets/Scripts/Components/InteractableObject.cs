using Scripts.Components.CommonEntities;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Components
{
    public class InteractableObject : MonoBehaviour
    {
        public const float InteractDistance = 5;

        private Collider2D collider2d;
        private IInteractable interactable;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            collider2d = GetComponent<Collider2D>();
            interactable = GetComponent<IInteractable>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (interactable == null || collider2d == null) return;
            
            if (UnityState.Instance.cat.collider2d.Distance(collider2d).distance < InteractDistance && interactable.CanBeInteractedWith())
            {
                spriteRenderer.material = UiManager.Instance.spriteHighlight;
                if (InputManager.Actions.Player.Interact.WasPressedThisFrame())
                {
                    interactable.Interact();
                }
            }
            else
            {
                spriteRenderer.material = UiManager.Instance.spriteDefault;
            }
        }
    }
}