﻿using Scripts.Components.CommonEntities;
using Scripts.Components.Room1;
using Scripts.Components.UI;
using UnityEngine;

namespace Scripts.Components.Room3
{
    /// <summary>
    /// Info Statue in Room 3.
    /// Provides dialogue when interacted with
    /// </summary>
    public class InfoStatue : MonoBehaviour, IInteractable
    {
        private const string Author = "Witch Statue";
        public bool dialogueTriggered;

        public bool CanBeInteractedWith() => !dialogueTriggered;

        public void Interact()
        {
            dialogueTriggered = true;
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                new DialogueLine(Author, "Curiously, these old ruins seem to be built specifically so that some cat can solve them...").AuthorColor(Color.blue).TextColor(Color.black)
            ));
        }
    }
}