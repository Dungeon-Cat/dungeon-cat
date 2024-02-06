using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class UnityState : MonoBehaviour
    {
        public static UnityState Instance { get; private set; }

        public Cat cat;
        
        private void Awake()
        {
            Instance = this;
            GameStateManager.Init(cat.data);
            
            // other startup tasks
            
        }
    }
}