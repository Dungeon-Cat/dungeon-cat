using System;
using System.Collections.Generic;
using Scripts.Components;
using TMPro;

namespace Scripts.Data
{
    /// <summary>
    /// The player controlled character
    /// </summary>
    [Serializable]
    public class DialogueData : EntityData
    {
        public TMP_Text author_vis;
        public TMP_Text text_vis;
        public Queue<Interaction> Interactions = new();
    }
}