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
    /// Describes a 4d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector4 : IEquatable<Vector4>
    {
        private const MethodImplOptions METHOD_OPTIONS = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        /// <summary>
        /// Returns a vector with components (0, 0, 0, 0).
        /// </summary>
        public static readonly Vector4 Zero = new Vector4(0f, 0f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (1, 1, 1, 1).
        /// </summary>
        public static readonly Vector4 One = new Vector4(1f, 1f, 1f, 1f);

        /// <summary>
        /// Returns a vector with components (1, 0, 0, 0).
        /// </summary>
        public static readonly Vector4 UnitX = new Vector4(1f, 0f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 1, 0, 0).
        /// </summary>
        public static readonly Vector4 UnitY = new Vector4(0f, 1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 0, 1, 0).
        /// </summary>
        public static readonly Vector4 UnitZ = new Vector4(0f, 0f, 1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 0, 0, 1).
        /// </summary>
        public static readonly Vector4 UnitW = new Vector4(0f, 0f, 0f, 1f);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public float x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public float y;

        /// <summary>
        /// The z coordinate.
        /// </summary>
        public float z;

        /// <summary>
        /// The w coordinate.
        /// </summary>
        public float w;

        /// <summary>
        /// Constructs a 4d vector from four values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z and W set to the same value.
        /// </summary>
        /// <param name="value">The x, y, z and w coordinates.</param>
        public Vector4(float value)
        {
            x = value;
            y = value;
            z = value;
            w = value;
        }

        /// <summary>
        /// Constructs a 4d vector with X and Z from a <see cref="Vector2"/> and Z and W from scalars.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(Vector2 value, float z, float w)
        {
            x = value.x;
            y = value.y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a 4d vector from a pair of <see cref="Vector2"/>.
        /// </summary>
        /// <param name="xy">The x and y coordinates.</param>
        /// <param name="xy">The z and w coordinates.</param>
        public Vector4(Vector2 xy, Vector2 zw)
        {
            x = xy.x;
            y = xy.y;
            z = zw.x;
            w = zw.y;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z from <see cref="Vector3"/> and W from a scalar.
        /// </summary>
        /// <param name="value">The x, y and z coordinates.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4(Vector3 value, float w)
        {
            x = value.x;
            y = value.y;
            z = value.z;
            this.w = w;
        }
        
        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        public unsafe float this[int index]
        {
            get
            {
                Debug.Assert(0 <= index && index <= 3, $"Index out of range: {index}");
                return *((float*)Unsafe.AsPointer(ref this) + index);
            }
            set
            {
                Debug.Assert(0 <= index && index <= 3, $"Index out of range: {index}");
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
        public Vector4 Normalized
        {
            get
            {
                Normalize(ref this, out Vector4 result);
                return result;
            }
        }

        /// <summary>
        /// Returns a copy of the vector scaled to approximately unit length, with 1.5*2^-12 relative error..
        /// </summary>
        public Vector4 NormalizedFast
        {
            get
            {
                NormalizeFast(ref this, out Vector4 result);
                return result;
            }
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 ComponentMin(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return VectorIntrinsics.Store4(Sse.Min(a0, b0));
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMin(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse.Min(a0, b0));
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 ComponentMax(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return VectorIntrinsics.Store4(Sse.Max(a0, b0));
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMax(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse.Max(a0, b0));
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 ComponentClamp(Vector4 vector, Vector4 min, Vector4 max)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> min0 = VectorIntrinsics.Load(min);
            Vector128<float> max0 = VectorIntrinsics.Load(max);
            return VectorIntrinsics.Store4(Sse.Max(Sse.Min(v0, max0), min0));
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentClamp(ref Vector4 vector, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> min0 = VectorIntrinsics.Load(ref min);
            Vector128<float> max0 = VectorIntrinsics.Load(ref max);
            result = VectorIntrinsics.Store4(Sse.Max(Sse.Min(v0, max0), min0));
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Min(Vector4 left, Vector4 right)
        {
            return left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Min(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = MagnitudeSquared(ref left) < MagnitudeSquared(ref right) ? left : right;
        }

        /// <summary>
        /// Returns the vector with the greater magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Max(Vector4 left, Vector4 right)
        {
            return left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Max(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = MagnitudeSquared(ref left) >= MagnitudeSquared(ref right) ? left : right;
        }

        /// <summary>
        /// Returns the vector with a magnitude less than or equal to the specified length.
        /// </summary>
        /// <param name="vector">The vector to clamp.</param>
        /// <param name="maxLength">The maxiumum maginude of the returned vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 MagnitudeClamp(Vector4 vector, float maxLength)
        {
            if (vector.LengthSquared > maxLength * maxLength)
            {
                return Multiply(Normalize(vector), maxLength);
            }
            return vector;
        }

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Magnitude(Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return Sse.SqrtScalar(v1).ToScalar();
        }

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Magnitude(ref Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return Sse.SqrtScalar(v1).ToScalar();
        }

        /// <summary>
        /// Returns the magnitude of a vector, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeFast(Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return Sse.MultiplyScalar(v1, Sse.ReciprocalSqrtScalar(v1)).ToScalar();
        }

        /// <summary>
        /// Returns the magnitude of a vector, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeFast(ref Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return Sse.MultiplyScalar(v1, Sse.ReciprocalSqrtScalar(v1)).ToScalar();
        }

        /// <summary>
        /// Returns the squared magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSquared(Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return v1.ToScalar();
        }

        /// <summary>
        /// Returns the squared magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSquared(ref Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return v1.ToScalar();
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Normalize(Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> v1 = Sse.Sqrt(Sse41.DotProduct(v0, v0, 0b_1111_1111));
            return VectorIntrinsics.Store4(Sse.Divide(v0, v1));
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Normalize(ref Vector4 vector, out Vector4 result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> v1 = Sse.Sqrt(Sse41.DotProduct(v0, v0, 0b_1111_1111));
            result = VectorIntrinsics.Store4(Sse.Divide(v0, v1));
        }

        /// <summary>
        /// Scale a vector to approximately unit length, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 NormalizeFast(Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> v1 = Sse.ReciprocalSqrt(Sse41.DotProduct(v0, v0, 0b_1111_1111));
            return VectorIntrinsics.Store4(Sse.Multiply(v0, v1));
        }

        /// <summary>
        /// Scale a vector to approximately unit length, with 1.5*2^-12 relative error.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void NormalizeFast(ref Vector4 vector, out Vector4 result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> v1 = Sse.ReciprocalSqrt(Sse41.DotProduct(v0, v0, 0b_1111_1111));
            result = VectorIntrinsics.Store4(Sse.Multiply(v0, v1));
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Add(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return VectorIntrinsics.Store4(Sse.Add(a0, b0));
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Add(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse.Add(a0, b0));
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Subtract(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return VectorIntrinsics.Store4(Sse.Subtract(a0, b0));
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Subtract(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse.Subtract(a0, b0));
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Negate(Vector4 vector)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> n0 = Vector128.Create(-0f);
            return VectorIntrinsics.Store4(Sse.Xor(v0, n0));
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Negate(ref Vector4 vector, out Vector4 result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> n0 = Vector128.Create(-0f);
            result = VectorIntrinsics.Store4(Sse.Xor(v0, n0));
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Multiply(Vector4 vector, float scale)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> s0 = Vector128.Create(scale);
            return VectorIntrinsics.Store4(Sse.Multiply(v0, s0));
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Multiply(ref Vector4 vector, float scale, out Vector4 result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> s0 = Vector128.Create(scale);
            result = VectorIntrinsics.Store4(Sse.Multiply(v0, s0));
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Multiply(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return VectorIntrinsics.Store4(Sse.Multiply(a0, b0));
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Multiply(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse.Multiply(a0, b0));
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divisor">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Divide(Vector4 vector, float divisor)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(vector);
            Vector128<float> s0 = Vector128.Create(divisor);
            return VectorIntrinsics.Store4(Sse.Divide(v0, s0));
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divisor">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Divide(ref Vector4 vector, float divisor, out Vector4 result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<float> s0 = Vector128.Create(divisor);
            result = VectorIntrinsics.Store4(Sse.Divide(v0, s0));
        }

        /// <summary>
        /// Divides a vector component-wise by another vector.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Divide(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return VectorIntrinsics.Store4(Sse.Divide(a0, b0));
        }

        /// <summary>
        /// Divides a vector component-wise by another vector.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Divide(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse.Divide(a0, b0));
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Dot(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            return Sse41.DotProduct(a0, b0, 0b_1111_0001).ToScalar();
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Dot(ref Vector4 a, ref Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            return Sse41.DotProduct(a0, b0, 0b_1111_0001).ToScalar();
        }

        /// <summary>
        /// Projects a vector along a normal.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal the vector is projected on to.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Project(Vector4 vector, Vector4 normal)
        {
            Vector128<float> n0 = VectorIntrinsics.Load(normal);
            Vector128<float> sqrLen = Sse41.DotProduct(n0, n0, 0b_1111_1111);

            if (sqrLen.ToScalar() < float.Epsilon)
            {
                return Zero;
            }
            else
            {
                Vector128<float> v0 = VectorIntrinsics.Load(vector);
                Vector128<float> m0 = Sse.Divide(Sse41.DotProduct(v0, n0, 0b_1111_1111), sqrLen);
                return VectorIntrinsics.Store4(Sse.Multiply(n0, m0));
            }
        }

        /// <summary>
        /// Projects a vector along a normal.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal the vector is projected on to.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Project(ref Vector4 vector, ref Vector4 normal, out Vector4 result)
        {
            Vector128<float> n0 = VectorIntrinsics.Load(ref normal);
            Vector128<float> sqrLen = Sse41.DotProduct(n0, n0, 0b_1111_1111);

            if (sqrLen.ToScalar() < float.Epsilon)
            {
                result = Zero;
            }
            else
            {
                Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
                Vector128<float> m0 = Sse.Divide(Sse41.DotProduct(v0, n0, 0b_1111_1111), sqrLen);
                result = VectorIntrinsics.Store4(Sse.Multiply(n0, m0));
            }
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Distance(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);

            Vector128<float> v0 = Sse.Subtract(a0, b0);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return Sse.SqrtScalar(v1).ToScalar();
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Distance(ref Vector4 a, ref Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);

            Vector128<float> v0 = Sse.Subtract(a0, b0);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return Sse.SqrtScalar(v1).ToScalar();
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSquared(Vector4 a, Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);

            Vector128<float> v0 = Sse.Subtract(a0, b0);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return v1.ToScalar();
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSquared(ref Vector4 a, ref Vector4 b)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);

            Vector128<float> v0 = Sse.Subtract(a0, b0);
            Vector128<float> v1 = Sse41.DotProduct(v0, v0, 0b_1111_0001);
            return v1.ToScalar();
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);
            Vector128<float> t0 = Vector128.Create(t);
            return VectorIntrinsics.Store4(Fma.MultiplySubtract(t0, b0, Fma.MultiplySubtract(t0, a0, a0)));
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Lerp(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);
            Vector128<float> t0 = Vector128.Create(t);
            result = VectorIntrinsics.Store4(Fma.MultiplySubtract(t0, b0, Fma.MultiplySubtract(t0, a0, a0)));
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 LerpClamped(Vector4 a, Vector4 b, float t)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);

            Vector128<float> t0 = Vector128.Create(t);
            Vector128<float> min = Vector128<float>.Zero;
            Vector128<float> max = Vector128.Create(1f);

            t0 = Sse.Min(t0, max);
            t0 = Sse.Max(t0, min);
            return VectorIntrinsics.Store4(Fma.MultiplySubtract(t0, b0, Fma.MultiplySubtract(t0, a0, a0)));
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void LerpClamped(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);

            Vector128<float> t0 = Vector128.Create(t);
            Vector128<float> min = Vector128<float>.Zero;
            Vector128<float> max = Vector128.Create(1f);

            t0 = Sse.Min(t0, max);
            t0 = Sse.Max(t0, min);
            result = VectorIntrinsics.Store4(Fma.MultiplySubtract(t0, b0, Fma.MultiplySubtract(t0, a0, a0)));
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 MoveTowards(Vector4 a, Vector4 b, float maxDelta)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(a);
            Vector128<float> b0 = VectorIntrinsics.Load(b);

            Vector128<float> delta = Sse.Subtract(b0, a0);
            Vector128<float> len = Sse.Sqrt(Sse41.DotProduct(delta, delta, 0b_1111_1111));

            if (len.ToScalar() <= maxDelta)
            {
                return b;
            }
            else
            {
                delta = Sse.Multiply(Sse.Divide(delta, len), Vector128.Create(maxDelta));
                return VectorIntrinsics.Store4(Sse.Add(a0, delta));
            }
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void MoveTowards(ref Vector4 a, ref Vector4 b, float maxDelta, out Vector4 result)
        {
            Vector128<float> a0 = VectorIntrinsics.Load(ref a);
            Vector128<float> b0 = VectorIntrinsics.Load(ref b);

            Vector128<float> delta = Sse.Subtract(b0, a0);
            Vector128<float> len = Sse.Sqrt(Sse41.DotProduct(delta, delta, 0b_1111_1111));

            if (len.ToScalar() <= maxDelta)
            {
                result = b;
            }
            else
            {
                delta = Sse.Multiply(Sse.Divide(delta, len), Vector128.Create(maxDelta));
                result = VectorIntrinsics.Store4(Sse.Add(a0, delta));
            }
        }

        ///// <summary>
        ///// Rotates a vector.
        ///// </summary>
        ///// <param name="vector">The vector to transform.</param>
        ///// <param name="rotation">The rotation to apply.</param>
        //[MethodImpl(METHOD_OPTIONS)]
        //public static Vector4 Rotate(Vector4 vector, Quaternion rotation)
        //{
        //    Rotate(ref vector, ref rotation, out vector);
        //    return vector;
        //}

        ///// <summary>
        ///// Rotates a vector.
        ///// </summary>
        ///// <param name="vector">The vector to transform.</param>
        ///// <param name="rotation">The rotation to apply.</param>
        ///// <param name="result">The transformed vector as an output parameter.</param>
        //[MethodImpl(METHOD_OPTIONS)]
        //public static void Rotate(ref Vector4 vector, ref Quaternion rotation, out Vector4 result)
        //{
        //    Quaternion v = (Quaternion)vector;
        //    Quaternion.Invert(ref rotation, out Quaternion invRotation);
        //    Quaternion.Multiply(ref rotation, ref v, out Quaternion t);
        //    Quaternion.Multiply(ref t, ref invRotation, out v);

        //    result.x = v.x;
        //    result.y = v.y;
        //    result.z = v.z;
        //    result.w = v.w;
        //}

        ///// <summary>
        ///// Applies a rotation to all vectors within array and places the results in an another array.
        ///// </summary>
        ///// <param name="srcArray">The vectors to transform.</param>
        ///// <param name="rotation">The rotation to apply.</param>
        ///// <param name="destArray">The array transformed vectors are output to.</param>
        //public static void Rotate(Vector4[] srcArray, ref Quaternion rotation, Vector4[] destArray)
        //{
        //    if (srcArray == null)
        //    {
        //        throw new ArgumentNullException("srcArray");
        //    }
        //    if (destArray == null)
        //    {
        //        throw new ArgumentNullException("destArray");
        //    }
        //    if (destArray.Length < srcArray.Length)
        //    {
        //        throw new ArgumentException("Destination array is smaller than source array.");
        //    }

        //    for (int i = 0; i < srcArray.Length; i++)
        //    {
        //        Rotate(ref srcArray[i], ref rotation, out destArray[i]);
        //    }
        //}

        ///// <summary>
        ///// Applies a rotation to all vectors within array and places the results in an another array.
        ///// </summary>
        ///// <param name="srcArray">The vectors to transform.</param>
        ///// <param name="srcIndex">The starting index in the source array.</param>
        ///// <param name="rotation">The rotation to apply.</param>
        ///// <param name="destArray">The array transformed vectors are output to.</param>
        ///// <param name="destIndex">The starting index in the destination array.</param>
        ///// <param name="length">The number of vectors to be transformed.</param>
        //public static void Rotate(Vector4[] srcArray, int srcIndex, ref Quaternion rotation, Vector4[] destArray, int destIndex, int length)
        //{
        //    if (srcArray == null)
        //    {
        //        throw new ArgumentNullException("srcArray");
        //    }
        //    if (destArray == null)
        //    {
        //        throw new ArgumentNullException("destArray");
        //    }
        //    if (srcArray.Length < srcIndex + length)
        //    {
        //        throw new ArgumentException("Source array length is lesser than srcIndex + length");
        //    }
        //    if (destArray.Length < destIndex + length)
        //    {
        //        throw new ArgumentException("Destination array length is lesser than destIndex + length");
        //    }

        //    for (int i = 0; i < length; i++)
        //    {
        //        Rotate(ref srcArray[srcIndex + i], ref rotation, out destArray[destIndex + i]);
        //    }
        //}

        ///// <summary>
        ///// Transform a vector.
        ///// </summary>
        ///// <param name="vector">The vector to transform.</param>
        ///// <param name="matrix">The transformation to apply.</param>
        //[MethodImpl(METHOD_OPTIONS)]
        //public static Vector4 Transform(Vector4 vector, Matrix matrix)
        //{
        //    Transform(ref vector, ref matrix, out Vector4 result);
        //    return result;
        //}

        ///// <summary>
        ///// Transform a vector.
        ///// </summary>
        ///// <param name="vector">The vector to transform.</param>
        ///// <param name="matrix">The transformation to apply.</param>
        ///// <param name="result">The transformed vector as an output parameter.</param>
        //[MethodImpl(METHOD_OPTIONS)]
        //public static void Transform(ref Vector4 vector, ref Matrix matrix, out Vector4 result)
        //{
        //    result.x = (vector.x * matrix.m00) + (vector.y * matrix.m10) + (vector.z * matrix.m20) + (vector.w * matrix.m30);
        //    result.y = (vector.x * matrix.m01) + (vector.y * matrix.m11) + (vector.z * matrix.m21) + (vector.w * matrix.m31);
        //    result.z = (vector.x * matrix.m02) + (vector.y * matrix.m12) + (vector.z * matrix.m22) + (vector.w * matrix.m32);
        //    result.w = (vector.x * matrix.m03) + (vector.y * matrix.m13) + (vector.z * matrix.m23) + (vector.w * matrix.m33);
        //}

        ///// <summary>
        ///// Applies a transformation to all vectors within array and places the results in an another array.
        ///// </summary>
        ///// <param name="srcArray">The vectors to transform.</param>
        ///// <param name="srcIndex">The starting index in the source array.</param>
        ///// <param name="matrix">The transformation to apply.</param>
        ///// <param name="destArray">The array transformed vectors are output to.</param>
        ///// <param name="destIndex">The starting index in the destination array.</param>
        ///// <param name="length">The number of vectors to be transformed.</param>
        //public static void Transform(Vector4[] srcArray, ref Matrix matrix, Vector4[] destArray)
        //{
        //    if (srcArray == null)
        //    {
        //        throw new ArgumentNullException("srcArray");
        //    }
        //    if (destArray == null)
        //    {
        //        throw new ArgumentNullException("destArray");
        //    }
        //    if (destArray.Length < srcArray.Length)
        //    {
        //        throw new ArgumentException("Destination array is smaller than source array.");
        //    }

        //    for (int i = 0; i < srcArray.Length; i++)
        //    {
        //        Transform(ref srcArray[i], ref matrix, out destArray[i]);
        //    }
        //}

        ///// <summary>
        ///// Applies a transformation to all vectors within array and places the results in an another array.
        ///// </summary>
        ///// <param name="srcArray">The vectors to transform.</param>
        ///// <param name="srcIndex">The starting index in the source array.</param>
        ///// <param name="matrix">The transformation to apply.</param>
        ///// <param name="destArray">The array transformed vectors are output to.</param>
        ///// <param name="destIndex">The starting index in the destination array.</param>
        ///// <param name="length">The number of vectors to be transformed.</param>
        //public static void Transform(Vector4[] srcArray, int srcIndex, ref Matrix matrix, Vector4[] destArray, int destIndex, int length)
        //{
        //    if (srcArray == null)
        //    {
        //        throw new ArgumentNullException("srcArray");
        //    }
        //    if (destArray == null)
        //    {
        //        throw new ArgumentNullException("destArray");
        //    }
        //    if (srcArray.Length < srcIndex + length)
        //    {
        //        throw new ArgumentException("Source array length is lesser than sourceIndex + length");
        //    }
        //    if (destArray.Length < destIndex + length)
        //    {
        //        throw new ArgumentException("Destination array length is lesser than destinationIndex + length");
        //    }

        //    for (int i = 0; i < length; i++)
        //    {
        //        Transform(ref srcArray[srcIndex + i], ref matrix, out destArray[destIndex + i]);
        //    }
        //}

        /// <summary>
        /// Compares whether the current instance is equal to a specified instance.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public bool Equals(Vector4 other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }

        /// <summary>
        /// Compares whether the current instance is equal to a specified instance.
        /// </summary>
        /// <param name="obj">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public override bool Equals(object obj)
        {
            return (obj is Vector4) && Equals((Vector4)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                hashCode = (hashCode * 397) ^ w.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            const string format = "F2";
            return $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)}, {w.ToString(format)})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator +(Vector4 left, Vector4 right) => Add(left, right);

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator -(Vector4 left, Vector4 right) => Subtract(left, right);

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator -(Vector4 vector) => Negate(vector);

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator *(Vector4 vector, float scale) => Multiply(vector, scale);

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator *(float scale, Vector4 vector) => Multiply(vector, scale);

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator *(Vector4 left, Vector4 right) => Multiply(left, right);

        /// <summary>
        /// Divides the components of a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divisor">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator /(Vector4 vector, float divisor) => Divide(vector, divisor);

        /// <summary>
        /// Component-wise division of one vector by another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 operator /(Vector4 left, Vector4 right) => Divide(left, right);

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return
                left.x == right.x &&
                left.y == right.y &&
                left.z == right.z &&
                left.w == right.w;
        }

        /// <summary>
        /// Compares whether two vectors are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>	
        [MethodImpl(METHOD_OPTIONS)]
        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static implicit operator Vector2(Vector4 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static implicit operator Vector3(Vector4 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        /// <summary>
        /// Cast the vector to a <see cref="Color"/> by mapping xyzw to rgba.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        //[MethodImpl(METHOD_OPTIONS)]
        //public static explicit operator Color(Vector4 vector)
        //{
        //    return new Color(vector.x, vector.y, vector.z, vector.w);
        //}

        /// <summary>
        /// Cast the vector to a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        //[MethodImpl(METHOD_OPTIONS)]
        //public static explicit operator Quaternion(Vector4 vector)
        //{
        //    return new Quaternion(vector.x, vector.y, vector.z, vector.w);
        //}
    }
}
