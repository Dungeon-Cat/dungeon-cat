using Scripts.Components.CommonEntities;
using Scripts.Components.UI;
using Scripts.Definitions.Items;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class DeathlyDoor : DoorEntity
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (IsOpen && other.gameObject.HasComponent(out Cat _))
            {
                UnityState.Instance.dialogue.StartInteraction(new Interaction(
                    new DialogueLine("Statue", "I am Tiresias. The psychopomp thinks to send a little cat hence?")
                        .AuthorColor(Color.red)
                        .AddDefault(
                            new DialogueLine("Tiresias", "Do not trust everything you hear.").AuthorColor(Color.red)
                        )).AddEndCallback(() =>
                {
                    // reset level two
                    // TiresiasStatue.Instance.dialogueTriggered = false;
                    base.OnCollisionEnter2D(other);
                    HermaicAltar.Instance.alreadyTriggered = false;
                    SetOpen(true);
                    var cat = UnityState.Instance.cat.data;
                    cat.inventory.ClearSlot(cat.inventory.Find<Boots>());
                    cat.tags.Remove(Boots.FlyingTag);
                    Debug.Log("Boots have been cleared, in endcallback after superclass transfer.");
                }));
            }
            
        }
    }
}