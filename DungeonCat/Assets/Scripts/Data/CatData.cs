using System;

namespace Scripts.Data
{
    /// <summary>
    /// The stored data for the player controlled character
    /// </summary>
    [Serializable]
    public class CatData : CharacterData
    {
        public const int MaxInventorySlots = 10;

        public ContainerData inventory = new(MaxInventorySlots);

        public bool TryPickupItem(ItemData item)
        {
            if (inventory.TryAdd(item))
            {
                // TODO trigger an event
                return true;
            }

            return false;
        }
    }
}