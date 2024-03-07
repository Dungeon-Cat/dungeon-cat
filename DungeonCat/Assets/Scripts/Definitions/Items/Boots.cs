using Scripts.Data;

namespace Scripts.Definitions.Items
{
    /// <summary>
    /// Boots that allow the player character to traverse abyss tiles
    /// </summary>
    public class Boots : EquipmentDef
    {
        public override string Icon => "Images/Items/hermes_boots";

        public static readonly string FlyingTag = "Flying";
        
        public override void OnPickup(CharacterData character)
        {
            character.AddTag(FlyingTag);
        }

        public override void OnDrop(CharacterData character)
        {
            character.RemoveTag(FlyingTag);
        }
    }
}