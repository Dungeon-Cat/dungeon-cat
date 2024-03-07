using Scripts.Components.CommonEntities;
using Scripts.Components.Room1;
using Scripts.Components.UI;
using UnityEngine;

namespace Scripts.Components.WinRoom
{
    /// <summary>
    /// Statue in Win Room.
    /// Presents dialogue when interacted with
    /// </summary>
    public class WinStatue : MonoBehaviour, IInteractable
    {
        private const string Author = "Witch Statue";
        public bool dialogueTriggered;
        
        public bool CanBeInteractedWith() => !dialogueTriggered;
        public new void Interact()
        {
            dialogueTriggered = true;
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                new DialogueLine(Author, "You win!").AuthorColor(Color.blue).TextColor(Color.black)
            ));
        }
    }
}