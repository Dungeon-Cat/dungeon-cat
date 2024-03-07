using Scripts.Data;
using UnityEngine;

namespace Scripts.Definitions.Items
{
    public class Boots : EquipmentDef
    {
        public override string Icon => "Images/Items/hermes_boots";

        public static readonly string FlyingTag = "Flying";
        
        public override void OnPickup(CharacterData character)
        {
            Debug.Log("If cats could fly");
            character.AddTag(FlyingTag);
        }

        public override void OnDrop(CharacterData character)
        {
            character.RemoveTag(FlyingTag);
        }
    }
}