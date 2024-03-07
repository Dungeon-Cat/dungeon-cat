using System;
using System.Collections.Generic;
namespace Scripts.Data
{
    /// <summary>
    /// Data for a scene, including the entities and tags for it
    /// </summary>
    [Serializable]
    public class SceneData : Data
    {
        public Dictionary<string, EntityData> entities = new();
        public Dictionary<string, bool> flags = new();
    }
}