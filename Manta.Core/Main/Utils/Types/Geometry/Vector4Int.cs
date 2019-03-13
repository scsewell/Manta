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
    /// Describes an integer 4d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector4Int : IEquatable<Vector4Int>
    {
        private const MethodImplOptions METHOD_OPTIONS = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        /// <summary>
        /// Returns a vector with components (0, 0, 0, 0).
        /// </summary>
        public static readonly Vector4Int Zero = new Vector4Int(0, 0, 0, 0);

        /// <summary>
        /// Returns a vector with components (1, 1, 1, 1).
        /// </summary>
        public static readonly Vector4Int One = new Vector4Int(1, 1, 1, 1);

        /// <summary>
        /// Returns a vector with components (1, 0, 0, 0).
        /// </summary>
        public static readonly Vector4Int UnitX = new Vector4Int(1, 0, 0, 0);

        /// <summary>
        /// Returns a vector with components (0, 1, 0, 0).
        /// </summary>
        public static readonly Vector4Int UnitY = new Vector4Int(0, 1, 0, 0);

        /// <summary>
        /// Returns a vector with components (0, 0, 1, 0).
        /// </summary>
        public static readonly Vector4Int UnitZ = new Vector4Int(0, 0, 1, 0);

        /// <summary>
        /// Returns a vector with components (0, 0, 0, 1).
        /// </summary>
        public static readonly Vector4Int UnitW = new Vector4Int(0, 0, 0, 1);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public int x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public int y;

        /// <summary>
        /// The z coordinate.
        /// </summary>
        public int z;

        /// <summary>
        /// The w coordinate.
        /// </summary>
        public int w;

        /// <summary>
        /// Constructs a 4d vector from four values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4Int(int x, int y, int z, int w)
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
        public Vector4Int(int value)
        {
            x = value;
            y = value;
            z = value;
            w = value;
        }

        /// <summary>
        /// Constructs a 4d vector with X and Z from a <see cref="Vector2Int"/> and Z and W from scalars.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4Int(Vector2Int value, int z, int w)
        {
            x = value.x;
            y = value.y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a 4d vector from a pair of <see cref="Vector2Int"/>.
        /// </summary>
        /// <param name="xy">The x and y coordinates.</param>
        /// <param name="xy">The z and w coordinates.</param>
        public Vector4Int(Vector2Int xy, Vector2Int zw)
        {
            x = xy.x;
            y = xy.y;
            z = zw.x;
            w = zw.y;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z from <see cref="Vector3Int"/> and W from a scalar.
        /// </summary>
        /// <param name="value">The x, y and z coordinates.</param>
        /// <param name="w">The w coordinate.</param>
        public Vector4Int(Vector3Int value, int w)
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
        public unsafe int this[int index]
        {
            get
            {
                Debug.Assert(0 <= index && index <= 3, $"Index out of range: {index}");
                return *((int*)Unsafe.AsPointer(ref this) + index);
            }
            set
            {
                Debug.Assert(0 <= index && index <= 3, $"Index out of range: {index}");
                *((int*)Unsafe.AsPointer(ref this) + index) = value;
            }
        }

        /// <summary>
        /// Returns the magnitude of this vector.
        /// </summary>
        public float Length => Magnitude(ref this);

        /// <summary>
        /// Returns the squared magnitude of this vector.
        /// </summary>
        public int LengthSquared => MagnitudeSquared(ref this);

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Magnitude(ref Vector4Int vector)
        {
            Vector128<int> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<int> v1 = Sse41.MultiplyLow(v0, v0);
            Vector128<int> v2 = Ssse3.HorizontalAdd(v1, v1);
            Vector128<int> v3 = Ssse3.HorizontalAdd(v2, v2);
            return Sse.SqrtScalar(Sse2.ConvertToVector128Single(v3)).ToScalar();
        }

        /// <summary>
        /// Returns the squared magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static int MagnitudeSquared(ref Vector4Int vector)
        {
            return (vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z) + (vector.w * vector.w);
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ComponentMin(Vector4Int a, Vector4Int b)
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
        public static void ComponentMin(ref Vector4Int a, ref Vector4Int b, out Vector4Int result)
        {
            Vector128<int> a0 = VectorIntrinsics.Load(ref a);
            Vector128<int> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse41.Min(a0, b0));
        }
        
        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ComponentMax(Vector4Int a, Vector4Int b)
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
        public static void ComponentMax(ref Vector4Int a, ref Vector4Int b, out Vector4Int result)
        {
            Vector128<int> a0 = VectorIntrinsics.Load(ref a);
            Vector128<int> b0 = VectorIntrinsics.Load(ref b);
            result = VectorIntrinsics.Store4(Sse41.Max(a0, b0));
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ComponentClamp(Vector4Int vector, Vector4Int min, Vector4Int max)
        {
            ComponentClamp(ref vector, ref min, ref max, out vector);
            return vector;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentClamp(ref Vector4Int vector, ref Vector4Int min, ref Vector4Int max, out Vector4Int result)
        {
            Vector128<int> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<int> min0 = VectorIntrinsics.Load(ref min);
            Vector128<int> max0 = VectorIntrinsics.Load(ref max);
            result = VectorIntrinsics.Store4(Sse41.Max(Sse41.Min(v0, max0), min0));
        }

        /// <summary>
        /// Returns an integer vector with the ceiling of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Ceil(Vector4 vector)
        {
            Ceil(ref vector, out Vector4Int result);
            return result;
        }

        /// <summary>
        /// Returns an integer vector with the ceiling of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Ceil(ref Vector4 vector, out Vector4Int result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            result = VectorIntrinsics.Store4(Sse2.ConvertToVector128Int32(Sse41.Ceiling(v0)));
        }

        /// <summary>
        /// Returns an integer vector with the floor of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Floor(Vector4 vector)
        {
            Floor(ref vector, out Vector4Int result);
            return result;
        }

        /// <summary>
        /// Returns an integer vector with the floor of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Floor(ref Vector4 vector, out Vector4Int result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            result = VectorIntrinsics.Store4(Sse2.ConvertToVector128Int32(Sse41.Floor(v0)));
        }

        /// <summary>
        /// Returns an integer vector with each component rounded.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Round(Vector4 vector)
        {
            Round(ref vector, out Vector4Int result);
            return result;
        }

        /// <summary>
        /// Returns an integer vector with each component rounded to the nearest integer.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Round(ref Vector4 vector, out Vector4Int result)
        {
            Vector128<float> v0 = VectorIntrinsics.Load(ref vector);
            result = VectorIntrinsics.Store4(Sse2.ConvertToVector128Int32(Sse41.RoundToNearestInteger(v0)));
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Add(Vector4Int left, Vector4Int right)
        {
            Add(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Add(ref Vector4Int left, ref Vector4Int right, out Vector4Int result)
        {
            Vector128<int> l0 = VectorIntrinsics.Load(ref left);
            Vector128<int> r0 = VectorIntrinsics.Load(ref right);
            result = VectorIntrinsics.Store4(Sse2.Add(l0, r0));
        }
        
        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Subtract(Vector4Int left, Vector4Int right)
        {
            Subtract(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Subtract(ref Vector4Int left, ref Vector4Int right, out Vector4Int result)
        {
            Vector128<int> l0 = VectorIntrinsics.Load(ref left);
            Vector128<int> r0 = VectorIntrinsics.Load(ref right);
            result = VectorIntrinsics.Store4(Sse2.Subtract(l0, r0));
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Negate(Vector4Int vector)
        {
            Negate(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Negate(ref Vector4Int vector, out Vector4Int result)
        {
            Vector128<int> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<int> n0 = Vector128.Create(-1);
            result = VectorIntrinsics.Store4(Ssse3.Sign(v0, n0));
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Scale(Vector4Int vector, int scale)
        {
            Scale(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Scale(ref Vector4Int vector, int scale, out Vector4Int result)
        {
            Vector128<int> v0 = VectorIntrinsics.Load(ref vector);
            Vector128<int> n0 = Vector128.Create(scale);
            result = VectorIntrinsics.Store4(Sse41.MultiplyLow(v0, n0));
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int Multiply(Vector4Int left, Vector4Int right)
        {
            Multiply(ref left, ref right, out left);
            return left;
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static void Multiply(ref Vector4Int left, ref Vector4Int right, out Vector4Int result)
        {
            Vector128<int> l0 = VectorIntrinsics.Load(ref left);
            Vector128<int> r0 = VectorIntrinsics.Load(ref right);
            result = VectorIntrinsics.Store4(Sse41.MultiplyLow(l0, r0));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Distance(Vector4Int a, Vector4Int b)
        {
            return Distance(ref a, ref b);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Distance(ref Vector4Int a, ref Vector4Int b)
        {
            Vector128<int> a0 = VectorIntrinsics.Load(ref a);
            Vector128<int> b0 = VectorIntrinsics.Load(ref b);

            Vector128<int> v0 = Sse2.Subtract(a0, b0);
            Vector128<int> v1 = Sse41.MultiplyLow(v0, v0);
            Vector128<int> v2 = Ssse3.HorizontalAdd(v1, v1);
            Vector128<int> v3 = Ssse3.HorizontalAdd(v2, v2);
            return Sse.SqrtScalar(Sse2.ConvertToVector128Single(v3)).ToScalar();
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquared(Vector4Int a, Vector4Int b)
        {
            return DistanceSquared(ref a, ref b);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquared(ref Vector4Int a, ref Vector4Int b)
        {
            Vector128<int> a0 = VectorIntrinsics.Load(ref a);
            Vector128<int> b0 = VectorIntrinsics.Load(ref b);

            Vector128<int> v0 = Sse2.Subtract(a0, b0);
            Vector128<int> v1 = Sse41.MultiplyLow(v0, v0);
            Vector128<int> v2 = Ssse3.HorizontalAdd(v1, v1);
            Vector128<int> v3 = Ssse3.HorizontalAdd(v2, v2);
            return v3.ToScalar();
        }

        /// <summary>
        /// Compares whether the current instance is equal to a specified instance.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public bool Equals(Vector4Int other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }

        /// <summary>
        /// Compares whether the current instance is equal to specified instance.
        /// </summary>
        /// <param name="obj">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public override bool Equals(object obj)
        {
            return (obj is Vector4Int) && Equals((Vector4Int)obj);
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
            return $"({x}, {y}, {z}, {w})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int operator +(Vector4Int left, Vector4Int right) => Add(left, right);

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int operator -(Vector4Int left, Vector4Int right) => Subtract(left, right);

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int operator -(Vector4Int vector) => Negate(vector);

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int operator *(Vector4Int vector, int scale) => Scale(vector, scale);

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int operator *(int scale, Vector4Int vector) => Scale(vector, scale);

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int operator *(Vector4Int left, Vector4Int right) => Multiply(left, right);

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool operator ==(Vector4Int left, Vector4Int right)
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
        public static bool operator !=(Vector4Int left, Vector4Int right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2Int"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static implicit operator Vector2Int(Vector4Int vector)
        {
            return new Vector2Int(vector.x, vector.y);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector3Int"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static implicit operator Vector3Int(Vector4Int vector)
        {
            return new Vector3Int(vector.x, vector.y, vector.z);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static explicit operator Vector4(Vector4Int vector)
        {
            Vector128<int> v0 = VectorIntrinsics.Load(ref vector);
            return VectorIntrinsics.Store4(Sse2.ConvertToVector128Single(v0));
        }
    }
}
