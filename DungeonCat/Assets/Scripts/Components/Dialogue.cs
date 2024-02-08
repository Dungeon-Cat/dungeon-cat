using System;
using Scripts.Data;

namespace Scripts.Components
{
    public class Dialogue : EntityComponent<DialogueData>
    {
        public new void Awake()
        {
            base.Awake();
            data.text.text = "";
            gameObject.SetActive(true);
        }

        public void Push(String author, String text)
        {
            gameObject.SetActive(true);
            data.DialogueEntries.Enqueue(new DialogueEntry (author, author));
            data.text.text = data.DialogueEntries.Peek().text;
        }

        public void Cycle()
        {
            data.DialogueEntries.Dequeue();
            if (data.DialogueEntries.Count == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}