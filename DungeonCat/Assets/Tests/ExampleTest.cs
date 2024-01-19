using System.Collections;
using System.Numerics;
using Model;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class ExampleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ExampleTestSimplePasses()
        {
            // Use the Assert class to test conditions
            var cat = new Cat
            {
                Position = new Vector2(0, 0)
            };

            Assert.AreEqual(cat.x, 0);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ExampleTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}