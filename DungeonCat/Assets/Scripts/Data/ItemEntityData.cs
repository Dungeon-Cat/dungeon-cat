using System;
namespace Scripts.Data
{
    /// <summary>
    /// Data for an Item Entity, namely the ItemData of the Item it represents
    /// </summary>
    [Serializable]
    public class ItemEntityData : EntityData
    {
        public ItemData item;
    }
}