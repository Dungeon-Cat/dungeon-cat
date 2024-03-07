using System;
using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using Scripts.Data;
using Scripts.Definitions.Items;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class HermaicAltar : AltarEntity, IInteractable
    {
        public static HermaicAltar Instance { get; private set; }

        public HermaicAltar()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        public ContainerData containerData = new(1);

        public bool alreadyTriggered;

        protected override void Start()
        {
            Instance = this;
            // if (!GameStateManager.CurrentState.currentScene.Equals("Room2"))
            // {
            //     GameStateManager.SwitchScene("Room2", new Vector2{x = 0, y = -50});
            // }
            containerData.TryAddToSlot(ItemData.Create<Boots>(), 0);
        }

        public bool CanBeInteractedWith()
        {
            if (alreadyTriggered) return false;
            var result = true;
            foreach (var rune in data.runes)
            {
                result &= rune;
                if (!result) return false;
            }
            return true;
        }
        
        public void Interact()
        {
            if (!alreadyTriggered)
            {
                alreadyTriggered = true;
                UnityState.Instance.dialogue.StartInteraction(new Interaction(
                    new DialogueLine("Cat", "You see a set of weathered boots appear before you.").AuthorColor(Color.blue)
                        .AddDefault(new DialogueLine("Cat",
                                "All of a sudden, little wings sprout from their heels and begin flapping furiously.").AuthorColor(Color.blue)
                            .AddDefault(new DialogueLine("Cat",
                                "A little emblem bearing the mark of the caduceus lights up.").AuthorColor(Color.blue)
                ))));
            }

            containerData.DropAllItems(data.position + new Vector2(0, 10));
        }
    }
}