using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Data;
using TMPro;
using UnityEngine;
using Scripts.Components.CommonEntities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#nullable enable

namespace Scripts.Components.UI
{
    /// <summary>
    /// Dialogue that is presented to the player, with the ability to choose dialogue options
    /// </summary>
    public class Dialogue : MonoBehaviour
    {
        public TMP_Text authorVis, textVis, option1, option2, option3, option4, instructions;
        public Queue<Interaction> Interactions = new();

        private Interaction? _currentInteraction;
        private IEnumerator? _coroutine;
        private bool _inInteractionChain, _displayingIncrementally, _skipDisplay, _enableBoxSelection;
        private readonly List<TMP_Text> _dialogueOptions = new();
        public const string DefaultDialogueOption = "";
        private int _boxToProgress = -1;

        private bool _dialogueClicked;

        public void Awake()
        {
            gameObject.SetActive(false);
            textVis.text = "";
            authorVis.text = "";
            _dialogueOptions.Add(option1);
            _dialogueOptions.Add(option2);
            _dialogueOptions.Add(option3);
            _dialogueOptions.Add(option4);
            foreach (var tmpText in _dialogueOptions)
            {
                tmpText.text = "";
                tmpText.enabled = false;
            }
        }

        private void Start()
        {
            if (InputManager.playerInput.currentControlScheme != InputManager.Keyboard)
            {
                instructions.SetText("Tap to continue...");
            }
        }

        private bool DialogueInteractedWith() => InputManager.Actions.Player.Interact.WasPressedThisFrame() || _dialogueClicked;

        public void OnDialogueClicked() => _dialogueClicked = true;

        public void Update()
        {
            if (_displayingIncrementally && DialogueInteractedWith())
            {
                _skipDisplay = true;
            }
        }

        private void LateUpdate()
        {
            _dialogueClicked = false;
        }

        public void StartInteraction(Interaction interaction)
        {
            if (_currentInteraction == null)
            {
                _currentInteraction = interaction;
            }
            else
            {
                Interactions.Enqueue(interaction);
            }
            if (_inInteractionChain) return;

            _inInteractionChain = true;
            CurrentInteraction();
        }

        public void ProgressBox(int i)
        {
            if (!_enableBoxSelection) return;

            _boxToProgress = i;
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

        private void ProgressInteractionWithOption(string dialogueOption)
        {
            if (_currentInteraction == null) return;

            var result = _currentInteraction.GetNext(dialogueOption);

            if (result == null)
            {
                _currentInteraction = Interactions.Count > 0 ? Interactions.Dequeue() : null;
                CurrentInteraction();
                return;
            }
            Display(result);
        }

        private void ProgressInteractionWithDefault()
        {
            if (_currentInteraction == null) return;

            var result = _currentInteraction.GetNext();

            if (result == null)
            {
                _currentInteraction = Interactions.Count > 0 ? Interactions.Dequeue() : null;
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
            authorVis.text = line.author;
            authorVis.color = line.authorColor;
            textVis.text = "";
            textVis.color = line.textColor;

            _displayingIncrementally = true;
            foreach (var letter in line.text)
            {
                textVis.text += letter;
                yield return new WaitForSeconds(0.05f);

                if (_skipDisplay) break;
            }
            _skipDisplay = false;
            _displayingIncrementally = false;
            textVis.text = line.text;
            yield return new WaitForSeconds(0.05f);

            var next = DefaultDialogueOption;
            if (line.HasOptions())
            {
                for (var i = 0; i < line.Options.Count; i++)
                {
                    _dialogueOptions[i].enabled = true;
                    var (option, color) = line.Options[i];
                    _dialogueOptions[i].text = option;
                    _dialogueOptions[i].color = color;
                }
                _enableBoxSelection = true;
                // temporarily enable box selection
                yield return new WaitUntil(() => _boxToProgress >= 0 && _boxToProgress < line.Options.Count);

                _enableBoxSelection = false;

                next = _dialogueOptions[_boxToProgress].text;
                _boxToProgress = -1;
            }
            else
            {
                yield return new WaitUntil(DialogueInteractedWith);

                _dialogueOptions[0].enabled = true;
                (_dialogueOptions[0].text, _dialogueOptions[0].color) = line.GetDefaultOption().GetValueOrDefault(("", Color.black));
            }

            yield return new WaitForSeconds(0.10f);

            foreach (var option in _dialogueOptions)
            {
                option.text = "";
                option.color = Color.black;
                option.enabled = false;
            }

            textVis.text = "";
            textVis.color = Color.white;
            authorVis.text = "";
            textVis.color = Color.white;

            if (line.HasOptions())
            {
                ProgressInteractionWithOption(next);
            }
            else
            {
                ProgressInteractionWithDefault();
            }
        }
    }

    /// <summary>
    /// A class representing a line of dialogue. A fluent interface largely following the
    /// configuration pattern -- the class can be configured via a series of chained methods
    /// and then passed to Interaction, which will take responsibility for wrapping the
    /// interface.
    /// </summary>
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
        public List<(string, Color)> Options { get; } = new();

        private Dictionary<string, DialogueLine>? _options;
        private ((string, Color), DialogueLine)? _defaultNext;
        // A node in an interaction takes these actions when it is reached.
        // This is to signal choices that might affect the character with whom the
        // dialogue is being taken.
        private List<Action> _callbacksOnStart = new(), _callbacksOnEnd = new();

        public bool HasOptions()
        {
            return _options != null;
        }

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

        public DialogueLine AddOption(string option, DialogueLine node, Color color)
        {
            if (_defaultNext != null)
            {
                throw new Exception("Invalid state, attempting to add a dialogue option to a non-null node.");
            }

            _options ??= new();
            Options.Add((option, color));
            _options.Add(option, node);
            return this;
        }

        public DialogueLine AddOption(string option, DialogueLine node)
        {
            return AddOption(option, node, Color.black);
        }

        public DialogueLine AddDefault(DialogueLine node)
        {
            return AddDefault("", node);
        }

        public DialogueLine AddDefault(string option, DialogueLine node)
        {
            return AddDefault(option, node, Color.black);
        }

        public DialogueLine AddDefault(string option, DialogueLine node, Color color)
        {
            if (_defaultNext != null)
            {
                throw new Exception("Invalid state, attempting to overwrite immutably set default next line.");
            }

            _defaultNext = ((option, color), node);
            return this;
        }

        public DialogueLine? GetNext(string option)
        {
            if (_options == null) return null;

            EndCallbacks();
            _options.TryGetValue(option, out var next);
            if (next == null) return next;

            next.StartCallbacks();
            return next;
        }

        public DialogueLine? GetNext()
        {
            return _defaultNext?.Item2;
        }

        public (string, Color)? GetDefaultOption()
        {
            return _defaultNext?.Item1;
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

            _curr = _curr.HasOptions() ? _curr.GetNext(dialogueOption) : _curr.GetNext();
            if (_curr != null) return _curr;

            EndCallbacks();
            return null;
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
                    curr.AddDefault(next);
                    curr = next;
                }
                curr.text = text;
                next = new DialogueLine(author, "");
            }

            return interaction;
        }
    }
}