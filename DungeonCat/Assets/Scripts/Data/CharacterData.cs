using System;
using UnityEngine;
namespace Scripts.Data
{
    /// <summary>
    /// A character is an entity that 
    /// </summary>
    [Serializable]
    public class CharacterData : EntityData
    {
        public int hp;

        public Vector2 facing = Vector2.up;
    }
}