/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Manta
{
    /// <summary>
    /// Describes a 3d vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector3 : IEquatable<Vector3>
    {
        /// <summary>
        /// Returns a vector with components (0, 0, 0).
        /// </summary>
        public static readonly Vector3 Zero = new Vector3(0f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (1, 1, 1).
        /// </summary>
        public static readonly Vector3 One = new Vector3(1f, 1f, 1f);

        /// <summary>
        /// Returns a vector with components (1, 0, 0).
        /// </summary>
        public static readonly Vector3 UnitX = new Vector3(1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 1, 0).
        /// </summary>
        public static readonly Vector3 UnitY = new Vector3(0f, 1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 0, 1).
        /// </summary>
        public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);

        /// <summary>
        /// Returns a vector with components (-1, 0, 0).
        /// </summary>
        public static readonly Vector3 Left = new Vector3(-1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (1, 0, 0).
        /// </summary>
        public static readonly Vector3 Right = new Vector3(1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with components (0, -1, 0).
        /// </summary>
        public static readonly Vector3 Down = new Vector3(0f, -1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 1, 0).
        /// </summary>
        public static readonly Vector3 Up = new Vector3(0f, 1f, 0f);

        /// <summary>
        /// Returns a vector with components (0, 0, -1).
        /// </summary>
        public static readonly Vector3 Forward = new Vector3(0f, 0f, -1f);

        /// <summary>
        /// Returns a vector with components (0, 0, 1).
        /// </summary>
        public static readonly Vector3 Backward = new Vector3(0f, 0f, 1f);

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
        /// Constructs a 3d vector from three values.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Constructs a 3d vector with X, Y and Z set to the same value.
        /// </summary>
        /// <param name="value">The x, y and z coordinates.</param>
        public Vector3(float value)
        {
            x = value;
            y = value;
            z = value;
        }

        /// <summary>
        /// Constructs a 3d vector from a <see cref="Vector2"/> and a scalar.
        /// </summary>
        /// <param name="value">The x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3(Vector2 value, float z)
        {
            x = value.x;
            y = value.y;
            this.z = z;
        }
        /*
        /// <summary>
        /// Gets or sets the value at an index of the vector.
        /// </summary>
        /// <param name="index">The index of the component from the vector.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is less than 0 or greater than 2.</exception>
        public float this[int index]
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
        /// Returns an approximation for the length of this vector.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        public float LengthFast => 1f / Mathf.InverseSqrtFast((x * x) + (y * y) + (z * z));

        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        public float LengthSquared => (x * x) + (y * y) + (z * z);

        /// <summary>
        /// Returns a copy of the vector scaled to unit length.
        /// </summary>
        public Vector3 Normalized => Normalize(this);

        /// <summary>
        /// Returns a copy of the vector scaled to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        public Vector3 NormalizedFast => NormalizeFast(this);

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static Vector3 ComponentMin(Vector3 a, Vector3 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the smallest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The component-wise minimum.</param>
        public static void ComponentMin(ref Vector3 a, ref Vector3 b, out Vector3 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static Vector3 ComponentMax(Vector3 a, Vector3 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            return a;
        }

        /// <summary>
        /// Returns a vector created from the largest of the corresponding components of the given vectors.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The component-wise maximum.</param>
        public static void ComponentMax(ref Vector3 a, ref Vector3 b, out Vector3 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector3 ComponentClamp(Vector3 vector, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z));
        }

        /// <summary>
        /// Clamps the specified vector per component.
        /// </summary>
        /// <param name="vector">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        public static void ComponentClamp(ref Vector3 vector, ref Vector3 min, ref Vector3 max, out Vector3 result)
        {
            result.x = Mathf.Clamp(vector.x, min.x, max.x);
            result.y = Mathf.Clamp(vector.y, min.y, max.y);
            result.z = Mathf.Clamp(vector.z, min.z, max.z);
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static Vector3 Min(Vector3 left, Vector3 right)
        {
            return left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the lesser magnitude. If the magnitudes are equal, the second vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <param name="result">The magnitude-wise minimum.</param>
        public static void Min(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the greater magnitude.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        public static Vector3 Max(Vector3 left, Vector3 right)
        {
            return left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with the maximum magnitude. If the magnitudes are equal, the first vector
        /// is selected.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <param name="result">The magnitude-wise maximum.</param>
        public static void Max(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>
        /// Returns the vector with a magnitude less than or equal to the specified length.
        /// </summary>
        /// <param name="vector">The vector to clamp.</param>
        /// <param name="maxLength">The maxiumum maginude of the returned vector.</param>
        public static Vector3 MagnitudeClamp(Vector3 vector, float maxLength)
        {
            if (vector.LengthSquared > maxLength * maxLength)
            {
                return vector.Normalized * maxLength;
            }
            return vector;
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        public static Vector3 Add(Vector3 a, Vector3 b)
        {
            Add(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Performs vector addition.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The result of the vector addition.</param>
        public static void Add(ref Vector3 a, ref Vector3 b, out Vector3 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            result.z = a.z + b.z;
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        public static Vector3 Subtract(Vector3 a, Vector3 b)
        {
            Subtract(ref a, ref b, out a);
            return a;
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        public static void Subtract(ref Vector3 a, ref Vector3 b, out Vector3 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            result.z = a.z - b.z;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        public static Vector3 Negate(Vector3 vector)
        {
            Negate(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vector">The vector to negate.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        public static void Negate(ref Vector3 vector, out Vector3 result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
            result.z = -vector.z;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector3 Multiply(Vector3 vector, float scale)
        {
            Multiply(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Multiply(ref Vector3 vector, float scale, out Vector3 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector3 Multiply(Vector3 vector, Vector3 scale)
        {
            Multiply(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Multiplies a vector by the components another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Multiply(ref Vector3 vector, ref Vector3 scale, out Vector3 result)
        {
            result.x = vector.x * scale.x;
            result.y = vector.y * scale.y;
            result.z = vector.z * scale.z;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector3 Divide(Vector3 vector, float scale)
        {
            Divide(ref vector, scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Divide(ref Vector3 vector, float scale, out Vector3 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
            result.z = vector.z / scale;
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static Vector3 Divide(Vector3 vector, Vector3 scale)
        {
            Divide(ref vector, ref scale, out vector);
            return vector;
        }

        /// <summary>
        /// Divide a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <param name="result">Result of the operation.</param>
        public static void Divide(ref Vector3 vector, ref Vector3 scale, out Vector3 result)
        {
            result.x = vector.x / scale.x;
            result.y = vector.y / scale.y;
            result.z = vector.z / scale.z;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static float Dot(Vector3 a, Vector3 b)
        {
            Dot(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        public static void Dot(ref Vector3 a, ref Vector3 b, out float result)
        {
            result = (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            Cross(ref left, ref right, out Vector3 result);
            return result;
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="result">The cross product of two vectors as an output parameter.</param>
        public static void Cross(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result.x = (left.y * right.z) - (left.z * right.y);
            result.y = (left.z * right.x) - (left.x * right.z);
            result.z = (left.x * right.y) - (left.y * right.x);
        }

        /// <summary>
        /// Projects a vector along a normal.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal the vector is projected on to.</param>
        public static Vector3 Project(Vector3 vector, Vector3 normal)
        {
            Project(ref vector, ref normal, out vector);
            return vector;
        }

        /// <summary>
        /// Projects a vector along a normal.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal the vector is projected on to.</param>
        /// <param name="result">Projected vector as an output parameter.</param>
        public static void Project(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            float sqrLen = normal.LengthSquared;
            if (sqrLen > float.Epsilon)
            {
                result = normal * Dot(vector, normal) / sqrLen;
            }
            else
            {
                result = Zero;
            }
        }

        /// <summary>
        /// Projects a vector onto a plane.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal of the plane.</param>
        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 normal)
        {
            ProjectOnPlane(ref vector, ref normal, out vector);
            return vector;
        }

        /// <summary>
        /// Projects a vector onto a plane.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="normal">The normal of the plane.</param>
        /// <param name="result">Projected vector as an output parameter.</param>
        public static void ProjectOnPlane(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            result = vector - Project(vector, normal);
        }

        /// <summary>
        /// Reflects a vector about a normal.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="normal">The reflection normal.</param>
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            Reflect(ref vector, ref normal, out vector);
            return vector;
        }

        /// <summary>
        /// Reflects a vector about a normal.
        /// </summary>
        /// <param name="vector">The vector to reflect.</param>
        /// <param name="normal">The reflection normal.</param>
        /// <param name="result">Reflected vector as an output parameter.</param>
        public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            result = vector - (2f * Dot(vector, normal) * normal);
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        public static Vector3 Normalize(Vector3 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to unit length.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        /// <param name="result">The normalized vector as an output parameter.</param>
        public static void Normalize(ref Vector3 vector, out Vector3 result)
        {
            float scale = 1f / vector.Length;
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
        }

        /// <summary>
        /// Scale a vector to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        /// <param name="vector">The vector to normalize.</param>
        public static Vector3 NormalizeFast(Vector3 vector)
        {
            NormalizeFast(ref vector, out vector);
            return vector;
        }

        /// <summary>
        /// Scale a vector to approximately unit length.
        /// </summary>
        /// <remarks>
        /// This property uses an approximation of the square root function to calculate vector magnitude, with
        /// an upper error bound of 0.001.
        /// </remarks>
        /// <param name="vector">The vector to normalize.</param>
        /// <param name="result">The normalized vector as an output parameter.</param>
        public static void NormalizeFast(ref Vector3 vector, out Vector3 result)
        {
            float scale = Mathf.InverseSqrtFast(vector.LengthSquared);
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static float Distance(Vector3 a, Vector3 b)
        {
            Distance(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        public static void Distance(ref Vector3 a, ref Vector3 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            result = Mathf.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static float DistanceSquared(Vector3 a, Vector3 b)
        {
            DistanceSquared(ref a, ref b, out float result);
            return result;
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        public static void DistanceSquared(ref Vector3 a, ref Vector3 b, out float result)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            result = (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Returns the absolute value of the angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        public static float Angle(Vector3 from, Vector3 to)
        {
            Angle(ref from, ref to, out float result);
            return result;
        }

        /// <summary>
        /// Returns the absolute value of the angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        /// <param name="result">The angle between two vectors as an output parameter.</param>
        public static void Angle(ref Vector3 from, ref Vector3 to, out float result)
        {
            float denominator = Mathf.Sqrt(from.LengthSquared * to.LengthSquared);
            if (denominator < 1e-15f)
            {
                result = 0f;
            }
            else
            {
                result = Mathf.Acos(Mathf.Clamp(Dot(from, to) / denominator, -1f, 1f));
            }
        }

        /// <summary>
        /// Returns the signed angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        /// <param name="axis">The axis to measure the angle around.</param>
        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            SignedAngle(ref from, ref to, ref axis, out float result);
            return result;
        }

        /// <summary>
        /// Returns the signed angle between to vectors in radians.
        /// </summary>
        /// <param name="from">The vector to measure from.</param>
        /// <param name="to">The vector to measure towards.</param>
        /// <param name="axis">The axis to measure the angle around.</param>
        public static void SignedAngle(ref Vector3 from, ref Vector3 to, ref Vector3 axis, out float result)
        {
            result = Mathf.Sign(Dot(axis, Cross(from, to))) * Angle(from, to);
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
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
        public static void Lerp(ref Vector3 a, ref Vector3 b, float t, out Vector3 result)
        {
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
            result.z = (t * (b.z - a.z)) + a.z;
        }

        /// <summary>
        /// Linearly interpolates from a to b by factor t, where t is clamped to 0 and 1.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector3 LerpClamped(Vector3 a, Vector3 b, float t)
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
        public static void LerpClamped(ref Vector3 a, ref Vector3 b, float t, out Vector3 result)
        {
            t = Mathf.Clamp01(t);
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
            result.z = (t * (b.z - a.z)) + a.z;
        }

        /// <summary>
        /// Move a towards b up to some amount.
        /// </summary>
        /// <param name="a">The vector to move from.</param>
        /// <param name="b">The vector to move towards.</param>
        /// <param name="maxDelta">The maximum distance that a can move.</param>
        public static Vector3 MoveTowards(Vector3 a, Vector3 b, float maxDelta)
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
        public static void MoveTowards(ref Vector3 a, ref Vector3 b, float maxDelta, out Vector3 result)
        {
            Vector3 delta = b - a;
            if (delta.LengthSquared <= maxDelta * maxDelta)
            {
                result = b;
            }
            else
            {
                result = a + delta.Normalized * maxDelta;
            }
        }

        /// <summary>
        /// Creates a vector that contains the cartesian coordinates of a vector specified in barycentric coordinates.
        /// </summary>
        /// <param name="a">The first vector of triangle.</param>
        /// <param name="b">The second vector of triangle.</param>
        /// <param name="c">The third vector of triangle.</param>
        /// <param name="u">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of triangle.</param>
        /// <param name="v">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector3 Barycentric(Vector3 a, Vector3 b, Vector3 c, float u, float v)
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
        public static void Barycentric(ref Vector3 a, ref Vector3 b, ref Vector3 c, float u, float v, out Vector3 result)
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
        public static Vector3 CatmullRom(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            return new Vector3(
                Mathf.CatmullRom(a.x, b.x, c.x, d.x, t),
                Mathf.CatmullRom(a.y, b.y, c.y, d.y, t),
                Mathf.CatmullRom(a.z, b.z, c.z, d.z, t));
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
        public static void CatmullRom(ref Vector3 a, ref Vector3 b, ref Vector3 c, ref Vector3 d, float t, out Vector3 result)
        {
            result.x = Mathf.CatmullRom(a.x, b.x, c.x, d.x, t);
            result.y = Mathf.CatmullRom(a.y, b.y, c.y, d.y, t);
            result.z = Mathf.CatmullRom(a.z, b.z, c.z, d.z, t);
        }

        /// <summary>
        /// Creates a vector that contains hermite spline interpolation.
        /// </summary>
        /// <param name="v1">The first position vector.</param>
        /// <param name="t1">The first tangent vector.</param>
        /// <param name="v2">The second position vector.</param>
        /// <param name="t2">The second tangent vector.</param>
        /// <param name="t">Weighting factor.</param>
        public static Vector3 Hermite(Vector3 v1, Vector3 t1, Vector3 v2, Vector3 t2, float t)
        {
            return new Vector3(
                Mathf.Hermite(v1.x, t1.x, v2.x, t2.x, t),
                Mathf.Hermite(v1.y, t1.y, v2.y, t2.y, t),
                Mathf.Hermite(v1.z, t1.z, v2.z, t2.z, t));
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
        public static void Hermite(ref Vector3 v1, ref Vector3 t1, ref Vector3 v2, ref Vector3 t2, float t, out Vector3 result)
        {
            result.x = Mathf.Hermite(v1.x, t1.x, v2.x, t2.x, t);
            result.y = Mathf.Hermite(v1.y, t1.y, v2.y, t2.y, t);
            result.z = Mathf.Hermite(v1.z, t1.z, v2.z, t2.z, t);
        }

        /// <summary>
        /// Creates a vector that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="t">Weighting value.</param>
        public static Vector3 SmoothStep(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(
                Mathf.SmoothStep(a.x, b.x, t),
                Mathf.SmoothStep(a.y, b.y, t),
                Mathf.SmoothStep(a.z, b.z, t));
        }

        /// <summary>
        /// Creates a vector that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="a">The first vector in interpolation.</param>
        /// <param name="b">The second vector in interpolation.</param>
        /// <param name="t">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector3 a, ref Vector3 b, float t, out Vector3 result)
        {
            result.x = Mathf.SmoothStep(a.x, b.x, t);
            result.y = Mathf.SmoothStep(a.y, b.y, t);
            result.z = Mathf.SmoothStep(a.z, b.z, t);
        }

        /// <summary>
        /// Creates an orthonomal basis from two vectors.
        /// </summary>
        /// <param name="normal">A non-zero vector with the normal.</param>
        /// <param name="tangent">A non-zero vector with the tangent. Will be made perpendicular to the normal.</param>
        /// <param name="binormal">The binormal of the basis as an output parameter.</param>
        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, out Vector3 binormal)
        {
            Normalize(ref normal, out normal);
            Cross(ref normal, ref tangent, out binormal);
            Normalize(ref binormal, out binormal);
            Cross(ref binormal, ref normal, out tangent);
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        public static Vector3 Rotate(Vector3 vector, Quaternion rotation)
        {
            Rotate(ref vector, ref rotation, out vector);
            return vector;
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void Rotate(ref Vector3 vector, ref Quaternion rotation, out Vector3 result)
        {
            float x = 2f * (rotation.y * vector.z - rotation.z * vector.y);
            float y = 2f * (rotation.z * vector.x - rotation.x * vector.z);
            float z = 2f * (rotation.x * vector.y - rotation.y * vector.x);

            result.x = vector.x + x * rotation.w + (rotation.y * z - rotation.z * y);
            result.y = vector.y + y * rotation.w + (rotation.z * x - rotation.x * z);
            result.z = vector.z + z * rotation.w + (rotation.x * y - rotation.y * x);
        }

        /// <summary>
        /// Applies a rotation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void Rotate(Vector3[] srcArray, ref Quaternion rotation, Vector3[] destArray)
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
        public static void Rotate(Vector3[] srcArray, int srcIndex, ref Quaternion rotation, Vector3[] destArray, int destIndex, int length)
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

        /// <summary>
        /// Transform a position.
        /// </summary>
        /// <param name="position">The position to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        public static Vector3 TransformPosition(Vector3 position, Matrix matrix)
        {
            TransformPosition(ref position, ref matrix, out Vector3 result);
            return result;
        }

        /// <summary>
        /// Transform a position.
        /// </summary>
        /// <param name="position">The position to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        public static void TransformPosition(ref Vector3 position, ref Matrix matrix, out Vector3 result)
        {
            result.x = (position.x * matrix.m00) + (position.y * matrix.m10) + (position.z * matrix.m20) + matrix.m30;
            result.y = (position.x * matrix.m01) + (position.y * matrix.m11) + (position.z * matrix.m21) + matrix.m31;
            result.z = (position.x * matrix.m02) + (position.y * matrix.m12) + (position.z * matrix.m22) + matrix.m32;
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the resuls in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void TransformPosition(Vector3[] srcArray, ref Matrix matrix, Vector3[] destArray)
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
                TransformPosition(ref srcArray[i], ref matrix, out destArray[i]);
            }
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void TransformPosition(Vector3[] srcArray, int srcIndex, ref Matrix matrix, Vector3[] destArray, int destIndex, int length)
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
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            }
            if (destArray.Length < destIndex + length)
            {
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");
            }

            for (int i = 0; i < length; i++)
            {
                TransformPosition(ref srcArray[srcIndex + i], ref matrix, out destArray[destIndex + i]);
            }
        }

        /// <summary>
        /// Transforms a direction vector.
        /// </summary>
        /// <param name="direction">The vector to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        public static Vector3 TransformDirection(Vector3 direction, Matrix matrix)
        {
            TransformDirection(ref direction, ref matrix, out Vector3 result);
            return result;
        }

        /// <summary>
        /// Transforms a direction vector.
        /// </summary>
        /// <param name="direction">The vector to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void TransformDirection(ref Vector3 direction, ref Matrix matrix, out Vector3 result)
        {
            result.x = (direction.x * matrix.m00) + (direction.y * matrix.m10) + (direction.z * matrix.m20);
            result.y = (direction.x * matrix.m01) + (direction.y * matrix.m11) + (direction.z * matrix.m21);
            result.z = (direction.x * matrix.m02) + (direction.y * matrix.m12) + (direction.z * matrix.m22);
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void TransformDirection(Vector3[] srcArray, ref Matrix matrix, Vector3[] destArray)
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
                TransformDirection(ref srcArray[i], ref matrix, out destArray[i]);
            }
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void TransformDirection(Vector3[] srcArray, int srcIndex, ref Matrix matrix, Vector3[] destArray, int destIndex, int length)
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
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            }
            if (destArray.Length < destIndex + length)
            {
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");
            }

            for (int i = 0; i < length; i++)
            {
                TransformDirection(ref srcArray[srcIndex + i], ref matrix, out destArray[destIndex + i]);
            }
        }

        /// <summary>
        /// Transforms a normal vector.
        /// </summary>
        /// <remarks>
        /// This calculates the inverse of the given matrix, use <see cref="TransformNormalInverse(Vector3, Matrix)"/>
        /// if you already have the inverse to avoid this extra calculation.
        /// </remarks>
        /// <param name="normal">The normal to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
        {
            TransformNormal(ref normal, ref matrix, out Vector3 result);
            return result;
        }

        /// <summary>
        /// Transforms a normal vector.
        /// </summary>
        /// <remarks>
        /// This calculates the inverse of the given matrix, use <see cref="TransformNormalInverse(ref Vector3, ref Matrix, out Vector3)"/>
        /// if you already have the inverse to avoid this extra calculation.
        /// </remarks>
        /// <param name="normal">The normal to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="result">The transformed normal as an output parameter.</param>
        public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result)
        {
            Matrix inverse = Matrix.Invert(matrix);
            TransformNormalInverse(ref normal, ref inverse, out result);
        }

        /// <summary>
        /// Applies a transformation to all normal vectors within array and places the results in an another array.
        /// </summary>
        /// <remarks>
        /// This calculates the inverse of the given matrix, use <see cref="TransformNormalInverse(Vector3[], ref Matrix, Vector3[])"/>
        /// if you already have the inverse to avoid this extra calculation.
        /// </remarks>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void TransformNormal(Vector3[] srcArray, ref Matrix matrix, Vector3[] destArray)
        {
            Matrix inverse = Matrix.Invert(matrix);
            TransformNormalInverse(srcArray, ref inverse, destArray);
        }

        /// <summary>
        /// Applies a transformation to all normal vectors within array and places the results in an another array.
        /// </summary>
        /// <remarks>
        /// This calculates the inverse of the given matrix, use <see cref="TransformNormalInverse(Vector3[], int, ref Matrix, Vector3[], int, int)"/>
        /// if you already have the inverse to avoid this extra calculation.
        /// </remarks>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void TransformNormal(Vector3[] srcArray, int srcIndex, ref Matrix matrix, Vector3[] destArray, int destIndex, int length)
        {
            Matrix inverse = Matrix.Invert(matrix);
            TransformNormalInverse(srcArray, srcIndex, ref inverse, destArray, destIndex, length);
        }

        /// <summary>
        /// Transform a normal by the transpose of a matrix.
        /// </summary>
        /// <remarks>
        /// This version doesn't calculate the inverse matrix.
        /// Use this version if you already have the inverse of the desired transform available.
        /// </remarks>
        /// <param name="normal">The normal to transform.</param>
        /// <param name="invMat">The inverse of the desired transformation.</param>
        /// <param name="result">The transformed normal as an output parameter.</param>
        public static Vector3 TransformNormalInverse(Vector3 normal, Matrix invMat)
        {
            TransformNormalInverse(ref normal, ref invMat, out Vector3 result);
            return result;
        }

        /// <summary>
        /// Transform a normal by the transpose of a matrix.
        /// </summary>
        /// <remarks>
        /// This version doesn't calculate the inverse matrix.
        /// Use this version if you already have the inverse of the desired transform available.
        /// </remarks>
        /// <param name="normal">The normal to transform.</param>
        /// <param name="invMat">The inverse of the desired transformation.</param>
        /// <param name="result">The transformed normal as an output parameter.</param>
        public static void TransformNormalInverse(ref Vector3 normal, ref Matrix invMat, out Vector3 result)
        {
            result.x = (normal.x * invMat.m00) + (normal.y * invMat.m01) + (normal.z * invMat.m02);
            result.y = (normal.x * invMat.m10) + (normal.y * invMat.m11) + (normal.z * invMat.m12);
            result.z = (normal.x * invMat.m20) + (normal.y * invMat.m21) + (normal.z * invMat.m22);
        }

        /// <summary>
        /// Applies a transformation to all normal vectors within array and places the results in an another array.
        /// </summary>
        /// <remarks>
        /// This version doesn't calculate the inverse matrix.
        /// Use this version if you already have the inverse of the desired transform available.
        /// </remarks>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void TransformNormalInverse(Vector3[] srcArray, ref Matrix matrix, Vector3[] destArray)
        {
            if (srcArray == null)
            {
                throw new ArgumentNullException("srcArray");
            }
            if (destArray == null)
            {
                throw new ArgumentNullException("dstArray");
            }
            if (destArray.Length < srcArray.Length)
            {
                throw new ArgumentException("Destination array is smaller than source array.");
            }

            for (int i = 0; i < srcArray.Length; i++)
            {
                TransformNormalInverse(ref srcArray[i], ref matrix, out destArray[i]);
            }
        }

        /// <summary>
        /// Applies a transformation to all normal vectors within array and places the results in an another array.
        /// </summary>
        /// <remarks>
        /// This version doesn't calculate the inverse matrix.
        /// Use this version if you already have the inverse of the desired transform available.
        /// </remarks>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void TransformNormalInverse(Vector3[] srcArray, int srcIndex, ref Matrix matrix, Vector3[] destArray, int destIndex, int length)
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
                TransformNormalInverse(ref srcArray[srcIndex + i], ref matrix, out destArray[destIndex + i]);
            }
        }

        /// <summary>
        /// Transform a vector accounting for perspective.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        public static Vector3 TransformPerspective(Vector3 vector, Matrix matrix)
        {
            TransformPerspective(ref vector, ref matrix, out vector);
            return vector;
        }

        /// <summary>
        /// Transform a vector accounting for perspective.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="result">The transformed vector as an output parameter.</param>
        public static void TransformPerspective(ref Vector3 vector, ref Matrix mat, out Vector3 result)
        {
            Vector4 v = new Vector4(vector.x, vector.y, vector.z, 1f);
            Vector4.Transform(ref v, ref mat, out Vector4 p);

            float scale = 1f / p.w;
            result.x = p.x * scale;
            result.y = p.y * scale;
            result.z = p.z * scale;
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        public static void TransformPerspective(Vector3[] srcArray, ref Matrix matrix, Vector3[] destArray)
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
                TransformPerspective(ref srcArray[i], ref matrix, out destArray[i]);
            }
        }

        /// <summary>
        /// Applies a transformation to all vectors within array and places the results in an another array.
        /// </summary>
        /// <param name="srcArray">The vectors to transform.</param>
        /// <param name="srcIndex">The starting index in the source array.</param>
        /// <param name="matrix">The transformation to apply.</param>
        /// <param name="destArray">The array transformed vectors are output to.</param>
        /// <param name="destIndex">The starting index in the destination array.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void TransformPerspective(Vector3[] srcArray, int srcIndex, ref Matrix matrix, Vector3[] destArray, int destIndex, int length)
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
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            }
            if (destArray.Length < destIndex + length)
            {
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");
            }

            for (int i = 0; i < length; i++)
            {
                TransformPerspective(ref srcArray[srcIndex + i], ref matrix, out destArray[destIndex + i]);
            }
        }

        /// <summary>
        /// Projects a vector from world space into screen space.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="x">The X coordinate of the viewport.</param>
        /// <param name="y">The Y coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport.</param>
        /// <param name="height">The height of the viewport.</param>
        /// <param name="minZ">The minimum depth of the viewport.</param>
        /// <param name="maxZ">The maximum depth of the viewport.</param>
        /// <param name="viewProj">The view-projection matrix.</param>
        /// <returns>The vector in screen space.</returns>
        /// <remarks>
        /// To project to normalized device coordinates (NDC) use the following parameters:
        /// Project(vector, -1, -1, 2, 2, -1, 1, viewProjection).
        /// </remarks>
        public static Vector3 Project(Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix viewProj)
        {
            TransformPerspective(ref vector, ref viewProj, out Vector3 result);

            result.x = x + (width * ((result.x + 1f) / 2f));
            result.y = y + (height * ((result.y + 1f) / 2f));
            result.z = minZ + ((maxZ - minZ) * ((result.z + 1f) / 2f));

            return result;
        }

        /// <summary>
        /// Projects a vector from screen space into world space.
        /// </summary>
        /// <param name="vector">The vector to project.</param>
        /// <param name="x">The X coordinate of the viewport.</param>
        /// <param name="y">The Y coordinate of the viewport.</param>
        /// <param name="width">The width of the viewport.</param>
        /// <param name="height">The height of the viewport.</param>
        /// <param name="minZ">The minimum depth of the viewport.</param>
        /// <param name="maxZ">The maximum depth of the viewport.</param>
        /// <param name="invViewProj">The inverse of the view-projection matrix.</param>
        /// <returns>The vector in world space.</returns>
        /// <remarks>
        /// To project from normalized device coordinates (NDC) use the following parameters:
        /// Project(vector, -1, -1, 2, 2, -1, 1, inverseViewProjection).
        /// </remarks>
        public static Vector3 Unproject(Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix invViewProj)
        {
            vector.x = ((vector.x - x) / width * 2f) - 1f;
            vector.y = ((vector.y - y) / height * 2f) - 1f;
            vector.z = (vector.z / (maxZ - minZ) * 2f) - 1f;

            TransformPerspective(ref vector, ref invViewProj, out Vector3 result);
            return result;
        }
        */
        /// <summary>
        /// Compares whether current instance is equal to a specified vector.
        /// </summary>
        /// <param name="other">The vector to compare.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public bool Equals(Vector3 other)
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
            return (obj is Vector3) && Equals((Vector3)obj);
        }
        /*
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
            const string format = "F2";
            return $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)})";
        }

        /// <summary>
        /// Adds the specified vectors.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
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
        public static Vector3 operator -(Vector3 left, Vector3 right)
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
        public static Vector3 operator -(Vector3 vector)
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
        public static Vector3 operator *(Vector3 vector, float scale)
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
        public static Vector3 operator *(float scale, Vector3 vector)
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
        public static Vector3 operator *(Vector3 vector, Vector3 scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            vector.z *= scale.z;
            return vector;
        }

        /// <summary>
        /// Rotates a vector.
        /// </summary>
        /// <param name="rotation">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        /// <returns>Result of the vector rotation.</returns>
        public static Vector3 operator *(Quaternion rotation, Vector3 vector)
        {
            return Rotate(vector, rotation);
        }

        /// <summary>
        /// Divides the components of a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="divider">Right operand.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector3 operator /(Vector3 vector, float divider)
        {
            float factor = 1f / divider;
            vector.x *= factor;
            vector.y *= factor;
            vector.z *= factor;
            return vector;
        }

        /// <summary>
        /// Component-wise division of one vector by another.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector3 operator /(Vector3 vector, Vector3 scale)
        {
            vector.x /= scale.x;
            vector.y /= scale.y;
            vector.z /= scale.z;
            return vector;
        }

        /// <summary>
        /// Compares whether two vectors are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
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
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z;
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector2(Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        /// <summary>
        /// Cast the vector as a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The vector to cast.</param>
        public static implicit operator Vector4(Vector3 vector)
        {
            return new Vector4(vector.x, vector.y, vector.z, 0f);
        }
        */
    }
}
