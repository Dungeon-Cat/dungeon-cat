using System;
using System.Collections.Generic;
namespace Scripts.Data
{
    [Serializable]
    public class SceneData
    {
        public List<EntityData> entities = new();
    }
}