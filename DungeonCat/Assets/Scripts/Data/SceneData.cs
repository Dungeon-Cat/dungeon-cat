using System;
using System.Collections.Generic;
namespace Scripts.Data
{
    [Serializable]
    public class SceneData
    {
        public Dictionary<string, EntityData> entities = new();
    }
}