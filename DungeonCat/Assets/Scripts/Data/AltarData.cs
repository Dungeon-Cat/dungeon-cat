using System;
namespace Scripts.Data
{
    [Serializable]
    public class AltarData : EntityData
    {
        public const int NumRunes = 4;

        public bool[] runes = new bool[NumRunes];
    }
}