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
        public TMP_Text text;
        public Queue<DialogueEntry> DialogueEntries = new();
    }

    [Serializable]
    public struct DialogueEntry
    {
        public String author;
        public String text;

        public DialogueEntry(String author, String text)
        {
            this.author = author;
            this.text = text;
        }
    }
}