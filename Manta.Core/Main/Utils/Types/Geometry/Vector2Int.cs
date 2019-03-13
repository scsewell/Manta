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
    /// Describes an integer 2d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct Vector2Int : IEquatable<Vector2Int>
    {
        private const MethodImplOptions METHOD_OPTIONS = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        /// <summary>
        /// Returns a vector with components (0, 0).
        /// </summary>
        public static readonly Vector2Int Zero = new Vector2Int(0, 0);

        /// <summary>
        /// Returns a vector with components (1, 1).
        /// </summary>
        public static readonly Vector2Int One = new Vector2Int(1, 1);

        /// <summary>
        /// Returns a vector with components (1, 0).
        /// </summary>
        public static readonly Vector2Int UnitX = new Vector2Int(1, 0);

        /// <summary>
        /// Returns a vector with components (0, 1).
        /// </summary>
        public static readonly Vector2Int UnitY = new Vector2Int(0, 1);

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public int x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public int y;

        /// <summary>
        /// Constructs a 2d vector from two values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        public Vector2Int(int value)
        {
            x = value;
            y = value;
        }

        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        public unsafe int this[int index]
        {
            get
            {
                Debug.Assert(0 <= index && index <= 1, $"Index out of range: {index}");
                return *((int*)Unsafe.AsPointer(ref this) + index);
            }
            set
            {
                Debug.Assert(0 <= index && index <= 1, $"Index out of range: {index}");
                *((int*)Unsafe.AsPointer(ref this) + index) = value;
            }
        }

        /// <summary>
        /// Returns the magnitude of this vector.
        /// </summary>
        public float Length => Magnitude(this);

        /// <summary>
        /// Returns the squared magnitude of this vector.
        /// </summary>
        public int LengthSquared => MagnitudeSquared(this);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ComponentMin(Vector2Int a, Vector2Int b)
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
        public static void ComponentMin(ref Vector2Int a, ref Vector2Int b, out Vector2Int result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ComponentMax(Vector2Int a, Vector2Int b)
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
        public static unsafe void ComponentMax(ref Vector2Int a, ref Vector2Int b, out Vector2Int result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ComponentClamp(Vector2Int vector, Vector2Int min, Vector2Int max)
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
        public static unsafe void ComponentClamp(ref Vector2Int vector, ref Vector2Int min, ref Vector2Int max, out Vector2Int result)
        {
            result.x = Mathf.Max(Mathf.Min(vector.x, max.x), min.x);
            result.y = Mathf.Max(Mathf.Min(vector.y, max.y), min.y);
        }

        /// <summary>
        /// Returns an integer vector with the ceiling of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Ceil(Vector2 vector)
        {
            Ceil(ref vector, out Vector2Int result);
            return result;
        }

        /// <summary>
        /// Returns an integer vector with the ceiling of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Ceil(ref Vector2 vector, out Vector2Int result)
        {
            result.x = Mathf.CeilToInt(vector.x);
            result.y = Mathf.CeilToInt(vector.y);
        }

        /// <summary>
        /// Returns an integer vector with the floor of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Floor(Vector2 vector)
        {
            Floor(ref vector, out Vector2Int result);
            return result;
        }

        /// <summary>
        /// Returns an integer vector with the floor of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Floor(ref Vector2 vector, out Vector2Int result)
        {
            result.x = Mathf.FloorToInt(vector.x);
            result.y = Mathf.FloorToInt(vector.y);
        }

        /// <summary>
        /// Returns an integer vector with each component rounded.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Round(Vector2 vector)
        {
            Round(ref vector, out Vector2Int result);
            return result;
        }

        /// <summary>
        /// Returns an integer vector with each component rounded to the nearest integer.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Round(ref Vector2 vector, out Vector2Int result)
        {
            result.x = Mathf.RoundToInt(vector.x);
            result.y = Mathf.RoundToInt(vector.y);
        }

        /// <summary>
        /// Returns the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Magnitude(Vector2Int vector)
        {
            return Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
        }

        /// <summary>
        /// Returns the squared magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static int MagnitudeSquared(Vector2Int vector)
        {
            return (vector.x * vector.x) + (vector.y * vector.y);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Add(Vector2Int left, Vector2Int right)
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
        public static unsafe void Add(ref Vector2Int left, ref Vector2Int right, out Vector2Int result)
        {
            result.x = left.x + right.x;
            result.y = left.y + right.y;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Subtract(Vector2Int left, Vector2Int right)
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
        public static unsafe void Subtract(ref Vector2Int left, ref Vector2Int right, out Vector2Int result)
        {
            result.x = left.x - right.x;
            result.y = left.y - right.y;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Negate(Vector2Int vector)
        {
            Negate(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe void Negate(ref Vector2Int vector, out Vector2Int result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Scale(Vector2Int vector, int scale)
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
        public static unsafe void Scale(ref Vector2Int vector, int scale, out Vector2Int result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int Multiply(Vector2Int left, Vector2Int right)
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
        public static unsafe void Multiply(ref Vector2Int left, ref Vector2Int right, out Vector2Int result)
        {
            result.x = left.x * right.x;
            result.y = left.y * right.y;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static float Distance(Vector2Int a, Vector2Int b)
        {
            return Distance(ref a, ref b);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe float Distance(ref Vector2Int a, ref Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return Mathf.Sqrt((dx * dx) + (dy * dy));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquared(Vector2Int a, Vector2Int b)
        {
            return DistanceSquared(ref a, ref b);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe int DistanceSquared(ref Vector2Int a, ref Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return (dx * dx) + (dy * dy);
        }

        /// <summary>
        /// Compares whether the current instance is equal to a specified instance.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public bool Equals(Vector2Int other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Compares whether the current instance is equal to a specified instance.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public unsafe bool Equals(ref Vector2Int other)
        {
            return
                x == other.x &&
                y == other.y;
        }

        /// <summary>
        /// Compares whether the current instance is equal to specified instance.
        /// </summary>
        /// <param name="obj">The instance to compare.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public override bool Equals(object obj)
        {
            return (obj is Vector2Int) && Equals((Vector2Int)obj);
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
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        [MethodImpl(METHOD_OPTIONS)]
        public override string ToString()
        {
            return $"({x}, {y})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int operator +(Vector2Int left, Vector2Int right) => Add(left, right);

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int operator -(Vector2Int left, Vector2Int right) => Subtract(left, right);

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int operator -(Vector2Int vector) => Negate(vector);

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int operator *(Vector2Int vector, int scale) => Scale(vector, scale);

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Vector operand.</param>
        /// <param name="scale">Scalar operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int operator *(int scale, Vector2Int vector) => Scale(vector, scale);

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int operator *(Vector2Int left, Vector2Int right) => Multiply(left, right);

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static bool operator ==(Vector2Int left, Vector2Int right)
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
        [MethodImpl(METHOD_OPTIONS)]
        public static bool operator !=(Vector2Int left, Vector2Int right)
        {
            return
                left.x != right.x ||
                left.y != right.y;
        }
        
        /// <summary>
        /// Cast the vector as a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        [MethodImpl(METHOD_OPTIONS)]
        public static explicit operator Vector2(Vector2Int vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}
