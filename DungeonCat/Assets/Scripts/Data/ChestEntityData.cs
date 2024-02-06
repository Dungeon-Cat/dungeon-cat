using System;
namespace Scripts.Data
{
    /// <summary>
    /// Data for a Chest entity that can contain items
    /// </summary>
    [Serializable]
    public class ChestEntityData : EntityData
    {
        public ContainerData container;
    }
}