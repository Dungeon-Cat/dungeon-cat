using System;
using UnityEngine;

namespace Scripts.Components
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private bool triggerActive = false;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                triggerActive = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                triggerActive = false;
            }
        }

        private void Update()
        {
            if (triggerActive && UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OpenDoor();
            }
        }

        private void OpenDoor()
        {
            Console.WriteLine("Space key pressed");
        }
    }
}