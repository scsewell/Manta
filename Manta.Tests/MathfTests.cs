using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manta.Tests
{
    [TestClass]
    public class MathfTests
    {
        [TestMethod]
        public void TestMinInt()
        {
            const int a0 = 14;
            const int b0 = -5;
            
            Assert.AreEqual(Mathf.Min(a0, b0), b0);
            Assert.AreEqual(Mathf.Min(b0, a0), b0);
        }
        
        [TestMethod]
        public void TestMinLong()
        {
            const long a0 = 14L;
            const long b0 = -5L;

            Assert.AreEqual(Mathf.Min(a0, b0), b0);
            Assert.AreEqual(Mathf.Min(b0, a0), b0);
        }

        [TestMethod]
        public void TestMinFloat()
        {
            const float a0 = 1.5f;
            const float b0 = -0.2f;

            Assert.AreEqual(Mathf.Min(a0, b0), b0);
            Assert.AreEqual(Mathf.Min(b0, a0), b0);
        }

        [TestMethod]
        public void TestMinDouble()
        {
            const double a0 = 1.5;
            const double b0 = -0.2;

            Assert.AreEqual(Mathf.Min(a0, b0), b0);
            Assert.AreEqual(Mathf.Min(b0, a0), b0);
        }

        [TestMethod]
        public void TestMaxInt()
        {
            const int a0 = 14;
            const int b0 = -5;

            Assert.AreEqual(Mathf.Max(a0, b0), a0);
            Assert.AreEqual(Mathf.Max(b0, a0), a0);
        }

        [TestMethod]
        public void TestMaxLong()
        {
            const long a0 = 14L;
            const long b0 = -5L;

            Assert.AreEqual(Mathf.Max(a0, b0), a0);
            Assert.AreEqual(Mathf.Max(b0, a0), a0);
        }

        [TestMethod]
        public void TestMaxFloat()
        {
            const float a0 = 1.5f;
            const float b0 = -0.2f;

            Assert.AreEqual(Mathf.Max(a0, b0), a0);
            Assert.AreEqual(Mathf.Max(b0, a0), a0);
        }

        [TestMethod]
        public void TestMaxDouble()
        {
            const double a0 = 1.5;
            const double b0 = -0.2;

            Assert.AreEqual(Mathf.Max(a0, b0), a0);
            Assert.AreEqual(Mathf.Max(b0, a0), a0);
        }

        [TestMethod]
        public void TestClampInt()
        {
            const int a0 = 14;
            const int b0 = -5;
            const int c0 = -20;

            Assert.AreEqual(Mathf.Clamp(a0, c0, b0), b0);
            Assert.AreEqual(Mathf.Clamp(b0, c0, a0), b0);
            Assert.AreEqual(Mathf.Clamp(c0, b0, a0), b0);
        }

        [TestMethod]
        public void TestClampLong()
        {
            const long a0 = 14L;
            const long b0 = -5L;
            const long c0 = -20L;

            Assert.AreEqual(Mathf.Clamp(a0, c0, b0), b0);
            Assert.AreEqual(Mathf.Clamp(b0, c0, a0), b0);
            Assert.AreEqual(Mathf.Clamp(c0, b0, a0), b0);
        }

        [TestMethod]
        public void TestClampFloat()
        {
            const float a0 = 1.5f;
            const float b0 = -0.2f;
            const float c0 = -5.0f;

            Assert.AreEqual(Mathf.Clamp(a0, c0, b0), b0);
            Assert.AreEqual(Mathf.Clamp(b0, c0, a0), b0);
            Assert.AreEqual(Mathf.Clamp(c0, b0, a0), b0);
        }

        [TestMethod]
        public void TestClampDouble()
        {
            const double a0 = 1.5;
            const double b0 = -0.2;
            const double c0 = -5.0;

            Assert.AreEqual(Mathf.Clamp(a0, c0, b0), b0);
            Assert.AreEqual(Mathf.Clamp(b0, c0, a0), b0);
            Assert.AreEqual(Mathf.Clamp(c0, b0, a0), b0);
        }

        [TestMethod]
        public void TestSqrtFloat()
        {
            const float a0 = 5.5f;
            const float b0 = -1f;

            Assert.AreEqual(Mathf.Sqrt(a0), MathF.Sqrt(a0));
            Assert.AreEqual(Mathf.Sqrt(b0), MathF.Sqrt(b0));
        }

        [TestMethod]
        public void TestSqrtDouble()
        {
            const double a0 = 5.5;
            const double b0 = -1.0;

            Assert.AreEqual(Mathf.Sqrt(a0), Math.Sqrt(a0));
            Assert.AreEqual(Mathf.Sqrt(b0), Math.Sqrt(b0));
        }
        [TestMethod]
        public void TestInvSqrtFastFloat()
        {
            const float a0 = 5.5f;
            const float b0 = -1f;

            Assert.AreEqual(Mathf.InvSqrtFast(a0), 1f / MathF.Sqrt(a0), a0 * 0.0001f);
            Assert.AreEqual(Mathf.InvSqrtFast(b0), 1f / MathF.Sqrt(b0));
        }

        [TestMethod]
        public void TestNextPowerOfTwoInt()
        {
            Assert.AreEqual(1, Mathf.NextPowerOfTwo(1));
            Assert.AreEqual(64, Mathf.NextPowerOfTwo(52));
            Assert.AreEqual(4096, Mathf.NextPowerOfTwo(4096));
            Assert.AreEqual(8192, Mathf.NextPowerOfTwo(4097));
        }

        [TestMethod]
        public void TestNextPowerOfTwoLong()
        {
            Assert.AreEqual(1L, Mathf.NextPowerOfTwo(1L));
            Assert.AreEqual(64L, Mathf.NextPowerOfTwo(52L));
            Assert.AreEqual(4096L, Mathf.NextPowerOfTwo(4096L));
            Assert.AreEqual(8192L, Mathf.NextPowerOfTwo(4097L));
        }

        [TestMethod]
        public void TestNextPowerOfTwoFloat()
        {
            Assert.AreEqual(0f, Mathf.NextPowerOfTwo(0f));
            Assert.AreEqual(0.5f, Mathf.NextPowerOfTwo(0.3f));
            Assert.AreEqual(1f, Mathf.NextPowerOfTwo(1f));
            Assert.AreEqual(2f, Mathf.NextPowerOfTwo(1.5f));
            Assert.AreEqual(2f, Mathf.NextPowerOfTwo(2f));
            Assert.AreEqual(64f, Mathf.NextPowerOfTwo(52f));
            Assert.AreEqual(4096f, Mathf.NextPowerOfTwo(4096f));
            Assert.AreEqual(8192f, Mathf.NextPowerOfTwo(4097f));
        }

        [TestMethod]
        public void TestNextPowerOfTwoDouble()
        {
            Assert.AreEqual(0.0, Mathf.NextPowerOfTwo(0.0));
            Assert.AreEqual(0.5, Mathf.NextPowerOfTwo(0.3));
            Assert.AreEqual(1.0, Mathf.NextPowerOfTwo(1.0));
            Assert.AreEqual(2.0, Mathf.NextPowerOfTwo(1.5));
            Assert.AreEqual(2.0, Mathf.NextPowerOfTwo(2.0));
            Assert.AreEqual(64.0, Mathf.NextPowerOfTwo(52.0));
            Assert.AreEqual(4096.0, Mathf.NextPowerOfTwo(4096.0));
            Assert.AreEqual(8192.0, Mathf.NextPowerOfTwo(4097.0));
        }

        [TestMethod]
        public void TestIsPowerOfTwoInt()
        {
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(0));
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(1));
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(2));
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(64));
            Assert.AreEqual(false, Mathf.IsPowerOfTwo(-2));
            Assert.AreEqual(false, Mathf.IsPowerOfTwo(57));
            Assert.AreEqual(false, Mathf.IsPowerOfTwo(4097));
        }

        [TestMethod]
        public void TestIsPowerOfTwoLong()
        {
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(0L));
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(1L));
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(2L));
            Assert.AreEqual(true, Mathf.IsPowerOfTwo(64L));
            Assert.AreEqual(false, Mathf.IsPowerOfTwo(-2L));
            Assert.AreEqual(false, Mathf.IsPowerOfTwo(57L));
            Assert.AreEqual(false, Mathf.IsPowerOfTwo(4097L));
        }

        [TestMethod]
        public void TestFactorial()
        {
            Assert.AreEqual(120L, Mathf.Factorial(5));
            Assert.AreEqual(40320L, Mathf.Factorial(8));
        }

        [TestMethod]
        public void TestBinomial()
        {
            Assert.AreEqual(4923689695575L, Mathf.BinomialCoefficient(50, 34));
        }

        [TestMethod]
        public void TestLerp()
        {
            const float a = 5f;
            const float b = -11f;

            Assert.AreEqual(a, Mathf.Lerp(a, b, 0f));
            Assert.AreEqual(b, Mathf.Lerp(a, b, 1f));
            Assert.AreEqual(a * 0.50f + b * 0.50f, Mathf.Lerp(a, b, 0.5f));
            Assert.AreEqual(a * 0.75f + b * 0.25f, Mathf.Lerp(a, b, 0.25f));
        }

        [TestMethod]
        public void TestInverseLerp()
        {
            const float a = 5f;
            const float b = -11f;

            Assert.AreEqual(0f, Mathf.InverseLerp(a, b, a));
            Assert.AreEqual(1f, Mathf.InverseLerp(a, b, b));
            Assert.AreEqual(0.50f, Mathf.InverseLerp(a, b, a * 0.50f + b * 0.50f));
            Assert.AreEqual(0.25f, Mathf.InverseLerp(a, b, a * 0.75f + b * 0.25f));
        }
    }
}
