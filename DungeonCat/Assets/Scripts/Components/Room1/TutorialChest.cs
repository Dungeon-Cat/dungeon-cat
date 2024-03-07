using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room1
{
    /// <summary>
    /// Chest in Room 1.
    /// Opens when interacted with.
    /// </summary>
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