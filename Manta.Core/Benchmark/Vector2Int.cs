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
    public partial struct Vector2Int : IEquatable<Vector2Int>
    {
        [MethodImpl(METHOD_OPTIONS)]
        public int GetValue(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                default:
                    throw new IndexOutOfRangeException($"Index out of range: {index}");
            }
        }

        public float LengthSlow => Mathf.Sqrt((x * x) + (y * y));

        public int LengthSquaredSlow => (x * x) + (y * y);

        public int LengthSquaredOptimized => MagnitudeSquaredOptimized(this);

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe int MagnitudeSquaredOptimized(Vector2Int vector)
        {
            Vector128<int> v0 = VectorIntrinsics.Load(vector);
            Vector128<int> v1 = Sse41.MultiplyLow(v0, v0);
            Vector128<int> v2 = Ssse3.HorizontalAdd(v1, v1);
            return v2.ToScalar();
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ComponentMinSlow(Vector2Int a, Vector2Int b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMinSlow(ref Vector2Int a, ref Vector2Int b, out Vector2Int result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ComponentMaxSlow(Vector2Int a, Vector2Int b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMaxSlow(ref Vector2Int a, ref Vector2Int b, out Vector2Int result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ComponentClampSlow(Vector2Int vector, Vector2Int min, Vector2Int max)
        {
            vector.x = Mathf.Max(Mathf.Min(vector.x, max.x), min.x);
            vector.y = Mathf.Max(Mathf.Min(vector.y, max.y), min.y);
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentClampSlow(ref Vector2Int vector, ref Vector2Int min, ref Vector2Int max, out Vector2Int result)
        {
            result.x = Mathf.Max(Mathf.Min(vector.x, max.x), min.x);
            result.y = Mathf.Max(Mathf.Min(vector.y, max.y), min.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int CeilSlow(Vector2 vector)
        {
            return new Vector2Int(
                Mathf.CeilToInt(vector.x),
                Mathf.CeilToInt(vector.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void CeilSlow(ref Vector2 vector, out Vector2Int result)
        {
            result.x = Mathf.CeilToInt(vector.x);
            result.y = Mathf.CeilToInt(vector.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int FloorSlow(Vector2 vector)
        {
            return new Vector2Int(
                Mathf.FloorToInt(vector.x),
                Mathf.FloorToInt(vector.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void FloorSlow(ref Vector2 vector, out Vector2Int result)
        {
            result.x = Mathf.FloorToInt(vector.x);
            result.y = Mathf.FloorToInt(vector.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int RoundSlow(Vector2 vector)
        {
            return new Vector2Int(
                Mathf.RoundToInt(vector.x),
                Mathf.RoundToInt(vector.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void RoundSlow(ref Vector2 vector, out Vector2Int result)
        {
            result.x = Mathf.RoundToInt(vector.x);
            result.y = Mathf.RoundToInt(vector.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int AddSlow(Vector2Int left, Vector2Int right)
        {
            AddSlow(ref left, ref right, out left);
            return left;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void AddSlow(ref Vector2Int left, ref Vector2Int right, out Vector2Int result)
        {
            result.x = left.x + right.x;
            result.y = left.y + right.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int SubtractSlow(Vector2Int left, Vector2Int right)
        {
            SubtractSlow(ref left, ref right, out left);
            return left;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void SubtractSlow(ref Vector2Int left, ref Vector2Int right, out Vector2Int result)
        {
            result.x = left.x - right.x;
            result.y = left.y - right.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int NegateSlow(Vector2Int vector)
        {
            NegateSlow(ref vector, out vector);
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void NegateSlow(ref Vector2Int vector, out Vector2Int result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int ScaleSlow(Vector2Int vector, int scale)
        {
            ScaleSlow(ref vector, scale, out vector);
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ScaleSlow(ref Vector2Int vector, int scale, out Vector2Int result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2Int MultiplySlow(Vector2Int left, Vector2Int right)
        {
            MultiplySlow(ref left, ref right, out left);
            return left;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void MultiplySlow(ref Vector2Int left, ref Vector2Int right, out Vector2Int result)
        {
            result.x = left.x * right.x;
            result.y = left.y * right.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSlow(Vector2Int a, Vector2Int b)
        {
            a.x -= b.x;
            a.y -= b.y;
            return Mathf.Sqrt((a.x * a.x) + (a.y * a.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float DistanceSlow(ref Vector2Int a, ref Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return Mathf.Sqrt((dx * dx) + (dy * dy));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquaredSlow(Vector2Int a, Vector2Int b)
        {
            a.x -= b.x;
            a.y -= b.y;
            return (a.x * a.x) + (a.y * a.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static int DistanceSquaredSlow(ref Vector2Int a, ref Vector2Int b)
        {
            int dx = a.x - b.x;
            int dy = a.y - b.y;
            return (dx * dx) + (dy * dy);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public bool EqualsSlow(Vector2Int other)
        {
            return
                x == other.x &&
                y == other.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 Vector2CastSlow(Vector2Int vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}
#endif
