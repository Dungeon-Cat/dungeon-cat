using Scripts.Data;

namespace Scripts.Definitions.Items
{
    public class Boots : EquipmentDef
    {
        public override void OnPickup(CharacterData character)
        {
            character.tags.Add("Flying");
        }

        public override void OnDrop(CharacterData character)
        {
            character.tags.Remove("Flying");
        }
    }
}