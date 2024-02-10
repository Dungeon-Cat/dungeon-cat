using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
using UnityEngine.UIElements;

#nullable enable

namespace Scripts.Components
{
    public class Dialogue : EntityComponent<DialogueData>
    {
        private Interaction? _currentInteraction;
        private IEnumerator? _coroutine;
        public new void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
            data.text_vis.text = "";
            data.author_vis.text = "";
        }

        public void StartInteraction(Interaction interaction)
        {
            gameObject.SetActive(true);

            if (_currentInteraction == null)
            {
                _currentInteraction = interaction;
            }
            else
            {
                data.Interactions.Enqueue(interaction);
            }

            _coroutine = TypeSentence(_currentInteraction.First().Item2);
        }

        public void ProgressInteraction(string dialogueOption)
        {
            (data.text_vis.text, data.author_vis.text) = _currentInteraction.GetNext(dialogueOption);
            
        }
        
        // private IEnumerator Cycle()
        // {
        //     for (var i = 0; i < data.DialogueEntries.Count; i++)
        //     {
        //         data.text.text = data.DialogueEntries.Dequeue().text;
        //
        //         yield return new WaitUntil(() => Input.InputActions.UIActions.Click);
        //         yield return null;
        //     }
        //     gameObject.SetActive(false);
        // }
        
        private IEnumerator TypeSentence(string sentence)
        {
            data.text_vis.text = "";
            foreach (char letter in sentence)
            {
                data.text_vis.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(2);
            data.text_vis.text = "";
            data.author_vis.text = "";
        }
    }

    [Serializable]
    public class Interaction
    {
        private Node? _curr;

        
        public (string, string) First()
        {
            _curr.StartCallbacks();
            if (_curr == null) return ("", "");
            return (_curr.author, _curr.text);
        }
        
        public (string, string) GetNext(string dialogueOption)
        {
            if (_curr == null) return ("", "");
            _curr = _curr.GetNext(dialogueOption);
            if (_curr == null) return ("", "");
            return (_curr.author, _curr.text);
        }
        
        [Serializable]
        private class Node
        {
            // The name of the author (may be separate from their name as a character, for
            // instance if a character's name is hidden from the player).
            public string author;
            // The current string being displayed as the dialogue text.
            public string text;
            // These are the dialogue options of the cat (or response options, anyway).
            // Most of the time, the default dialogue option to progress to the next
            // next will be 'meow'.
            private Dictionary<string, Node> _options = new();
            // A node in an interaction takes these actions when it is reached.
            // This is to signal choices that might affect the character with whom the
            // dialogue is being taken.
            private List<Action> _callbacksOnStart = new(), _callbacksOnEnd = new();
            private static string _defaultOption = "MEOW";

            public Node(string author, string text)
            {
                this.author = author;
                this.text = text;
            }

            public void AddNext(Node node)
            {
                _options.Add(_defaultOption, node);
            }
            
            public void AddNext(string option, Node node)
            {
                _options.Add(option, node);
            }

            public Node? GetNext(string option)
            {
                foreach (var callback in _callbacksOnEnd)
                {
                    callback.Invoke();
                }
                Node? next = _options[option];
                next.StartCallbacks();
                return next;
            }

            public void StartCallbacks()
            {
                foreach (var callback in _callbacksOnStart)
                {
                    callback.Invoke();
                }
            }
        }

        public static Interaction Basic(string author, IEnumerable<string> iterator)
        {
            Interaction interaction = new();
            Node curr = new Node(author, "");
            interaction._curr = curr;
            
            foreach (var text in iterator)
            {
                if (text == null) return interaction;
                curr.text = text;
                Node next = new Node(author, "");
                curr.AddNext(next);
                curr = next;
            }

            return interaction;
        }
    }
}