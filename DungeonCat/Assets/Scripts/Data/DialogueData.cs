using System;
using System.Collections.Generic;
using Scripts.Components.UI;
using TMPro;

namespace Scripts.Data
{
    /// <summary>
    /// Data for a given dialogue
    /// </summary>
    [Serializable]
    public class DialogueData : EntityData
    {
        public TMP_Text authorVis, textVis, option1, option2, option3, option4;
        public Queue<Interaction> Interactions = new();
    }
}