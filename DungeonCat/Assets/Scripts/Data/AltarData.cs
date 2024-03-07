using System;
namespace Scripts.Data
{
    /// <summary>
    /// Data that stores the active state of 4 runes
    /// </summary>
    [Serializable]
    public class AltarData : EntityData
    {
        public const int NumRunes = 4;

        public bool[] runes = new bool[NumRunes];
    }
}