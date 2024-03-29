﻿using System.Collections.Generic;
using Scripts.Data;

namespace Scripts.Definitions
{
    /// <summary>
    /// Class that defines a particular type of item
    /// </summary>
    public abstract class ItemDef
    {
        public virtual string Id => GetType().Name;

        public virtual int StackSize => 64;

        public virtual string Icon => "Unknown";

        public virtual bool IsConsumable => false;

        public virtual string DisplayName => Id;

        public virtual string Description => "";

        public virtual IEnumerable<ItemCombination> AddCombinations()
        {
            yield break;
        }
        
        public virtual void OnPickup(CharacterData character)
        {
            // Do Nothing
        }

        public virtual void OnDrop(CharacterData character)
        {
            // Do Nothing
        }

        public bool isMaterial;
    }
}