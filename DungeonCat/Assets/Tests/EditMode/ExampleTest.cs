using System.Collections;
using NUnit.Framework;
using Scripts.Data;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class ExampleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ExampleTestSimplePasses()
        {
            // Use the Assert class to test conditions
            var cat = new CatData
            {
                position = new Vector2(0, 0)
            };

            Assert.AreEqual(cat.position.x, 0);
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