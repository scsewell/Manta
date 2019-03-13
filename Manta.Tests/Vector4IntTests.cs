using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manta.Tests
{
    [TestClass]
    public class Vector4IntTests
    {
        [TestMethod]
        public void TestIndexer()
        {
            Vector4Int v0 = new Vector4Int(-3, 5, 4, -2);
            Assert.AreEqual(v0.x, v0[0]);
            Assert.AreEqual(v0.y, v0[1]);
            Assert.AreEqual(v0.z, v0[2]);
            Assert.AreEqual(v0.w, v0[3]);
        }

        [TestMethod]
        public void TestLength()
        {
            Assert.AreEqual(7.34847f, new Vector4Int(-3, 5, 4, -2).Length, 0.0001f);
        }

        [TestMethod]
        public void TestLengthSquared()
        {
            Assert.AreEqual(54, new Vector4Int(-3, 5, 4, -2).LengthSquared);
        }

        [TestMethod]
        public void TestComponentMin()
        {
            Vector4Int v0 = new Vector4Int(-3, 5, 4, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(new Vector4Int(-3, 0, -2, -9), Vector4Int.ComponentMin(v0, v1));
        }

        [TestMethod]
        public void TestComponentMax()
        {
            Vector4Int v0 = new Vector4Int(-3, 5, 4, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(new Vector4Int(4, 5, 4, -2), Vector4Int.ComponentMax(v0, v1));
        }

        [TestMethod]
        public void TestComponentClamp()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Vector4Int v2 = new Vector4Int(6, 1, 0, 18);
            Assert.AreEqual(new Vector4Int(4, 1, -1, -2), Vector4Int.ComponentClamp(v0, v1, v2));
        }

        [TestMethod]
        public void TestCiel()
        {
            Assert.AreEqual(new Vector4Int(0, 1, -2, 4), Vector4Int.Ceil(new Vector4(0.0f, 0.52f, -2.52f, 3.41f)));
        }

        [TestMethod]
        public void TestFloor()
        {
            Assert.AreEqual(new Vector4Int(0, 0, -3, 3), Vector4Int.Floor(new Vector4(0.0f, 0.52f, -2.52f, 3.41f)));
        }

        [TestMethod]
        public void TestRound()
        {
            Assert.AreEqual(new Vector4Int(0, 1, -3, 3), Vector4Int.Round(new Vector4(0.0f, 0.52f, -2.52f, 3.41f)));
        }

        [TestMethod]
        public void TestAdd()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(new Vector4Int(1, 2, -3, -11), Vector4Int.Add(v0, v1));
        }

        [TestMethod]
        public void TestSubtract()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(new Vector4Int(-7, 2, 1, 7), Vector4Int.Subtract(v0, v1));
        }

        [TestMethod]
        public void TestNegate()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Assert.AreEqual(new Vector4Int(3, -2, 1, 2), Vector4Int.Negate(v0));
        }

        [TestMethod]
        public void TestScale()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Assert.AreEqual(new Vector4Int(-9, 6, -3, -6), Vector4Int.Scale(v0, 3));
        }

        [TestMethod]
        public void TestMultiply()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(new Vector4Int(-12, 0, 2, 18), Vector4Int.Multiply(v0, v1));
        }

        [TestMethod]
        public void TestDistance()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(10.1489f, Vector4Int.Distance(v0, v1), 0.0001f);
        }

        [TestMethod]
        public void TestDistanceSquared()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Vector4Int v1 = new Vector4Int(4, 0, -2, -9);
            Assert.AreEqual(103, Vector4Int.DistanceSquared(v0, v1));
        }

        [TestMethod]
        public void TestEquals()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Assert.IsTrue(new Vector4Int(-3, 2, -1, -2).Equals(v0));
        }

        [TestMethod]
        public void TestVector4Cast()
        {
            Vector4Int v0 = new Vector4Int(-3, 2, -1, -2);
            Assert.AreEqual(new Vector4(-3, 2, -1, -2), (Vector4)v0);
        }
    }
}
