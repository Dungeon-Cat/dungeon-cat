using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class UnityState : MonoBehaviour
    {
        public Cat cat;
        public Dialogue dialogue;
        public static UnityState Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameStateManager.Init();
            // other startup tasks
        }
    }
}