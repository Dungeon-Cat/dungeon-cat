using Scripts.Components.CommonEntities;
using Scripts.Components.Room1;
using Scripts.Components.UI;
using Scripts.Data;
using Scripts.Definitions.Items;
using UnityEngine;

namespace Scripts.Components
{
    public class Cat : EntityComponent<CatData>
    {
        protected override void Start()
        {
            base.Start();
            InputManager.Actions.Player.DropItem.performed += _ => data.DropAllItems();
            InvokeRepeating(nameof(SyncToData), 1f, 1f);
        }

        private bool seenControlsHint;

        public void Meow()
        {
            Debug.Log("Meow");

            if (!seenControlsHint)
            {
                SayControlsLine();
                seenControlsHint = true;
                return;
            }

            switch (GameStateManager.CurrentState.currentScene)
            {
                case "Room1":
                    var witch = FindObjectOfType<TutorialWitch>();
                    var chest = FindObjectOfType<ChestEntity>();
                    var door = FindObjectOfType<DoorEntity>();
                    if (!witch.dialogueTriggered)
                    {
                        SayLine("That statue over there looks like the Witch who summoned me.");
                    }
                    else if (!chest.IsOpen)
                    {
                        SayLine("Is that a chest over there?");
                    }
                    else if (!data.inventory.Contains<TutorialKeyFragment1>())
                    {
                        SayLine("I see something shiny over there by the well.");
                    }
                    else if (data.inventory.Contains<TutorialKeyFragment2>())
                    {
                        SayLine("I can combine these key fragments by dragging them together in my inventory.");
                    }
                    else if (data.inventory.Contains<TutorialKey>())
                    {
                        SayLine("I should be able to open the door now!");
                    }
                    else if (door.IsOpen)
                    {
                        SayLine("Into the dungeon I go!");
                    }
                    break;
                case "Room2":
                    break;
                case "Room3":
                    break;
            }

        }

        private static void SayControlsLine() =>
            SayLine(InputManager.playerInput.currentControlScheme == InputManager.Keyboard
                ? "Use WASD to move, and Space to interact with objects."
                : "Tap on the ground to move, and tap on objects to interact with them.");



        private static void SayLine(string text)
        {
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                new DialogueLine("Cat", text)
                    .AuthorColor(Color.blue)
                    .TextColor(Color.white)
            ));
        }
    }
}