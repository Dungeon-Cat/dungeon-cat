using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room1
{
    public class TutorialChest : MonoBehaviour, IInteractable
    {
        public ChestEntity chest;

        public bool CanBeInteractedWith() => !chest.IsOpen;

        public void Interact()
        {
            chest.SetOpen(true);
            chest.data.DropContents();
        }
    }
}