using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using UnityEngine;

namespace Scripts.Components.Room3
{
    /// <summary>
    /// Statue in Room 3.
    /// Provides dialogue and resets Yarn when interacted with
    /// </summary>
    public class YarnStatue : MonoBehaviour, IInteractable
    {
        private const string Author = "Witch Statue";

        public bool talkedTo;

        public Room3DungeonLevel dungeonLevel;

        public bool CanBeInteractedWith() => true;

        public void Interact()
        {
            if (!talkedTo)
            {
                UnityState.Instance.dialogue.StartInteraction(new Interaction(
                    new DialogueLine(Author, "I can bring back the yarn if you ever get stuck.").AuthorColor(Color.blue).TextColor(Color.black))
                );
                talkedTo = true;
            }
            else
            {
                dungeonLevel.OnYarnStatueInteract();
            }
        }
    }
}