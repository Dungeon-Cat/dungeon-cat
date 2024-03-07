using Scripts.Components.UI;
using Scripts.Data;
using Scripts.UI;
using Scripts.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Components.Inventory
{
    /// <summary>
    /// Component that controls the specific behavior of items that appear in the inventory
    /// </summary>
    public class InventoryItem : MonoBehaviour, IEndDragHandler, IBeginDragHandler
    {
        public Image icon;
        
        public int slot;

        public void OnBeginDrag(PointerEventData eventData)
        {
            UiManager.Instance.isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var results = EventSystem.current.RaycastAll(eventData);

            var foundInventory = false;
            
            foreach (var result in results)
            {
                foundInventory |= result.gameObject.HasComponent<Inventory>();
                
                if (result.gameObject.HasComponent(out InventoryItem inventoryItem) && inventoryItem.slot != slot)
                {
                    GameStateManager.CurrentState.cat.TryCombine(slot, inventoryItem.slot);
                    return;
                }
            }

            if (!foundInventory)
            {
                var pos = eventData.position;
                var objectResults = Physics2D.RaycastAll(pos, Vector2.zero);
                if (objectResults.Length == 0)
                {
                    GameStateManager.CurrentState.cat.DropItem(slot, Camera.main!.ScreenToWorldPoint(pos));
                
                }
                else
                {
                    foreach (var result in objectResults)
                    {
                        if (result.transform.gameObject.HasComponent(out InteractableObject interactable) &&
                            interactable.CanBeInteractedWith())
                        {
                            GameStateManager.CurrentState.cat.DropItem(slot, Camera.main!.ScreenToWorldPoint(pos));
                            return;
                        }
                    }
                }
            }
            
            UiManager.Instance.isDragging = false;
        }

        public void ButtonPress()
        {
            if (GetComponent<DraggablePanel>().isDragging) return;

            Debug.Log($"pressed {slot}");
        }
    }
}