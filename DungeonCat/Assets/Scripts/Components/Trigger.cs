using System;
using UnityEngine;

namespace Scripts.Components
{
    // A class to controller the interaction trigger to open a wooden door (could be more general)s
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