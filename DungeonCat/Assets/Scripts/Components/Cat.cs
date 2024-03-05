using Pathfinding;
using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class Cat : EntityComponent<CatData>
    {
        protected override void Start()
        {
            base.Start();
            InputManager.Actions.Player.DropItem.performed += _ => data.DropAllItems();
        }
        
        public void Meow()
        {
            Debug.Log("Meow");
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                Line("I'm Momo!")
                    .AddOption(
                        "Alright, sounds interesting...",
                        Line("Welcome to the magical world of Dungeon Cat.")
                    )
                    .AddOption(
                        "I don't want to do this anymore...",
                        Line("Hiss.....")
                    )
            ));
        }

        private static DialogueLine Line(string text)
        {
            return new DialogueLine(
                    "Cat",
                    text
                ).AuthorColor(Color.blue)
                .TextColor(Color.white);
        }
    }
}