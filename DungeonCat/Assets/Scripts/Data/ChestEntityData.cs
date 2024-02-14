using System;
using UnityEngine;
namespace Scripts.Data
{
    /// <summary>
    /// Data for a Chest entity that can contain items
    /// </summary>
    [Serializable]
    public class ChestEntityData : OpenableEntityData
    {
        public ContainerData container;

        public void DropContents()
        {
            container.DropAllItems(position + new Vector2(0, 10));
        }
    }
}