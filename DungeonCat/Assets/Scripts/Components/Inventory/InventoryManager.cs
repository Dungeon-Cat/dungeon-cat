using System;
using Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Components.Inventory
{
    /// <summary>
    /// Component that handles the player inventory, opening/closing and saving
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {

        public GameObject inventory;

        public Cat cat;

        public Transform itemContent;
        public GameObject inventoryItem;
        
        public static InventoryManager Instance { get; private set; }

        private void Start()
        {
            // Make sure singleton applied
            Instance = this;
        }

        private void Update()
        {
            if (cat.data.inventory.isDirty && inventory.activeSelf)
            {
                cat.data.inventory.isDirty = false;
                ListItems();
            }
        }

        private void ListItems()
        {
            // Clean previous rendered items in the inventory to make sure we don't duplicate things
            foreach (Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }

            // Read inventory item data from cat's inventory
            // Render each item in cat's inventory
            for (var slot = 0; slot < cat.data.inventory.items.Length; slot++)
            {
                var item = cat.data.inventory.items[slot];
                
                // Render if item ID is valid
                if (!item.IsEmpty())
                {
                    var obj = Instantiate(inventoryItem, itemContent);
                    var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                    var itemCount = obj.transform.Find("ItemCount").GetComponent<Text>();
                    var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                    obj.GetComponent<InventoryItem>().slot = slot;

                    itemName.text = item.id;
                    itemCount.text = Convert.ToString(item.count);
                    var itemDef = item.GetItemDef();
                    itemIcon.sprite = Resources.Load<Sprite>(itemDef.Icon);
                    itemIcon.preserveAspect = true;
                }
            }
        }

        public void ToggleInventory()
        {
            inventory.SetActive(!inventory.activeSelf);

            if (inventory.activeSelf)
            {
                ListItems();
            }
        }

    }
}