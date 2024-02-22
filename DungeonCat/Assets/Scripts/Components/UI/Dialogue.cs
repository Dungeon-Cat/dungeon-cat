#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Components.CommonEntities;
using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.Components.UI
{
    public class Dialogue : EntityComponent<DialogueData>
    {
        private Interaction? _currentInteraction;
        private IEnumerator? _coroutine;
        private bool _inInteractionChain;
        private readonly List<TMP_Text> _dialogueOptions = new();
        public const string DefaultDialogueOption = "";
        
        public void Awake()
        {
            gameObject.SetActive(false);
            data.textVis.text = "";
            data.authorVis.text = "";
            _dialogueOptions.Add(data.option1);
            _dialogueOptions.Add(data.option2);
            _dialogueOptions.Add(data.option3);
            _dialogueOptions.Add(data.option4);
            foreach (var tmpText in _dialogueOptions)
            {
                tmpText.text = "";
            }
        }

        public void StartInteraction(Interaction interaction)
        {
            if (_currentInteraction == null)
            {
                _currentInteraction = interaction;
            }
            else
            {
                data.Interactions.Enqueue(interaction);
            }
            if (_inInteractionChain) return;
            _inInteractionChain = true;
            CurrentInteraction();
        }

        private void CurrentInteraction()
        {
            if (!_inInteractionChain) return;
            gameObject.SetActive(_currentInteraction != null);
            if (_currentInteraction != null)
            {
                Display(_currentInteraction.First());
                return;
            }
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _inInteractionChain = false;
        }

        private void ProgressInteraction(string dialogueOption)
        {
            if (_currentInteraction == null) return;
            var result = _currentInteraction.GetNext(dialogueOption);
            if (result == null)
            {
                _currentInteraction = data.Interactions.Count > 0 ? data.Interactions.Dequeue() : null;
                CurrentInteraction();
                return;
            }
            Display(result);
        }

        private void Display(DialogueLine? line)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            if (line == null) return;
            _coroutine = TypeSentence(line); 
            StartCoroutine(_coroutine); 
        }
        
        private IEnumerator TypeSentence(DialogueLine line)
        {
            data.authorVis.text = line.author;
            data.authorVis.color = line.authorColor;
            data.textVis.text = "";
            data.textVis.color = line.textColor;
            foreach (var letter in line.text)
            {
                data.textVis.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.05f);
            if (!(line.Options.Count == 1 && line.Options[0].Item1 == ""))
            {
                for (var i = 0; i < line.Options.Count; i++)
                {
                    var (option, color) = line.Options[i];
                    _dialogueOptions[i].text = option;
                    _dialogueOptions[i].color = color;
                }
            }
            
            yield return new WaitUntil(() => InputManager.Actions.Player.Interact.IsPressed());
            yield return new WaitForSeconds(0.10f);
            foreach (var option in _dialogueOptions)
            {
                option.text = "";
                option.color = Color.white;
            }
            
            data.textVis.text = "";
            data.textVis.color = Color.white;
            data.authorVis.text = "";
            data.textVis.color = Color.white;
            ProgressInteraction(DefaultDialogueOption);
        }
    }
    
    [Serializable]
    public class DialogueLine
    {
        public string author;
        public string text;

        public Color authorColor = Color.white;
        public Color textColor = Color.white;
        // These are the dialogue options of the cat (or response options, anyway).
        // Most of the time, the default dialogue option to progress to the next
        // next will be 'meow'.
        // Provisional upper limit of four dialogue options.
        public List<(string, Color)> Options = new();
        private Dictionary<string, (Color, DialogueLine)> _options = new();
        // A node in an interaction takes these actions when it is reached.
        // This is to signal choices that might affect the character with whom the
        // dialogue is being taken.
        private List<Action> _callbacksOnStart = new(), _callbacksOnEnd = new();

        public DialogueLine(string author, string text)
        {
            this.author = author;
            this.text = text;
        }

        public DialogueLine AuthorColor(Color color)
        {
            authorColor = color;
            return this;
        }
        
        public DialogueLine TextColor(Color color)
        {
            textColor = color;
            return this;
        }
        
 
        public DialogueLine AddNext(DialogueLine node, string option, Color color)
        {
            Options.Add((option, color));
            _options.Add(option, (color, node));
            return this;
        }
        
        public DialogueLine AddNext(DialogueLine node, string option = Dialogue.DefaultDialogueOption)
        {
            return AddNext(node, option, Color.white);
        }

        public DialogueLine? GetNext(string option)
        {
            EndCallbacks();
            DialogueLine? next = _options.ContainsKey(option) ? _options[option].Item2 : null;
            if (next == null) return next;
            next.StartCallbacks();
            return next;
        }

        // A node's start callbacks are called by the node that
        // creates it.
        public void StartCallbacks()
        {
            foreach (Action callback in _callbacksOnStart)
            {
                callback.Invoke();
            }
        }
        
        // A node is responsible for calling its own EndCallbacks.
        private void EndCallbacks()
        {
            foreach (Action callback in _callbacksOnEnd)
            {
                callback.Invoke();
            }
        }

        public DialogueLine AddStartCallBack(Action action)
        {
            _callbacksOnStart.Add(action);
            return this;
        }
        
        public DialogueLine AddEndCallBack(Action action)
        {
            _callbacksOnEnd.Add(action);
            return this;
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
        private DialogueLine? _curr;
        // A node doesn't know whether it's the end, so to end an interaction, we have a
        // separate set of callbacks.
        private List<Action> _onEndCallbacks = new();
        
        public Interaction(DialogueLine curr)
        {
            _curr = curr;
        }
        
        public DialogueLine? First()
        {
            if (_curr == null) return null;
            _curr.StartCallbacks();
            return _curr;
        }
        
        public DialogueLine? GetNext(string dialogueOption = Dialogue.DefaultDialogueOption)
        {
            if (_curr == null) return null;
            _curr = _curr.GetNext(dialogueOption);
            if (_curr == null)
            {
                EndCallbacks();
                return null;
            }
            return _curr;
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
            DialogueLine curr = new(author, "");
            DialogueLine? next = null;
            
            Interaction interaction = new(curr);
            foreach (var text in iterator)
            {
                if (text == null) return interaction;
                if (next != null)
                {
                    curr.AddNext(next);
                    curr = next;
                }
                curr.text = text;
                next = new DialogueLine(author, "");
            }

            return interaction;
        }
    }
}
