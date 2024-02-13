using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;

#nullable enable

namespace Scripts.Components
{
    public class Dialogue : EntityComponent<DialogueData>
    {
        private Interaction? _currentInteraction;
        private IEnumerator? _coroutine;
        public const string DefaultDialogueOption = "MEOW";
        public void Awake()
        {
            gameObject.SetActive(true);
            data.text_vis.text = "blank";
            data.author_vis.text = "blank";
        }

        public void StartInteraction(Interaction? interaction)
        {
            if (interaction == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);

            if (_currentInteraction == null)
            {
                _currentInteraction = interaction;
            }
            else
            {
                data.Interactions.Enqueue(interaction);
            }

            var (author, sentence) = _currentInteraction.First().GetValueOrDefault(("", ""));
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = TypeSentence(author, sentence);
            StartCoroutine(_coroutine);
        }

        private void ProgressInteraction(string dialogueOption)
        {
            if (_currentInteraction == null) return;
            var result = _currentInteraction.GetNext(dialogueOption);
            if (result == null)
            {
                _currentInteraction = null;
                var nextInteraction = data.Interactions.Dequeue();
                StartInteraction(nextInteraction);
            }
            else
            {
                (data.text_vis.text, data.author_vis.text) = result.Value;
            }
        }
        
        private IEnumerator TypeSentence(string author, string sentence)
        {
            data.author_vis.text = author;
            data.text_vis.text = "";
            foreach (char letter in sentence)
            {
                data.text_vis.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitUntil(() => InputManager.Actions.Player.Interact.IsPressed());
            data.text_vis.text = "";
            data.author_vis.text = "";
            ProgressInteraction(DefaultDialogueOption);
        }
    }

    /// <summary>
    /// A rooted tree with dialogue options as edges and (author, text) pairs as nodes.
    /// The root is accessible via First(), and all nodes thereafter by providing an
    /// edge to move along.
    /// </summary>
    [Serializable]
    public class Interaction
    {
        private Node? _start;
        private Node? _curr;
        // A node doesn't know whether it's the end, so to end an interaction, we have a
        // separate set of callbacks.
        private List<Action> _onEndCallbacks = new();
        
        public (string, string)? First()
        {
            if (_curr == null) return null;
            _curr.StartCallbacks();
            return (_curr.author, _curr.text);
        }
        
        public (string, string)? GetNext(string dialogueOption)
        {
            if (_curr == null) return null;
            _curr.EndCallbacks();
            _curr = _curr.GetNext(dialogueOption);
            if (_curr == null)
            {
                EndCallbacks();
                return null;
            }
            return (_curr.author, _curr.text);
        }

        public void AddEndCallback(Action action)
        {
            _onEndCallbacks.Add(action);
        }

        private void EndCallbacks()
        {
            foreach (Action action in _onEndCallbacks)
            {
                action.Invoke();
            }
        }
        
        // Produces an interaction from an author and a list of text options.
        public static Interaction Basic(string author, IEnumerable<string> iterator)
        {
            Interaction interaction = new();
            Node curr = new(author, "");
            interaction._curr = curr;
            
            foreach (var text in iterator)
            {
                if (text == null) return interaction;
                curr.text = text;
                Node next = new(author, "");
                curr.AddNext(next);
                curr = next;
            }

            return interaction;
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

            public Node(string author, string text)
            {
                this.author = author;
                this.text = text;
            }

            public void AddNext(Node node)
            {
                _options.Add(Dialogue.DefaultDialogueOption, node);
            }
            
            public void AddNext(string option, Node node)
            {
                _options.Add(option, node);
            }

            public Node? GetNext(string option)
            {
                EndCallbacks();
                Node? next = _options[option];
                if (next == null) return next;
                next.StartCallbacks();
                return next;
            }

            // A node's start callbacks are called by the entity that
            // creates it.
            public void StartCallbacks()
            {
                foreach (var callback in _callbacksOnStart)
                {
                    callback.Invoke();
                }
            }
            
            // A node is responsible for calling its own EndCallbacks.
            public void EndCallbacks()
            {
                foreach (var callback in _callbacksOnEnd)
                {
                    callback.Invoke();
                }
            }

            public void AddStartCallBack(Action action)
            {
                _callbacksOnStart.Add(action);
            }
            
            public void AddEndCallBack(Action action)
            {
                _callbacksOnEnd.Add(action);
            }
        }
    }
}
