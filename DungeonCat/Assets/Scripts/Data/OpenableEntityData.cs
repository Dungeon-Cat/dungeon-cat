using System;
namespace Scripts.Data
{
    /// <summary>
    /// Data for a door that can be open or closed
    /// </summary>
    [Serializable]
    public class OpenableEntityData : EntityData
    {
        public bool isOpen;
    }
}