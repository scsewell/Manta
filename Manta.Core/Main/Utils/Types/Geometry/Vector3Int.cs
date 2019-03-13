/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.InteropServices;

namespace Manta
{
    /// <summary>
    /// Describes an integer 3d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector3Int : IEquatable<Vector3Int>
    {
        /// <summary>
        /// Returns a vector with components (0, 0, 0).
        /// </summary>
        public static readonly Vector3Int Zero = new Vector3Int(0, 0, 0);

        /// <summary>
        /// Returns a vector with components (1, 1, 1).
        /// </summary>
        public static readonly Vector3Int One = new Vector3Int(1, 1, 1);

        /// <summary>
        /// Returns a vector with components (1, 0, 0).
        /// </summary>
        public static readonly Vector3Int UnitX = new Vector3Int(1, 0, 0);

        /// <summary>
        /// Returns a vector with components (0, 1, 0).
        /// </summary>
        public static readonly Vector3Int UnitY = new Vector3Int(0, 1, 0);

        /// <summary>
        /// Returns a vector with components (0, 0, 1).
        /// </summary>
        public static readonly Vector3Int UnitZ = new Vector3Int(0, 0, 1);

        /// <summary>
        /// Returns a vector with components (-1, 0, 0).
        /// </summary>
        public static readonly Vector3Int Left = new Vector3Int(-1, 0, 0);

        /// <summary>
        /// Returns a vector with components (1, 0, 0).
        /// </summary>
        public static readonly Vector3Int Right = new Vector3Int(1, 0, 0);

        /// <summary>
        /// Returns a vector with components (0, -1, 0).
        /// </summary>
        public static readonly Vector3Int Down = new Vector3Int(0, -1, 0);

        /// <summary>
        /// Returns a vector with components (0, 1, 0).
        /// </summary>
        public static readonly Vector3Int Up = new Vector3Int(0, 1, 0);

        /// <summary>
        /// Returns a vector with components (0, 0, -1).
        /// </summary>
        public static readonly Vector3Int Forward = new Vector3Int(0, 0, -1);

        /// <summary>
        /// Returns a vector with components (0, 0, 1).
        /// </summary>
        public static readonly Vector3Int Backward = new Vector3Int(0, 0, 1);

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
        /// Constructs a 3d vector from three values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Constructs a 2d vector with X, Y and Z set to the same value.
        /// </summary>
        /// <param name="value">The x, y and z coordinates.</param>
        public Vector3Int(int value)
        {
            x = value;
            y = value;
            z = value;
        }

        /// <summary>
        /// Constructs a 3d vector from a <see cref="Vector2Int"/> and a scalar.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3Int(Vector2Int value, int z)
        {
            x = value.x;
            y = value.y;
            this.z = z;
        }

        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 2.</exception>
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Returns the length of this vector.
        /// </summary>
        public float Length => Mathf.Sqrt((x * x) + (y * y) + (z * z));

        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y) + (z * z);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector3Int ComponentMin(Vector3Int a, Vector3Int b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector3Int ComponentMax(Vector3Int a, Vector3Int b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            return a;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector3Int ComponentClamp(Vector3Int vector, Vector3Int min, Vector3Int max)
        {
            return new Vector3Int(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector3Int a, Vector3Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            int dz = a.z - b.z;
            return Mathf.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector3Int a, Vector3Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            int dz = a.z - b.z;
            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Returns an integer vector from the <see cref="Mathf.CeilToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to ceil.</param>
        public static Vector3Int Ceil(Vector3 vector)
        {
            return new Vector3Int(
                Mathf.CeilToInt(vector.x),
                Mathf.CeilToInt(vector.y),
                Mathf.CeilToInt(vector.z));
        }

        /// <summary>
        /// Returns an integer vector with the <see cref="Mathf.FloorToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to floor.</param>
        public static Vector3Int Floor(Vector3 vector)
        {
            return new Vector3Int(
                Mathf.FloorToInt(vector.x),
                Mathf.FloorToInt(vector.y),
                Mathf.FloorToInt(vector.z));
        }

        /// <summary>
        /// Returns an integer vector with the <see cref="Mathf.RoundToInt(float)"> of each component.
        /// </summary>
        /// <param name="vector">The vector to round.</param>
        public static Vector3Int Round(Vector3 vector)
        {
            return new Vector3Int(
                Mathf.RoundToInt(vector.x),
                Mathf.RoundToInt(vector.y),
                Mathf.RoundToInt(vector.z));
        }

        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector3Int other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector3Int) && Equals((Vector3Int)obj);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector3Int operator +(Vector3Int left, Vector3Int right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            return left;
        }

        /// <summary>
        /// Subtracts the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static Vector3Int operator -(Vector3Int left, Vector3Int right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            return left;
        }

        /// <summary>
        /// Negates the specified vector.
        /// </summary>
        /// <param name="vector">Operand.</param>
        /// <returns>Result of the negation.</returns>
        public static Vector3Int operator -(Vector3Int vector)
        {
            vector.x = -vector.x;
            vector.y = -vector.y;
            vector.z = -vector.z;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector3Int operator *(Vector3Int vector, int scale)
        {
            vector.x *= scale;
            vector.y *= scale;
            vector.z *= scale;
            return vector;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector3Int operator *(int scale, Vector3Int vector)
        {
            vector.x *= scale;
            vector.y *= scale;
            vector.z *= scale;
            return vector;
        }

        /// <summary>
        /// Component-wise multiplication of two vectors.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static Vector3Int operator *(Vector3Int vector, Vector3Int scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            vector.z *= scale.z;
            return vector;
        }

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector3Int left, Vector3Int right)
        {
            return
                left.x == right.x &&
                left.y == right.y &&
                left.z == right.z;
        }

        /// <summary>
        /// Compares whether two vectors are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are not equal, <c>false</c> otherwise.</returns>	
        public static bool operator !=(Vector3Int left, Vector3Int right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2Int"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector2Int(Vector3Int vector)
        {
            return new Vector2Int(vector.x, vector.y);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static explicit operator Vector3(Vector3Int vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }
    }
}
