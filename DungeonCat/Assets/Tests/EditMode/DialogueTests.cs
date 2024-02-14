using NUnit.Framework;
using Scripts.Components;

namespace Tests.EditMode
{
    public class DialogueTests
    {
        [Test]
        public void BasicInteraction()
        {
            var interaction = Interaction.Basic("cat", new[] { "a", "b", "c", "d" });
            var line = interaction.First();
            if (line == null) Assert.Fail();
            Assert.AreEqual(line.author, "cat");
            Assert.AreEqual(line.text, "a");

            line = interaction.GetNext();
            if (line == null) Assert.Fail();
            Assert.AreEqual(line.author, "cat");
            Assert.AreEqual(line.text, "b");
            
            line = interaction.GetNext();
            if (line == null) Assert.Fail();
            Assert.AreEqual(line.author, "cat");
            Assert.AreEqual(line.text, "c");
            
            line = interaction.GetNext();
            if (line == null) Assert.Fail();
            Assert.AreEqual(line.author, "cat");
            Assert.AreEqual(line.text, "d");

            var final = interaction.GetNext();
            Assert.AreEqual(null, final);
            
            final = interaction.GetNext();
            Assert.AreEqual(null, final);
        }

        public void BasicDialogue()
        {
            var interaction = Interaction.Basic("cat", new[] { "a", "b", "c", "d" });
            var dialogue = new Dialogue();
            dialogue.StartInteraction(interaction);
        }
    }
}