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
                    .AddNext(
                        Line("Welcome to the magical world of Dungeon Cat."),
                        "Alright, sounds interesting...",
                        Color.black
                    )
                    .AddNext(
                        Line("Hiss....."),
                        "I don't want to do this anymore...",
                        Color.black
                    ).AddNext(
                        Line("I am a noncommittal cat, you see...")
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