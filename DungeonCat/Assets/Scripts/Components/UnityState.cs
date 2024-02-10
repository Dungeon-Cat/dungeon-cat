using System;
using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class UnityState : MonoBehaviour
    {
        public static UnityState Instance { get; private set; }

        public Cat cat;
        public Dialogue dialogue;
        
        private void Awake()
        {
            Instance = this;
            GameStateManager.Init(cat.data);
            
            // other startup tasks

            GameStateManager.onItemPickedUp += OnItemPickedUp;

        }
        private void OnItemPickedUp(CatData _, ItemData item)
        {
            Debug.Log($"Picked up {item.id}");
        }

        private void OnDestroy()
        {
            GameStateManager.onItemPickedUp -= OnItemPickedUp;
        }
    }
}