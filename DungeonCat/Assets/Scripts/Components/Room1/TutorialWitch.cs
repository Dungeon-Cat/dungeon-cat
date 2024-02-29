using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using UnityEngine;

namespace Scripts.Components.Room1
{
    /// <summary>
    /// Controls the behavior of the Witch in the tutorial.
    /// </summary>
    public class TutorialWitch : MonoBehaviour, IInteractable
    {
        private const string Author = "Witch Statue";

        public bool dialogueTriggered;

        public bool CanBeInteractedWith() => !dialogueTriggered;

        public void Interact()
        {
            dialogueTriggered = true;
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                new DialogueLine(Author, "The door is locked").AuthorColor(Color.blue).TextColor(Color.black)
                    .AddNext(new DialogueLine(Author, "There used to be a key around here somewhere, but it may have been broken...").AuthorColor(Color.blue).TextColor(Color.black))
            ));
        }
    }
}