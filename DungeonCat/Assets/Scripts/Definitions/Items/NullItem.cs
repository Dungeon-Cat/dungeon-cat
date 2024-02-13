namespace Scripts.Definitions.Items
{
    /// <summary>
    /// Fallback item if an Id is ever null
    /// </summary>
    public class NullItem : ItemDef
    {
        public override string Icon => "Images/Items/barrier";
    }
}