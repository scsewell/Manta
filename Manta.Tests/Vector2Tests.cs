using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manta.Tests
{
    [TestClass]
    public class Vector2Tests
    {
        private static void ApproxEqual(Vector2 a, Vector2 b, float tolerance = 0.0001f)
        {
            Assert.AreEqual(a.x, b.x, tolerance);
            Assert.AreEqual(a.y, b.y, tolerance);
        }

        [TestMethod]
        public void TestIndexer()
        {
            Vector4 v0 = new Vector4(-3.0f, 5.3f, 4.1f, -2.2f);
            Assert.AreEqual(v0.x, v0[0]);
            Assert.AreEqual(v0.y, v0[1]);
        }
    }
}
