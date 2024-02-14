using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Controls the behavior of the Witch in the tutorial.
    /// </summary>
    public class TutorialWitch : MonoBehaviour
    {
        private const string Author = "Witch Statue";

        // currently using basic item entity
        public Collider2D collider2d;

        public bool dialogueTriggered;

        // update witch state
        //  should trigger dialogue on proximity
        //  should give key item to cat
        //  should disappear after dialogue
        private void FixedUpdate()
        {
            var cat = UnityState.Instance.cat;

            if (!dialogueTriggered &&
                cat.collider2d.Distance(collider2d).distance < 5 &&
                InputManager.Actions.Player.Interact.IsPressed())
            {
                dialogueTriggered = true;
                UnityState.Instance.dialogue.StartInteraction(new Interaction(
                    new DialogueLine(Author, "The door is locked").AuthorColor(Color.blue).TextColor(Color.black)
                        .AddNext(new DialogueLine(Author, "There used to be a key around here somewhere, but it may have been broken...").AuthorColor(Color.blue).TextColor(Color.black))
                ));
            }
        }
    }
}