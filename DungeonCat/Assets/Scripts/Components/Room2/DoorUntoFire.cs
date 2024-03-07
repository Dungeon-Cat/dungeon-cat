using System;
using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using Scripts.Data;
using Scripts.Definitions.Items;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class DeathlyDoor : DoorEntity
    {
        protected void OnCollision2DEnter(Collision2D other)
        {
            UnityState.Instance.dialogue.StartInteraction(new Interaction(
                    new DialogueLine("Statue", "I am Tiresias. The psychopomp thinks to send a little cat hence?").AuthorColor(Color.red)
                        .AddDefault(new DialogueLine("Tiresias", "Do not trust everything you hear.").AuthorColor(Color.red)
            )).AddEndCallback(() =>
            {
                // reset level two
                GameStateManager.SwitchScene("Room2", new Vector2 {x = 0, y = -50});
                // TiresiasStatue.Instance.dialogueTriggered = false;
                HermaicAltar.Instance.alreadyTriggered = false;
            }));
        }
    }
    
    public class DoorUntoFire : MonoBehaviour, IInteractable
    {
        public DoorEntity door;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TiresiasStatue.Instance.dialogueTriggered)
            {
                if (!door.IsOpen)
                {
                    door.SetOpen(true);
                }
            }
        }
        
        public bool CanBeInteractedWith() => !door.IsOpen && TiresiasStatue.Instance.dialogueTriggered;

        public void Interact()
        {
            door.SetOpen(true);
        }
    }
}