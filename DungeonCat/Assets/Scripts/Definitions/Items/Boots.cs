using Scripts.Data;

namespace Scripts.Definitions.Items
{
    public class Boots : EquipmentDef
    {
        public static readonly string FlyingTag = "Flying";
        public override void OnPickup(CharacterData character)
        {
            character.tags.Add(FlyingTag);
        }

        public override void OnDrop(CharacterData character)
        {
            character.tags.Remove(FlyingTag);
        }
    }
}