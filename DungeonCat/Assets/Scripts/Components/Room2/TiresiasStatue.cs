using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class TiresiasStatue : MonoBehaviour, IInteractable
    {
        public static TiresiasStatue Instance { get; private set; }
        
        public TiresiasStatue()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Start()
        {
            Instance = this;
        }

        private const string Author = "Tiresias";

        public static bool DialogueTriggered;

        public bool CanBeInteractedWith() => !DialogueTriggered;

        public void Interact()
        {
            DialogueTriggered = true;
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                new DialogueLine("Cat", "A statue... its eyes are closed.").AuthorColor(Color.blue)
                    .AddDefault(new DialogueLine("Statue", "Do I hear someone? It has been a long time since anyone came down here... I cannot see you. Long ago, I was blinded.").AuthorColor(Color.red)
                        .AddDefault(new DialogueLine("Cat", "Do you know how I may proceed from here? The doors are locked.").AuthorColor(Color.blue)
                            .AddDefault(new DialogueLine("Statue", "That is a simple matter. Behold, I shall unlock the left door.").AuthorColor(Color.red))))
            ));
        }
    }
}