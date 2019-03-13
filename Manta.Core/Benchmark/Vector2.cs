/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

#if BENCHMARK
using System;
using System.Runtime.CompilerServices;

namespace Manta
{
    public partial struct Vector2 : IEquatable<Vector2>
    {
        [MethodImpl(METHOD_OPTIONS)]
        public float GetValue(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                default:
                    throw new IndexOutOfRangeException($"Index out of range: {index}");
            }
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 ComponentMinSlow(Vector2 a, Vector2 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMinSlow(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 ComponentMaxSlow(Vector2 a, Vector2 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentMaxSlow(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 ComponentClampSlow(Vector2 vector, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void ComponentClampSlow(ref Vector2 vector, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            result.x = Mathf.Clamp(vector.x, min.x, max.x);
            result.y = Mathf.Clamp(vector.y, min.y, max.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSlow(Vector2 vector)
        {
            return Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSlow(ref Vector2 vector)
        {
            return Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y));
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSquaredSlow(Vector2 vector)
        {
            return (vector.x * vector.x) + (vector.y * vector.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float MagnitudeSquaredSlow(ref Vector2 vector)
        {
            return (vector.x * vector.x) + (vector.y * vector.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 NormalizeSlow(Vector2 vector)
        {
            float scale = 1f / vector.Length;
            vector.x *= scale;
            vector.y *= scale;
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void NormalizeSlow(ref Vector2 vector, out Vector2 result)
        {
            float scale = 1f / vector.Length;
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 AddSlow(Vector2 a, Vector2 b)
        {
            a.x += b.x;
            a.y += b.y;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void AddSlow(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 SubtractSlow(Vector2 a, Vector2 b)
        {
            a.x -= b.x;
            a.y -= b.y;
            return a;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void SubtractSlow(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 NegateSlow(Vector2 vector)
        {
            vector.x = -vector.x;
            vector.y = -vector.y;
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void NegateSlow(ref Vector2 vector, out Vector2 result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 MultiplySlow(Vector2 vector, float scale)
        {
            vector.x *= scale;
            vector.y *= scale;
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void MultiplySlow(ref Vector2 vector, float scale, out Vector2 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 MultiplySlow(Vector2 vector, Vector2 scale)
        {
            vector.x *= scale.x;
            vector.y *= scale.y;
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void MultiplySlow(ref Vector2 vector, ref Vector2 scale, out Vector2 result)
        {
            result.x = vector.x * scale.x;
            result.y = vector.y * scale.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 DivideSlow(Vector2 vector, float scale)
        {
            vector.x /= scale;
            vector.y /= scale;
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void DivideSlow(ref Vector2 vector, float scale, out Vector2 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector2 DivideSlow(Vector2 vector, Vector2 scale)
        {
            vector.x /= scale.x;
            vector.y /= scale.y;
            return vector;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void DivideSlow(ref Vector2 vector, ref Vector2 scale, out Vector2 result)
        {
            result.x = vector.x / scale.x;
            result.y = vector.y / scale.y;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float DotSlow(Vector2 a, Vector2 b)
        {
            return (a.x * b.x) + (a.y * b.y);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static float DotSlow(ref Vector2 a, ref Vector2 b)
        {
            return (a.x * b.x) + (a.y * b.y);
        }
    }
}
#endif
