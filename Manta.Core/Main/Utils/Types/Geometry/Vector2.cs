/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Diagnostics;

namespace Manta
{
    /// <summary>
    /// Describes a 2d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector2 : IEquatable<Vector2>
    {
        private const MethodImplOptions METHOD_OPTIONS = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        /// <summary>
        /// Returns a vector with components (0, 0).
        /// </summary>
        public static readonly Vector2 Zero = new Vector2(0f, 0f);

        /// <summary>
        /// Returns a vector with components (1, 1).
        /// </summary>
        public static readonly Vector2 One = new Vector2(1f, 1f);

        /// <summary>
        /// Returns a vector with components (1, 0).
        /// </summary>
        public static readonly Vector2 UnitX = new Vector2(1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 1).
        /// </summary>
        public static readonly Vector2 UnitY = new Vector2(0f, 1f);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// Constructs a 2d vector from two values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        public Vector2(float value)
        {
            x = value;
            y = value;
        }

        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        public unsafe float this[int index]
        {
            get
            {
                Debug.Assert(0 <= index && index <= 1, $"Index out of range: {index}");
                return *((float*)Unsafe.AsPointer(ref this) + index);
            }
            set
            {
                Debug.Assert(0 <= index && index <= 1, $"Index out of range: {index}");
                *((float*)Unsafe.AsPointer(ref this) + index) = value;
            }
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        public float Length => Magnitude(ref this);

        /// <summary>
        /// Returns an approximation for the length of this vector, with 1.5*2^-12 relative error.
        /// </summary>
        public float LengthFast => MagnitudeFast(ref this);

        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => MagnitudeSquared(ref this);

        /// <summary>
        /// Returns a copy of the vector scaled to unit length.
        /// </summary>
        public Vector2 Normalized
        {
            get
            {
                Normalize(ref this, out Vector2 result);
                return result;
            }
        }

        /// <summary>
        /// Returns a copy of the vector scaled to approximately unit length, with 1.5*2^-12 relative error..
        /// </summary>
        public Vector2 NormalizedFast
        {
            get
            {
                NormalizeFast(ref this, out Vector2 result);
                return result;
            }
        }

        /// <summary>
        /// Gets the perpendicular vector on the right side of this vector.
        /// </summary>
        public Vector2 PerpendicularRight => new Vector2(y, -x);

        /// <summary>
        /// Gets the perpendicular vector on the left side of this vector.
        /// </summary>
        public Vector2 PerpendicularLeft => new Vector2(-y, x);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 ComponentMin(Vector2 a, Vector2 b)
        {
            ComponentMin(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void ComponentMin(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v0 = Sse.Min(a0, b0);

            fixed (Vector2* pr = &result)
            {
                Sse.StoreLow((float*)pr, v0);
            }
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 ComponentMax(Vector2 a, Vector2 b)
        {
            ComponentMax(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void ComponentMax(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v0 = Sse.Max(a0, b0);

            fixed (Vector2* pr = &result)
            {
                Sse.StoreLow((float*)pr, v0);
            }
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min values.</param>
        /// <param name="max">The max values.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector2 ComponentClamp(Vector2 vector, Vector2 min, Vector2 max)
        {
            ComponentClamp(ref vector, ref min, ref max, out vector);
            return vector;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min values.</param>
        /// <param name="max">The max values.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void ComponentClamp(ref Vector2 vector, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();
            Vector128<float> min0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref min)).AsSingle();
            Vector128<float> max0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref max)).AsSingle();

            Vector128<float> v1 = Sse.Max(Sse.Min(v0, max0), min0);

            fixed (Vector2* pr = &result)
            {
                Sse.StoreLow((float*)pr, v1);
            }
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Min(Vector2 left, Vector2 right)
        {
            Min(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Min(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = MagnitudeSquared(ref left) < MagnitudeSquared(ref right) ? left : right;
        }

        /// <summary>
        /// Returns the vector with the greater magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Max(Vector2 left, Vector2 right)
        {
            Max(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Returns the vector with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Max(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = MagnitudeSquared(ref left) > MagnitudeSquared(ref right) ? left : right;
        }

        /// <summary>
        /// Returns the vector with a magnitude less than or equal to the specified length.
        /// </summary>
        /// <param name="vector">The vector to clamp.</param>
        /// <param name="maxLength">The maxiumum maginude of the returned vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 MagnitudeClamp(Vector2 vector, float maxLength)
        {
            if (vector.LengthSquared > maxLength * maxLength)
            {
                return vector.Normalized * maxLength;
            }
            return vector;
        }

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Magnitude(Vector2 vector)
        {
            return Magnitude(ref vector);
        }

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Magnitude(ref Vector2 vector)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();

            // get squared length
            Vector128<float> v1 = Sse.Multiply(v0, v0);
            Vector128<float> v2 = Sse.Shuffle(v1, v1, 0x0001);
            Vector128<float> v3 = Sse.Add(v1, v2);
            // do square root
            Vector128<float> v4 = Sse.SqrtScalar(v3);

            return v4.ToScalar();
        }

        /// <summary>
        /// Returns the magnitude of a vector, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float MagnitudeFast(Vector2 vector)
        {
            return MagnitudeFast(ref vector);
        }

        /// <summary>
        /// Returns the magnitude of a vector, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float MagnitudeFast(ref Vector2 vector)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();

            // get squared length
            Vector128<float> v1 = Sse.Multiply(v0, v0);
            Vector128<float> v2 = Sse.Shuffle(v1, v1, 0x0001);
            Vector128<float> v3 = Sse.Add(v1, v2);
            // do fast square root
            Vector128<float> v4 = Sse.MultiplyScalar(v3, Sse.ReciprocalSqrtScalar(v3));

            return v4.ToScalar();
        }

        /// <summary>
        /// Returns the squared magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSquared(Vector2 vector)
        {
            return MagnitudeSquared(ref vector);
        }

        /// <summary>
        /// Returns the squared magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float MagnitudeSquared(ref Vector2 vector)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();

            // get squared length
            Vector128<float> v1 = Sse.Multiply(v0, v0);
            Vector128<float> v2 = Sse.Shuffle(v1, v1, 0x0001);
            Vector128<float> v3 = Sse.Add(v1, v2);

            return v3.ToScalar();
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Normalize(Vector2 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Normalize(ref Vector2 vector, out Vector2 result)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();

            // get squared length
            Vector128<float> v1 = Sse.Multiply(v0, v0);
            Vector128<float> v2 = Sse.Shuffle(v1, v1, 0x0001);
            Vector128<float> v3 = Sse.Add(v1, v2);
            // get squared length
            Vector128<float> v4 = Sse.Sqrt(v3);
            // divide vector by length
            Vector128<float> v5 = Sse.Divide(v0, v4);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v5.AsDouble());
            }
        }

        /// <summary>
        /// Scale a vector to approximately unit length, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 NormalizeFast(Vector2 vector)
        {
            NormalizeFast(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to approximately unit length, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void NormalizeFast(ref Vector2 vector, out Vector2 result)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();

            // get squared length
            Vector128<float> v1 = Sse.Multiply(v0, v0);
            Vector128<float> v2 = Sse.Shuffle(v1, v1, 0x0001);
            Vector128<float> v3 = Sse.Add(v1, v2);
            // apply inverse of length squared
            Vector128<float> v4 = Sse.Multiply(v0, Sse.ReciprocalSqrt(v3));

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v4.AsDouble());
            }
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            Add(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Add(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v0 = Sse.Add(a0, b0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v0.AsDouble());
            }
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            Subtract(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Subtract(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v0 = Sse.Subtract(a0, b0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v0.AsDouble());
            }
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Negate(Vector2 vector)
        {
            Negate(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Negate(ref Vector2 vector, out Vector2 result)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();
            Vector128<float> n0 = Vector128.Create(-0f);

            Vector128<float> v1 = Sse.Xor(v0, n0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v1.AsDouble());
            }
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Multiply(Vector2 vector, float scale)
        {
            Multiply(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Multiply(ref Vector2 vector, float scale, out Vector2 result)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();
            Vector128<float> s0 = Vector128.Create(scale);

            Vector128<float> v1 = Sse.Multiply(v0, s0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v1.AsDouble());
            }
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Multiply(Vector2 a, Vector2 b)
        {
            Multiply(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Multiply(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v0 = Sse.Multiply(a0, b0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v0.AsDouble());
            }
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Divide(Vector2 vector, float scale)
        {
            Divide(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divisor">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Divide(ref Vector2 vector, float divisor, out Vector2 result)
        {
            Vector128<float> v0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();
            Vector128<float> s0 = Vector128.Create(divisor);

            Vector128<float> v1 = Sse.Divide(v0, s0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v1.AsDouble());
            }
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Divide(Vector2 vector, Vector2 scale)
        {
            Divide(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divide a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Divide(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v0 = Sse.Divide(a0, b0);

            fixed (Vector2* pr = &result)
            {
                Sse2.StoreScalar((double*)pr, v0.AsDouble());
            }
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Dot(Vector2 a, Vector2 b)
        {
            return Dot(ref a, ref b);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Dot(ref Vector2 a, ref Vector2 b)
        {
            Vector128<float> a0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref a)).AsSingle();
            Vector128<float> b0 = Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref b)).AsSingle();

            Vector128<float> v1 = Sse.Multiply(a0, b0);
            Vector128<float> v2 = Sse.Shuffle(v1, v1, 0x0001);
            Vector128<float> v3 = Sse.Add(v1, v2);

            return v3.ToScalar();
        }

        /// <summary>
        /// Reflects a vector about a normal.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="normal">The reflection normal.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Reflect(ref vector, ref normal, out vector);
            return vector;
        }

        /// <summary>
        /// Reflects a vector about a normal.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="normal">The reflection normal.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            result = vector - (2f * Dot(vector, normal) * normal);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Distance(Vector2 a, Vector2 b)
        {
            Distance(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Distance(ref Vector2 a, ref Vector2 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            result = (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            DistanceSquared(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void DistanceSquared(ref Vector2 a, ref Vector2 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            result = (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Returns the absolute value of the angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Angle(Vector2 from, Vector2 to)
        {
            Angle(ref from, ref to, out float result);
            return result;
        }

        /// <summary>
        /// Returns the absolute value of the angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Angle(ref Vector2 from, ref Vector2 to, out float result)
        {
            float denominator = (float)Math.Sqrt(from.LengthSquared * to.LengthSquared);
            if (denominator < 1e-15f)
            {
                result = 0f;
            }
            else
            {
                result = (float)Math.Acos(Math.Min(Math.Max(Dot(from, to) / denominator, -1f), 1f));
            }
        }

        /// <summary>
        /// Returns the signed angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            SignedAngle(ref from, ref to, out float result);
            return result;
        }

        /// <summary>
        /// Returns the signed angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        public static void SignedAngle(ref Vector2 from, ref Vector2 to, out float result)
        {
            result = Mathf.Sign((from.x * to.y) - (from.x * to.y)) * Angle(from, to);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            Lerp(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void Lerp(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector2 LerpClamped(Vector2 a, Vector2 b, float t)
        {
            LerpClamped(ref a, ref b, t, out a);
            return a;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void LerpClamped(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            t = Math.Min(Math.Max(t, 0f), 1f);
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        public static Vector2 MoveTowards(Vector2 a, Vector2 b, float maxDelta)
        {
            MoveTowards(ref a, ref b, maxDelta, out a);
            return a;
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        public static void MoveTowards(ref Vector2 a, ref Vector2 b, float maxDelta, out Vector2 result)
        {
            Vector2 delta = b - a;
            if (delta.LengthSquared <= maxDelta * maxDelta)
            {
                result = b;
            }
            else
            {
                result = a + delta.Normalized * maxDelta;
            }
        }

        /*
        /// <summary>
        /// Creates a vector that contains the cartesian coordinates of a vector specified in barycentric coordinates.
        /// </summary>
        /// <param name="a">The first vector of triangle.</param>
        /// <param name="b">The second vector of triangle.</param>
        /// <param name="c">The third vector of triangle.</param>
        /// <param name="u">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of triangle.</param>
        /// <param name="v">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector2 Barycentric(Vector2 a, Vector2 b, Vector2 c, float u, float v)
        {
            return a + (b - a) * u + (c - a) * v;
        }

        /// <summary>
        /// Creates a vector that contains the cartesian coordinates of a vector specified in barycentric coordinates.
        /// </summary>
        /// <param name="a">The first vector of triangle.</param>
        /// <param name="b">The second vector of triangle.</param>
        /// <param name="c">The third vector of triangle.</param>
        /// <param name="u">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of triangle.</param>
        /// <param name="v">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref Vector2 a, ref Vector2 b, ref Vector2 c, float u, float v, out Vector2 result)
        {
            result = a; // copy

            var temp = b; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, u, out temp);
            Add(ref result, ref temp, out result);

            temp = c; // copy
            Subtract(ref temp, ref a, out temp);
            Multiply(ref temp, v, out temp);
            Add(ref result, ref temp, out result);
        }

        /// <summary>
        /// Creates a vector that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="c">The third vector in interpolation.</param>
        /// <param name="d">The fourth vector in interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        public static Vector2 CatmullRom(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
        {
            return new Vector2(
                Mathf.CatmullRom(a.x, b.x, c.x, d.x, t),
                Mathf.CatmullRom(a.y, b.y, c.y, d.y, t));
        }

        /// <summary>
        /// Creates a vector that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="c">The third vector in interpolation.</param>
        /// <param name="d">The fourth vector in interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref Vector2 a, ref Vector2 b, ref Vector2 c, ref Vector2 d, float t, out Vector2 result)
        {
            result.x = Mathf.CatmullRom(a.x, b.x, c.x, d.x, t);
            result.y = Mathf.CatmullRom(a.y, b.y, c.y, d.y, t);
        }

        /// <summary>
        /// Creates a vector that contains hermite spline interpolation.
        /// </summary>
        /// <param name="v1">The first position vector.</param>
        /// <param name="t1">The first tangent vector.</param>
        /// <param name="v2">The second position vector.</param>
        /// <param name="t2">The second tangent vector.</param>
        /// <param name="t">Weighting factor.</param>
        public static Vector2 Hermite(Vector2 v1, Vector2 t1, Vector2 v2, Vector2 t2, float t)
        {
            return new Vector2(
                Mathf.Hermite(v1.x, t1.x, v2.x, t2.x, t),
                Mathf.Hermite(v1.y, t1.y, v2.y, t2.y, t));
        }

        /// <summary>
        /// Creates a vector that contains hermite spline interpolation.
        /// </summary>
        /// <param name="v1">The first position vector.</param>
        /// <param name="t1">The first tangent vector.</param>
        /// <param name="v2">The second position vector.</param>
        /// <param name="t2">The second tangent vector.</param>
        /// <param name="t">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref Vector2 v1, ref Vector2 t1, ref Vector2 v2, ref Vector2 t2, float t, out Vector2 result)
        {
            result.x = Mathf.Hermite(v1.x, t1.x, v2.x, t2.x, t);
            result.y = Mathf.Hermite(v1.y, t1.y, v2.y, t2.y, t);
        }

        /// <summary>
        /// Creates a vector that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector2 SmoothStep(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(
                Mathf.SmoothStep(a.x, b.x, t),
                Mathf.SmoothStep(a.y, b.y, t));
        }

        /// <summary>
        /// Creates a vector that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            result.x = Mathf.SmoothStep(a.x, b.x, t);
            result.y = Mathf.SmoothStep(a.y, b.y, t);
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        public static Vector2 Rotate(Vector2 vector, Quaternion rotation)
        {
            Rotate(ref vector, ref rotation, out Vector2 result);
            return result;
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void Rotate(ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            Vector3 r1 = new Vector3(rotation.x + rotation.x, rotation.y + rotation.y, rotation.z + rotation.z);
            Vector3 r2 = r1 * new Vector3(rotation.x, rotation.x, rotation.w);
            Vector3 r3 = r1 * new Vector3(1f, rotation.y, rotation.z);

            result.x = vector.x * (1f - r3.y - r3.z) + vector.y * (r2.y - r2.z);
            result.y = vector.x * (r2.y + r2.z) + vector.y * (1f - r2.x - r3.z);
        }

        /// <summary>
        /// Applies a rotation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void Rotate(Vector2[] srcArray, ref Quaternion rotation, Vector2[] destArray)
        {
            if (srcArray == null)
            {
                throw new ArgumentNullException("srcArray");
            }
            if (destArray == null)
            {
                throw new ArgumentNullException("destArray");
            }
            if (destArray.Length < srcArray.Length)
            {
                throw new ArgumentException("Destination array is smaller than source array.");
            }

            for (int i = 0; i < srcArray.Length; i++)
            {
                Rotate(ref srcArray[i], ref rotation, out destArray[i]);
            }
        }

        /// <summary>
        /// Applies a rotation to all vectors within array and places the resuls in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Rotate(Vector2[] srcArray, int srcIndex, ref Quaternion rotation, Vector2[] destArray, int destIndex, int length)
        {
            if (srcArray == null)
            {
                throw new ArgumentNullException("srcArray");
            }
            if (destArray == null)
            {
                throw new ArgumentNullException("destArray");
            }
            if (srcArray.Length < srcIndex + length)
            {
                throw new ArgumentException("Source array length is lesser than srcIndex + length");
            }
            if (destArray.Length < destIndex + length)
            {
                throw new ArgumentException("Destination array length is lesser than destIndex + length");
            }

            for (int i = 0; i < length; i++)
            {
                Rotate(ref srcArray[srcIndex + i], ref rotation, out destArray[destIndex + i]);
            }
        }
        */
        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector2 other)
        {
            return 
                x == other.x &&
                y == other.y;
        }

        /// <summary>
        /// Compares whether the current instance is equal to a specified instance.
        /// </summary>
        /// <param name="obj">The instance to compare.</param>
        /// <returns>True if the instances are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector2) && Equals((Vector2)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            const string format = "F2";
            return $"({x.ToString(format)}, {y.ToString(format)})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        /// <returns>Result of the negation.</returns>
        public static Vector2 operator -(Vector2 vector)
        {
            vector.x = -vector.x;
            vector.y = -vector.y;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector2 operator *(Vector2 vector, float scale)
        {
            vector.x *= scale;
            vector.y *= scale;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector2 operator *(float scale, Vector2 vector)
        {
            vector.x *= scale;
            vector.y *= scale;
            return vector;
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static Vector2 operator *(Vector2 vector, Vector2 scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            return vector;
        }

        /// <summary>
        /// Divides the components of a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divider">Right operand.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector2 operator /(Vector2 vector, float divider)
        {
            float factor = 1f / divider;
            vector.x *= factor;
            vector.y *= factor;
            return vector;
        }

        /// <summary>
        /// Component-wise division of one vector by another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector2 operator /(Vector2 vector, Vector2 scale)
        {
            vector.x /= scale.x;
            vector.y /= scale.y;
            return vector;
        }

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return 
                left.x == right.x &&
                left.y == right.y;
        }

        /// <summary>
        /// Compares whether two vectors are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return 
                left.x != right.x ||
                left.y != right.y;
        }
    }
}
