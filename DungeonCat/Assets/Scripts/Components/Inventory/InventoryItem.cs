using System.Collections.Generic;
using Scripts.Components.UI;
using Scripts.Data;
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
            // icon.maskable = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // icon.maskable = true;
            
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

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
                GameStateManager.CurrentState.cat.DropItem(slot, Camera.main!.ScreenToWorldPoint(eventData.position));
            }
        }

        public void ButtonPress()
        {
            if (GetComponent<DraggablePanel>().isDragging) return;

            Debug.Log($"pressed {slot}");
        }
    }
}