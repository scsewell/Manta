/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

#if BENCHMARK
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Manta
{
    public partial struct Vector4Int : IEquatable<Vector4Int>
    {
        [MethodImpl(METHOD_OPTIONS)]
        public int GetValue(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return z;
                case 3: return w;
                default:
                    throw new IndexOutOfRangeException($"Index out of range: {index}");
            }
        }

        public float LengthSlow => Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));

        public int LengthSquaredSlow => (x * x) + (y * y) + (z * z) + (w * w);

        public int LengthSquaredOptimized => MagnitudeSquaredOptimized(ref this);

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe int MagnitudeSquaredOptimized(ref Vector4Int vector)
        {
            Vector128<int> v0 = Sse2.LoadVector128((int*)Unsafe.AsPointer(ref vector));
            Vector128<int> v1 = Sse41.MultiplyLow(v0, v0);
            Vector128<int> v2 = Ssse3.HorizontalAdd(v1, v1);
            Vector128<int> v3 = Ssse3.HorizontalAdd(v2, v2);
            return v3.ToScalar();
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ComponentMinSlow(Vector4Int a, Vector4Int b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMinSlow(ref Vector4Int a, ref Vector4Int b, out Vector4Int result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
            result.w = a.w < b.w ? a.w : b.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ComponentMaxSlow(Vector4Int a, Vector4Int b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMaxSlow(ref Vector4Int a, ref Vector4Int b, out Vector4Int result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
            result.w = a.w > b.w ? a.w : b.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ComponentClampSlow(Vector4Int vector, Vector4Int min, Vector4Int max)
        {
            vector.x = Mathf.Max(Mathf.Min(vector.x, max.x), min.x);
            vector.y = Mathf.Max(Mathf.Min(vector.y, max.y), min.y);
            vector.z = Mathf.Max(Mathf.Min(vector.z, max.z), min.z);
            vector.w = Mathf.Max(Mathf.Min(vector.w, max.w), min.w);
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentClampSlow(ref Vector4Int vector, ref Vector4Int min, ref Vector4Int max, out Vector4Int result)
        {
            result.x = Mathf.Max(Mathf.Min(vector.x, max.x), min.x);
            result.y = Mathf.Max(Mathf.Min(vector.y, max.y), min.y);
            result.z = Mathf.Max(Mathf.Min(vector.z, max.z), min.z);
            result.w = Mathf.Max(Mathf.Min(vector.w, max.w), min.w);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int CeilSlow(Vector4 vector)
        {
            return new Vector4Int(
                Mathf.CeilToInt(vector.x),
                Mathf.CeilToInt(vector.y),
                Mathf.CeilToInt(vector.z),
                Mathf.CeilToInt(vector.w));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void CeilSlow(ref Vector4 vector, out Vector4Int result)
        {
            result.x = Mathf.CeilToInt(vector.x);
            result.y = Mathf.CeilToInt(vector.y);
            result.z = Mathf.CeilToInt(vector.z);
            result.w = Mathf.CeilToInt(vector.w);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int FloorSlow(Vector4 vector)
        {
            return new Vector4Int(
                Mathf.FloorToInt(vector.x),
                Mathf.FloorToInt(vector.y),
                Mathf.FloorToInt(vector.z),
                Mathf.FloorToInt(vector.w));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void FloorSlow(ref Vector4 vector, out Vector4Int result)
        {
            result.x = Mathf.FloorToInt(vector.x);
            result.y = Mathf.FloorToInt(vector.y);
            result.z = Mathf.FloorToInt(vector.z);
            result.w = Mathf.FloorToInt(vector.w);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int RoundSlow(Vector4 vector)
        {
            return new Vector4Int(
                Mathf.RoundToInt(vector.x),
                Mathf.RoundToInt(vector.y),
                Mathf.RoundToInt(vector.z),
                Mathf.RoundToInt(vector.w));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void RoundSlow(ref Vector4 vector, out Vector4Int result)
        {
            result.x = Mathf.RoundToInt(vector.x);
            result.y = Mathf.RoundToInt(vector.y);
            result.z = Mathf.RoundToInt(vector.z);
            result.w = Mathf.RoundToInt(vector.w);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int AddSlow(Vector4Int left, Vector4Int right)
        {
            AddSlow(ref left, ref right, out left);
            return left;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void AddSlow(ref Vector4Int left, ref Vector4Int right, out Vector4Int result)
        {
            result.x = left.x + right.x;
            result.y = left.y + right.y;
            result.z = left.z + right.z;
            result.w = left.w + right.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int SubtractSlow(Vector4Int left, Vector4Int right)
        {
            SubtractSlow(ref left, ref right, out left);
            return left;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void SubtractSlow(ref Vector4Int left, ref Vector4Int right, out Vector4Int result)
        {
            result.x = left.x - right.x;
            result.y = left.y - right.y;
            result.z = left.z - right.z;
            result.w = left.w - right.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int NegateSlow(Vector4Int vector)
        {
            NegateSlow(ref vector, out vector);
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void NegateSlow(ref Vector4Int vector, out Vector4Int result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
            result.z = -vector.z;
            result.w = -vector.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int ScaleSlow(Vector4Int vector, int scale)
        {
            ScaleSlow(ref vector, scale, out vector);
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ScaleSlow(ref Vector4Int vector, int scale, out Vector4Int result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4Int MultiplySlow(Vector4Int left, Vector4Int right)
        {
            MultiplySlow(ref left, ref right, out left);
            return left;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void MultiplySlow(ref Vector4Int left, ref Vector4Int right, out Vector4Int result)
        {
            result.x = left.x * right.x;
            result.y = left.y * right.y;
            result.z = left.z * right.z;
            result.w = left.w * right.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSlow(Vector4Int a, Vector4Int b)
        {
            a.x -= b.x;
            a.y -= b.y;
            a.z -= b.z;
            a.w -= b.w;
            return Mathf.Sqrt((a.x * a.x) + (a.y * a.y) + (a.z * a.z) + (a.w * a.w));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSlow(ref Vector4Int a, ref Vector4Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            int dz = a.z - b.z;
            int dw = a.w - b.w;
            return Mathf.Sqrt((dx * dx) + (dy * dy) + (dz * dz) + (dw * dw));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquaredSlow(Vector4Int a, Vector4Int b)
        {
            a.x -= b.x;
            a.y -= b.y;
            a.z -= b.z;
            a.w -= b.w;
            return (a.x * a.x) + (a.y * a.y) + (a.z * a.z) + (a.w * a.w);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquaredSlow(ref Vector4Int a, ref Vector4Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            int dz = a.z - b.z;
            int dw = a.w - b.w;
            return (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public bool EqualsSlow(Vector4Int other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 Vector4CastSlow(Vector4Int vector)
        {
            return new Vector4(vector.x, vector.y, vector.z, vector.w);
        }
    }
}
#endif
