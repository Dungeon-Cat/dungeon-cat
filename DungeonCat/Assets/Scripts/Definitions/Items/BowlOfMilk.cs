namespace Scripts.Definitions.Items
{
    public class BowlOfMilk : ItemDef
    {
        public override string Icon => "Images/Items/milk_bucket";
        
        public override string Description => "Yeah, milk isn't actually good for cats in real life, we know.";

        public override bool IsConsumable => true;
    }
}