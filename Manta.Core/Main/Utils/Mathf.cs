/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Manta
{
    /// <summary>
    /// Contains math constants and functions. Wraps much of the <see cref="Math"/> library, but adds additional optimizations.
    /// </summary>
    public static partial class Mathf
    {
        private const MethodImplOptions METHOD_OPTIONS = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        /// <summary>
        /// Defines the value of π as a <see cref="float"/>.
        /// </summary>
        public const float Pi = (float)Math.PI;

        /// <summary>
        /// Defines the value of π/2 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver2 = Pi / 2f;

        /// <summary>
        /// Defines the value of π/3 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver3 = Pi / 3f;

        /// <summary>
        /// Defines the value of π/4 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver4 = Pi / 4f;

        /// <summary>
        /// Defines the value of π/6 as a <see cref="float"/>.
        /// </summary>
        public const float PiOver6 = Pi / 6f;

        /// <summary>
        /// Defines the value of 2π as a <see cref="float"/>.
        /// </summary>
        public const float TwoPi = 2f * Pi;

        /// <summary>
        /// Defines the value of 2π as a <see cref="float"/>.
        /// </summary>
        public const float Tau = TwoPi;

        /// <summary>
        /// Defines the ratio of degrees per radian.
        /// </summary>
        public const float DegToRad = Pi / 180f;

        /// <summary>
        /// Defines the ratio of radians per degree.
        /// </summary>
        public const float RadToDeg = 180f / Pi;

        /// <summary>
        /// Defines the value of E as a <see cref="float"/>.
        /// </summary>
        public const float E = (float)Math.E;

        /// <summary>
        /// Defines the base-10 logarithm of E as a <see cref="float"/>.
        /// </summary>
        public const float Log10E = 0.434294482f;

        /// <summary>
        /// Defines the base-2 logarithm of E as a <see cref="float"/>.
        /// </summary>
        public const float Log2E = 1.442695041f;


        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int Min(int x, int y) => Math.Min(x, y);

        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static long Min(long x, long y) => Math.Min(x, y);

        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Min(float x, float y)
        {
            Vector128<float> x0 = Sse.LoadScalarVector128(&x);
            Vector128<float> y0 = Sse.LoadScalarVector128(&y);
            return Sse.MinScalar(x0, y0).ToScalar();
        }

        /// <summary>
        /// Returns the least of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe double Min(double x, double y)
        {
            Vector128<double> x0 = Sse2.LoadScalarVector128(&x);
            Vector128<double> y0 = Sse2.LoadScalarVector128(&y);
            return Sse2.MinScalar(x0, y0).ToScalar();
        }


        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int Max(int x, int y) => Math.Max(x, y);

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static long Max(long x, long y) => Math.Max(x, y);

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Max(float x, float y)
        {
            Vector128<float> x0 = Sse.LoadScalarVector128(&x);
            Vector128<float> y0 = Sse.LoadScalarVector128(&y);
            return Sse.MaxScalar(x0, y0).ToScalar();
        }

        /// <summary>
        /// Returns the greater of two numbers.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe double Max(double x, double y)
        {
            Vector128<double> x0 = Sse2.LoadScalarVector128(&x);
            Vector128<double> y0 = Sse2.LoadScalarVector128(&y);
            return Sse2.MaxScalar(x0, y0).ToScalar();
        }


        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int Clamp(int x, int min, int max) => Math.Clamp(x, min, max);

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static long Clamp(long x, long min, long max) => Math.Clamp(x, min, max);

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Clamp(float x, float min, float max)
        {
            Vector128<float> x0 = Sse.LoadScalarVector128(&x);
            Vector128<float> min0 = Sse.LoadScalarVector128(&min);
            Vector128<float> max0 = Sse.LoadScalarVector128(&max);

            x0 = Sse.MinScalar(x0, max0);
            x0 = Sse.MaxScalar(x0, min0);
            return x0.ToScalar();
        }

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe double Clamp(double x, double min, double max)
        {
            Vector128<double> x0 = Sse2.LoadScalarVector128(&x);
            Vector128<double> min0 = Sse2.LoadScalarVector128(&min);
            Vector128<double> max0 = Sse2.LoadScalarVector128(&max);

            x0 = Sse2.MinScalar(x0, max0);
            x0 = Sse2.MaxScalar(x0, min0);
            return x0.ToScalar();
        }


        /// <summary>
        /// Clamps a number between 0 and 1.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Clamp01(float x) => Clamp(x, 0f, 1f);

        /// <summary>
        /// Clamps a number between 0 and 1.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Clamp01(double x) => Clamp(x, 0.0, 1.0);


        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int Abs(int x) => Math.Abs(x);

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static long Abs(long x) => Math.Abs(x);

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Abs(float x) => MathF.Abs(x);

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Abs(double x) => Math.Abs(x);


        /// <summary>
        /// Gets the sign of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Sign(float x) => MathF.Sign(x);

        /// <summary>
        /// Gets the sign of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Sign(double x) => Math.Sign(x);


        /// <summary>
        /// Copies the sign of one number to another.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float CopySign(float to, float from) => MathF.CopySign(to, from);

        /// <summary>
        /// Copies the sign of one number to another.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double CopySign(double to, double from) => Math.CopySign(to, from);


        /// <summary>
        /// Returns the smallest integer greater than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int CeilToInt(float x) => (int)MathF.Ceiling(x);

        /// <summary>
        /// Returns the smallest integer greater than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int CeilToInt(double x) => (int)Math.Ceiling(x);

        /// <summary>
        /// Returns the largest integer less than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int FloorToInt(float n) => (int)MathF.Floor(n);

        /// <summary>
        /// Returns the largest integer less than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int FloorToInt(double n) => (int)Math.Floor(n);

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int RoundToInt(float n) => (int)MathF.Round(n);

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static int RoundToInt(double n) => (int)Math.Round(n);


        /// <summary>
        /// Returns the smallest integer greater than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Ceil(float n) => MathF.Ceiling(n);

        /// <summary>
        /// Returns the smallest integer greater than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Ceil(double n) => Math.Ceiling(n);

        /// <summary>
        /// Returns the largest integer less than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Floor(float n) => MathF.Floor(n);

        /// <summary>
        /// Returns the largest integer less than or equal to a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Floor(double n) => Math.Floor(n);

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Round(float n) => MathF.Round(n);

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Round(double n) => Math.Round(n);


        /// <summary>
        /// Returns the square root of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Sqrt(float x) => MathF.Sqrt(x);

        /// <summary>
        /// Returns the square root of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Sqrt(double x) => Math.Sqrt(x);
        
        /// <summary>
        /// Returns an approximation of the reciprocal square root of a number, with 1.5*2^-12 relative error.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float InvSqrtFast(float x)
        {
            Vector128<float> x0 = Sse.LoadScalarVector128(&x);
            return Sse.ReciprocalSqrtScalar(x0).ToScalar();
        }


        /// <summary>
        /// Returns the sine of an angle given in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Sin(float x) => MathF.Sin(x);

        /// <summary>
        /// Returns the sine of an angle given in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Sin(double x) => Math.Sin(x);

        /// <summary>
        /// Returns the cosine of an angle given in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Cos(float x) => MathF.Cos(x);

        /// <summary>
        /// Returns the cosine of an angle given in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Cos(double x) => Math.Cos(x);

        /// <summary>
        /// Returns the tangent of an angle given in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Tan(float x) => MathF.Tan(x);

        /// <summary>
        /// Returns the tangent of an angle given in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Tan(double x) => Math.Tan(x);

        /// <summary>
        /// Returns the arcsine of a number in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Asin(float x) => MathF.Asin(x);

        /// <summary>
        /// Returns the arcsine of a number in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Asin(double x) => Math.Asin(x);

        /// <summary>
        /// Returns the arccosine of a number in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Acos(float x) => MathF.Acos(x);

        /// <summary>
        /// Returns the arccosine of a number in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Acos(double x) => Math.Acos(x);

        /// <summary>
        /// Returns the arctangent of a number in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Atan(float x) => MathF.Atan(x);

        /// <summary>
        /// Returns the arctangent of a number in radians.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Atan(double x) => Math.Atan(x);

        /// <summary>
        /// Returns the angle in radians with a tan of y/x.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Atan2(float y, float x) => MathF.Atan2(y, x);

        /// <summary>
        /// Returns the angle in radians with a tan of y/x.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Atan2(double y, double x) => Math.Atan2(y, x);


        /// <summary>
        /// Returns the number x raised to the power y.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Pow(float x, float y) => MathF.Pow(x, y);

        /// <summary>
        /// Returns the number x raised to the power y.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Pow(double x, double y) => Math.Pow(x, y);

        /// <summary>
        /// Returns e raised to the power x.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Exp(float x) => MathF.Exp(x);

        /// <summary>
        /// Returns e raised to the power x.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Exp(double x) => Math.Exp(x);

        /// <summary>
        /// Returns logarithm base e of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Log(float x) => MathF.Log(x);

        /// <summary>
        /// Returns logarithm base e of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Log(double x) => Math.Log(x);

        /// <summary>
        /// Returns logarithm base y of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Log(float x, float y) => MathF.Log(x, y);

        /// <summary>
        /// Returns logarithm base y of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Log(double x, double y) => Math.Log(x, y);

        /// <summary>
        /// Returns logarithm base 10 of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Log10(float x) => MathF.Log10(x);

        /// <summary>
        /// Returns logarithm base 10 of a number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double Log10(double x) => Math.Log10(x);


        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// The input is only defined for positive numbers greater than 0.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe int NextPowerOfTwo(int x)
        {
            // this makes sure that powers of two result in themselves
            x = (x << 1) - 1;
            uint lzc = Lzcnt.LeadingZeroCount(*(uint*)&x);
            int shift = 31 - *(int*)&lzc;
            return 1 << shift;
        }

        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// The input is only defined for positive numbers greater than 0.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe long NextPowerOfTwo(long x)
        {
            x = (x << 1) - 1;
            ulong lzc = Lzcnt.X64.LeadingZeroCount(*(ulong*)&x);
            int shift = 63 - (int)lzc;
            return 1L << shift;
        }

        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static float NextPowerOfTwo(float x)
        {
            return Pow(2f, Ceil(Log(x, 2f)));
        }

        /// <summary>
        /// Returns the next power of two that is greater than or equal to the specified number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static double NextPowerOfTwo(double x)
        {
            return Pow(2.0, Ceil(Log(x, 2.0)));
        }


        /// <summary>
        /// Checks if a number is a power of two.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe bool IsPowerOfTwo(int x)
        {
            return Bmi1.ResetLowestSetBit(*(uint*)&x) == 0;
        }

        /// <summary>
        /// Checks if a number is a power of two.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe bool IsPowerOfTwo(long x)
        {
            return Bmi1.X64.ResetLowestSetBit(*(ulong*)&x) == 0;
        }


        /// <summary>
        /// Calculates the factorial of a given natural number.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public static long Factorial(int n)
        {
            long result = 1L;

            while (n > 1L)
            {
                result *= n--;
            }

            return result;
        }

        /// <summary>
        /// Calculates the binomial coefficient <paramref name="n"/> above <paramref name="k"/>.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="k">The k.</param>
        /// <returns>n! / (k! * (n - k)!).</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static long BinomialCoefficient(int n, int k)
        {
            long result = 1L;

            k = Min(k, n - k);
            
            for (int i = 0; i < k;)
            {
                result *= (n - i);
                result /= ++i;
            }

            return result;
        }


        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="t">The weight of b.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Lerp(float a, float b, float t)
        {
            // theoretically faster and more accurate, in practice the compiler doesn't to a great job with this
            //Vector128<float> a0 = Sse.LoadScalarVector128(&a);
            //Vector128<float> t0 = Sse.LoadScalarVector128(&t);
            //return Fma.MultiplySubtractScalar(t0, Sse.LoadScalarVector128(&b), Fma.MultiplySubtractScalar(t0, a0, a0)).ToScalar();
            return a + ((b - a) * t);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="t">The weight of b.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float LerpClamped(float a, float b, float t) => Lerp(a, b, Clamp01(t));

        /// <summary>
        /// Calculates the interpolation factor between a and b that results in a specific value.
        /// </summary>
        /// <param name="a">From value.</param>
        /// <param name="b">To value.</param>
        /// <param name="value">The value interpolated to.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
            {
                return (value - a) / (b - a);
            }
            return 0f;
        }

        /// <summary>
        /// Linearly interpolates from angle a to b by factor t.
        /// </summary>
        /// <param name="a">The angle to move from in radians.</param>
        /// <param name="b">The angle to move towards in radians.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float LerpAngle(float a, float b, float t)
        {
            float delta = Cycle(b - a, TwoPi);
            if (delta > Pi)
            {
                delta -= TwoPi;
            }
            return a + delta * t;
        }

        /// <summary>
        /// Linearly interpolates from angle a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The angle to move from in radians.</param>
        /// <param name="b">The angle to move towards in radians.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float LerpAngleClamped(float a, float b, float t) => LerpAngle(a, b, Clamp01(t));


        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The value to move from.</param>
        /// <param name="b">The value to move towards.</param>
        /// <param name="maxDelta">The maximum amount from can move.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float MoveTowards(float a, float b, float maxDelta)
        {
            float delta = b - a;
            if (Abs(delta) <= maxDelta)
            {
                return b;
            }
            return a + CopySign(maxDelta, delta);
        }

        /// <summary>
        /// Move from one angle to another up to some amount.
        /// </summary>
        /// <param name="from">The angle to move from in radians.</param>
        /// <param name="to">The angle to move to in radians.</param>
        /// <param name="maxDelta">The maximum amount from can move.</param>
        [MethodImpl(METHOD_OPTIONS)]
        static public float MoveTowardsAngle(float from, float to, float maxDelta)
        {
            float delta = DeltaAngle(from, to);
            if (-maxDelta < delta && delta < maxDelta)
            {
                return to;
            }
            return MoveTowards(from, from + delta, maxDelta);
        }


        /// <summary>
        /// Loop a number so that it is wrapped zero and size.
        /// </summary>
        /// <param name="n">The number to wrap.</param>
        /// <param name="size">The period of the wrap function.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Cycle(float n, float size)
        {
            return n - Floor(n / size) * size;
        }

        /// <summary>
        /// PingPongs a number so that it is wrapped between zero and size.
        /// </summary>
        /// <param name="n">The number to wrap.</param>
        /// <param name="size">The value to wrap around.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float PingPong(float n, float size)
        {
            return size - Abs(Cycle(n, size * 2f) - size);
        }

        /// <summary>
        /// Reduces a given angle to a value between -π and π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static float WrapAngle(float angle)
        {
            if ((angle <= -Pi) || (Pi < angle))
            {
                angle %= TwoPi;

                if (angle <= -Pi)
                {
                    return angle + TwoPi;
                }
                if (angle > Pi)
                {
                    return angle - TwoPi;
                }
            }
            return angle;
        }


        /// <summary>
        /// Returns the signed difference between two angles.
        /// </summary>
        /// <param name="from">The angle to move from in radians.</param>
        /// <param name="to">The angle to move to in radians.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float DeltaAngle(float from, float to)
        {
            float delta = Cycle(to - from, TwoPi);
            if (delta > Pi)
            {
                delta -= TwoPi;
            }
            return delta;
        }

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="a">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="b">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="c">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="u">The normalized barycentric (areal) coordinate equal to the weighting factor for b.</param>
        /// <param name="v">The normalized barycentric (areal) coordinate equal to the weighting factor for c.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Barycentric(float a, float b, float c, float u, float v)
        {
            return a + (b - a) * u + (c - a) * v;
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="p0">The first position in the interpolation.</param>
        /// <param name="p1">The second position in the interpolation.</param>
        /// <param name="p2">The third position in the interpolation.</param>
        /// <param name="p3">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static float CatmullRom(float p0, float p1, float p2, float p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            return 0.5f * (
                (3f * p1 - p0 - 3f * p2 + p3) * t3 +
                (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                (p2 - p0) * t +
                2f * p1
            );
        }

        /// <summary>
        /// Performs Hermite spline interpolation.
        /// </summary>
        /// <param name="p0">Source position.</param>
        /// <param name="m0">Source tangent.</param>
        /// <param name="p1">Source position.</param>
        /// <param name="m1">Source tangent.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Hermite(float p0, float m0, float p1, float m1, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;
            return
                (2f * p0 - 2f * p1 + m1 + m0) * t3 +
                (3f * p1 - 3f * p0 - 2f * m0 - m1) * t2 +
                m0 * t +
                p0;
        }

        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="a">From value. Must be less than b.</param>
        /// <param name="b">To value. Must be greater than a.</param>
        /// <param name="t">Value between 0 and 1 indicating the weight of b.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float SmoothStep(float a, float b, float t)
        {
            t = Clamp01((t - a) / (b - a));
            return t * t * (3f - 2f * t);
        }


        /// <summary>
        /// Transforms a value from linear space to gamma space.
        /// </summary>
        /// <param name="value">The linear value to transform.</param>
        /// <returns>An sRGB value.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static float LinearToSrgb(float value)
        {
            if (value <= 0.0031308)
            {
                return 12.92f * value;
            }
            else
            {
                return (1.055f * Pow(value, 1f / 2.4f)) - 0.055f;
            }
        }

        /// <summary>
        /// Transforms a value from gamma space to linear space.
        /// </summary>
        /// <param name="value">The sRGB value to transform.</param>
        /// <returns>A linear value.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static float SrgbToLinear(float value)
        {
            if (value <= 0.04045f)
            {
                return value / 12.92f;
            }
            else
            {
                return Pow((value + 0.055f) / 1.055f, 2.4f);
            }
        }


        /// <summary>
        /// Approximates floating point equality with a maximum number of different bits.
        /// This is typically used in place of an epsilon comparison.
        /// see: https://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/
        /// see: https://stackoverflow.com/questions/3874627/floating-point-comparison-functions-for-c-sharp.
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">>The second value to compare.</param>
        /// <param name="maxDeltaBits">The number of floating point bits to check.</param>
        /// <returns>True if the values are approximately equal, otherwise false.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe bool ApproximatelyEqual(float a, float b, int maxDeltaBits = 1)
        {
            // we use longs here, otherwise we run into a two's complement problem, causing this to fail with -2 and 2.0
            long k = *(int*)&a;
            if (k < 0)
            {
                k = int.MinValue - k;
            }

            long l = *(int*)&b;
            if (l < 0)
            {
                l = int.MinValue - l;
            }

            return Abs(k - l) <= (1 << maxDeltaBits);
        }

        /// <summary>
        /// Approximates equivalence between two single-precision floating-point numbers on a similar scale.
        /// This is fast, but for more accurate comparisons prefer <see cref="ApproximatelyEqualRelative(float, float, float)">
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <param name="tolerance">The tolerance within which the two values would be considered equivalent.</param>
        /// <returns>True if the values are approximately equal, otherwise false.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool ApproximatelyEqual(float a, float b, float tolerance)
        {
            if (a == b)
            {
                // handles infinities
                return true;
            }

            var diff = Abs(a - b);
            return diff <= tolerance;
        }

        /// <summary>
        /// Approximates equivalence between two double-precision floating-point numbers on a similar scale.
        /// This is fast, but for more accurate comparisons prefer <see cref="ApproximatelyEqualEpsilon(double, double, double)">
        /// </summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <param name="tolerance">The tolerance within which the two values would be considered equivalent.</param>
        /// <returns>True if the values are approximately equal, otherwise false.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool ApproximatelyEquivalent(double a, double b, double tolerance)
        {
            if (a == b)
            {
                // handles infinities
                return true;
            }

            var diff = Abs(a - b);
            return diff <= tolerance;
        }

        /// <summary>
        /// Approximates single-precision floating point equality by an epsilon (maximum error) value.
        /// This method is designed as a "fits-all" solution and attempts to handle as many cases as possible.
        /// </summary>
        /// <param name="a">The first float.</param>
        /// <param name="b">The second float.</param>
        /// <param name="epsilon">The maximum error between the two.</param>
        /// <returns>True if the values are approximately equal, otherwise false.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool ApproximatelyEqualRelative(float a, float b, float epsilon = 0.001f)
        {
            const float floatNormal = (1 << 23) * float.Epsilon;

            if (a == b)
            {
                // handles infinities
                return true;
            }

            var diff = Abs(a - b);

            if (a == 0f || b == 0f || diff < floatNormal)
            {
                // a or b is zero, or both are extremely close to it.
                // relative error is less meaningful here
                return diff < epsilon * floatNormal;
            }

            // use relative error
            var absA = Abs(a);
            var absB = Abs(b);
            return diff / Min(absA + absB, float.MaxValue) < epsilon;
        }

        /// <summary>
        /// Approximates double-precision floating point equality by an epsilon (maximum error) value.
        /// This method is designed as a "fits-all" solution and attempts to handle as many cases as possible.
        /// </summary>
        /// <param name="a">The first double.</param>
        /// <param name="b">The second double.</param>
        /// <param name="epsilon">The maximum error between the two.</param>
        /// <returns>True if the values are approximately equal, otherwise false.</returns>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool ApproximatelyEqualEpsilon(double a, double b, double epsilon = 0.001)
        {
            const double doubleNormal = (1L << 52) * double.Epsilon;

            if (a == b)
            {
                // handles infinities
                return true;
            }

            var diff = Abs(a - b);

            if (a == 0.0 || b == 0.0 || diff < doubleNormal)
            {
                // a or b is zero, or both are extremely close to it.
                // relative error is less meaningful here
                return diff < epsilon * doubleNormal;
            }

            // use relative error
            var absA = Abs(a);
            var absB = Abs(b);
            return diff / Min(absA + absB, double.MaxValue) < epsilon;
        }
    }
}
