using Scripts.Components.CommonEntities;
using Scripts.Components.Room1;
using Scripts.Components.Room2;
using Scripts.Components.Room3;
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

        private bool seenFirstRoom2Hint;
        private bool seenFirstRoom3Hint;

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
                    else
                    {
                        SayLine("I need to find a way to get this door open.");
                    }
                    break;
                case "Room2":
                    var altarChest = FindObjectOfType<AltarChest>();
                    var bootCount = data.inventory.Find<Boots>();

                    if (!seenFirstRoom2Hint)
                    {
                        SayLine("It looks like items are heavy enough to activate these altars");
                        seenFirstRoom2Hint = true;
                    } 
                    else if (bootCount > 0)
                    {
                        SayLine("With these boots I can fly to the other side");
                    } else if (!altarChest.chest.IsOpen)
                    {
                        SayLine("Maybe that chest will open once all 4 runes are lit up.");
                    }
                    else
                    {
                        SayLine("Seems like I need both of those pillars lit up to open the door.");
                    }
                    
                    break;
                case "Room3":
                    var statue = FindObjectOfType<YarnStatue>();

                    if (!seenFirstRoom3Hint)
                    {
                        SayLine("It looks like I need to get the yarn to that altar over there.");
                        seenFirstRoom3Hint = true;
                    } else if (!statue.talkedTo)
                    {
                        SayLine("Hopefully the witch can help me if I ever get the yarn stuck.");
                    }
                    else
                    {
                        SayLine("I really don't want to get the yarn stuck in a corner.");
                    }
                    
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