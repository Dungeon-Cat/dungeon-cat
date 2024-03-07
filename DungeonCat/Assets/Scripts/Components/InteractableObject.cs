using Scripts.Components.CommonEntities;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Object that can be interacted with by a player
    /// </summary>
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

        public bool CanBeInteractedWith()
        {
            if (interactable == null || collider2d == null) return false;

            return UnityState.Instance.cat.collider2d.Distance(collider2d).distance < InteractDistance && interactable.CanBeInteractedWith();
        }

        public void Interact()
        {
            interactable?.Interact();
        }

        private void Update()
        {
            if (interactable == null || collider2d == null) return;

            if (CanBeInteractedWith())
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