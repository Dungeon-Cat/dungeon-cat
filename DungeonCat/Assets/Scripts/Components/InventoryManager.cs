using System;
using Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Components
{
    public class InventoryManager : MonoBehaviour
    {
        
        public static InventoryManager instance;
        
        public GameObject cat;
        public Cat catComponent;
        public ContainerData itemsData;

        public Transform itemContent;
        public GameObject inventoryItem;

        private void Awake()
        {
            // Read inventory item data from cat's inventory
            catComponent = cat.GetComponent<Cat>();
            itemsData = catComponent.data.inventory;
            
            // Make sure singleton applied
            instance = this;
        }

        public void ListItems()
        {
            // Clean previous rendered items in the inventory to make sure we don't duplicate things
            foreach (Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }
            
            // Render each item in cat's inventory
            foreach (var item in itemsData.items)
            {
                // Render if item ID is valid
                if (!String.IsNullOrWhiteSpace(item.id))
                {
                    GameObject obj = Instantiate(inventoryItem, itemContent);
                    var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                    var itemCount = obj.transform.Find("ItemCount").GetComponent<Text>();
                    var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                    itemName.text = item.id;
                    itemCount.text = Convert.ToString(item.count);
                    itemIcon.sprite = Resources.Load<Sprite>("Images/Items/"+item.id);
                }
            }
        }

    }
}
