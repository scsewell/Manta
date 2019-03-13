using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manta.Tests
{
    [TestClass]
    public class Vector4Tests
    {
        private static void ApproxEqual(Vector4 a, Vector4 b, float tolerance = 0.0001f)
        {
            Assert.AreEqual(a.x, b.x, tolerance);
            Assert.AreEqual(a.y, b.y, tolerance);
            Assert.AreEqual(a.z, b.z, tolerance);
            Assert.AreEqual(a.w, b.w, tolerance);
        }

        [TestMethod]
        public void TestIndexer()
        {
            Vector4 v0 = new Vector4(-3.0f, 5.3f, 4.1f, -2.2f);
            Assert.AreEqual(v0.x, v0[0]);
            Assert.AreEqual(v0.y, v0[1]);
            Assert.AreEqual(v0.z, v0[2]);
            Assert.AreEqual(v0.w, v0[3]);
        }

        [TestMethod]
        public void TestLength()
        {
            Assert.AreEqual(7.34847f, new Vector4(-3f, 5f, 4f, -2f).Length, 0.0001f);
        }

        [TestMethod]
        public void TestLengthFast()
        {
            Assert.AreEqual(7.34847f, new Vector4(-3f, 5f, 4f, -2f).LengthFast, 0.001f);
        }

        [TestMethod]
        public void TestLengthSquared()
        {
            Assert.AreEqual(54f, new Vector4(-3f, 5f, 4f, -2f).LengthSquared, 0.0001f);
        }

        [TestMethod]
        public void TestNormalize()
        {
            ApproxEqual(new Vector4(-0.408248f, 0.680414f, 0.544331f, -0.272166f), new Vector4(-3f, 5f, 4f, -2f).Normalized, 0.0001f);
        }

        [TestMethod]
        public void TestNormalizeFast()
        {
            ApproxEqual(new Vector4(-0.408248f, 0.680414f, 0.544331f, -0.272166f), new Vector4(-3f, 5f, 4f, -2f).NormalizedFast, 0.001f);
        }

        [TestMethod]
        public void TestComponentMin()
        {
            var v0 = new Vector4(-3f, 5f, 4f, -2f);
            var v1 = new Vector4(4f, 0f, -2f, -9f);
            Assert.AreEqual(new Vector4(-3f, 0f, -2f, -9f), Vector4.ComponentMin(v0, v1));
        }

        [TestMethod]
        public void TestComponentMax()
        {
            var v0 = new Vector4(-3f, 5f, 4f, -2f);
            var v1 = new Vector4(4f, 0f, -2f, -9f);
            Assert.AreEqual(new Vector4(4f, 5f, 4f, -2f), Vector4.ComponentMax(v0, v1));
        }

        [TestMethod]
        public void TestComponentClamp()
        {
            var v0 = new Vector4(-3f, 2f, -1f, -2f);
            var v1 = new Vector4(4f, 0f, -2f, -9f);
            var v2 = new Vector4(6f, 1f, 0f, 18f);
            Assert.AreEqual(new Vector4(4f, 1f, -1f, -2f), Vector4.ComponentClamp(v0, v1, v2));
        }

        [TestMethod]
        public void TestMin()
        {
            var v0 = new Vector4(-3f, 5f, 4f, -2f);
            var v1 = new Vector4(4f, 50f, -2f, -9f);
            Assert.AreEqual(v0, Vector4.Min(v0, v1));
        }

        [TestMethod]
        public void TestMax()
        {
            var v0 = new Vector4(-3f, 5f, 4f, -2f);
            var v1 = new Vector4(4f, 50f, -2f, -9f);
            Assert.AreEqual(v1, Vector4.Max(v0, v1));
        }

        [TestMethod]
        public void TestMagnitudeClamp()
        {
            var v0 = new Vector4(-3f, 5f, 4f, -2f);
            ApproxEqual(v0, Vector4.MagnitudeClamp(v0, 1000f));
            ApproxEqual(new Vector4(-0.816497f, 1.36083f, 1.08866f, -0.544331f), Vector4.MagnitudeClamp(v0, 2.0f));
        }

        [TestMethod]
        public void TestAdd()
        {
            var v0 = new Vector4(-3f, 5f, 4f, -2f);
            var v1 = new Vector4(4f, 0f, -2f, -9f);
            ApproxEqual(new Vector4(1f, 5f, 2f, -11f), Vector4.Add(v0, v1));
        }

        [TestMethod]
        public void TestSubtract()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -9f);
            ApproxEqual(new Vector4(-7f, 2f, 1f, 7f), Vector4.Subtract(v0, v1));
        }

        [TestMethod]
        public void TestNegate()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            ApproxEqual(new Vector4(3f, -2f, 1f, 2f), Vector4.Negate(v0));
        }

        [TestMethod]
        public void TestMultiplyScalar()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            ApproxEqual(new Vector4(-6f, 4f, -2f, -4f), Vector4.Multiply(v0, 2.0f));
        }

        [TestMethod]
        public void TestMultiplyVector()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -9f);
            ApproxEqual(new Vector4(-12f, 0f, 2f, 18f), Vector4.Multiply(v0, v1));
        }

        [TestMethod]
        public void TestDivideScalar()
        {
            Vector4 v0 = new Vector4(-6f, 4f, -2f, -4f);
            ApproxEqual(new Vector4(-3f, 2f, -1f, -2f), Vector4.Divide(v0, 2.0f));
        }

        [TestMethod]
        public void TestDivideVector()
        {
            Vector4 v0 = new Vector4(-12f, 0f, 2f, 18f);
            Vector4 v1 = new Vector4(4f, 1f, -2f, -9f);
            ApproxEqual(new Vector4(-3f, 0f, -1f, -2f), Vector4.Divide(v0, v1));
        }

        [TestMethod]
        public void TestDot()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -9f);
            Assert.AreEqual(8f, Vector4.Dot(v0, v1));
        }

        [TestMethod]
        public void TestProject()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -9f);
            ApproxEqual(new Vector4(0.316832f, 0f, -0.158416f, -0.71287f), Vector4.Project(v0, v1));
        }

        [TestMethod]
        public void TestDistance()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -9f);
            Assert.AreEqual(10.1489f, Vector4.Distance(v0, v1), 0.0001f);
        }

        [TestMethod]
        public void TestDistanceSquared()
        {
            Vector4 v0 = new Vector4(-3f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -9f);
            Assert.AreEqual(103f, Vector4.DistanceSquared(v0, v1));
        }

        [TestMethod]
        public void TestLerp()
        {
            Vector4 v0 = new Vector4(0f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -18f);
            ApproxEqual(new Vector4(1f, 1.5f, -1.25f, -6f), Vector4.Lerp(v0, v1, 0.25f));
        }

        [TestMethod]
        public void TestLerpClamped()
        {
            Vector4 v0 = new Vector4(0f, 2f, -1f, -2f);
            Vector4 v1 = new Vector4(4f, 0f, -2f, -18f);
            ApproxEqual(v0, Vector4.LerpClamped(v0, v1, -1f));
            ApproxEqual(v1, Vector4.LerpClamped(v0, v1, 2f));
            ApproxEqual(new Vector4(1f, 1.5f, -1.25f, -6f), Vector4.LerpClamped(v0, v1, 0.25f));
        }

        [TestMethod]
        public void TestMoveTowards()
        {
            Vector4 v0 = new Vector4(1f, 1f, 1f, 1f);
            Vector4 v1 = new Vector4(2f, 2f, 2f, 2f);
            ApproxEqual(v0, Vector4.MoveTowards(v0, v1, 0f));
            ApproxEqual(v1, Vector4.MoveTowards(v0, v1, 5f));
            ApproxEqual(new Vector4(1.25f, 1.25f, 1.25f, 1.25f), Vector4.MoveTowards(v0, v1, 0.5f));
        }
    }
}
