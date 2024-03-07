using System;
using System.Collections.Generic;
namespace Scripts.Data
{
    [Serializable]
    public class SceneData : Data
    {
        public Dictionary<string, EntityData> entities = new();
        public Dictionary<string, bool> flags = new();
    }
}