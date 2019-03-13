/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

#if BENCHMARK
using System;
using System.Runtime.CompilerServices;

namespace Manta
{
    public partial struct Vector4 : IEquatable<Vector4>
    {
        [MethodImpl(METHOD_OPTIONS)]
        public float GetValue(int index)
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

        public float LengthSquaredSlow => (x * x) + (y * y) + (z * z) + (w * w);

        public static Vector4 NormalizeSlow(Vector4 vector)
        {
            Normalize(ref vector, out Vector4 result);
            return result;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void NormalizeSlow(ref Vector4 vector, out Vector4 result)
        {
            float scale = 1f / vector.Length;
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        public static Vector4 ComponentMinSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x < b.x ? a.x : b.x;
            a.y = a.y < b.y ? a.y : b.y;
            a.z = a.z < b.z ? a.z : b.z;
            a.w = a.w < b.w ? a.w : b.w;
            return a;
        }

        public static void ComponentMinSlow(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x < b.x ? a.x : b.x;
            result.y = a.y < b.y ? a.y : b.y;
            result.z = a.z < b.z ? a.z : b.z;
            result.w = a.w < b.w ? a.w : b.w;
        }

        public static Vector4 ComponentMaxSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x > b.x ? a.x : b.x;
            a.y = a.y > b.y ? a.y : b.y;
            a.z = a.z > b.z ? a.z : b.z;
            a.w = a.w > b.w ? a.w : b.w;
            return a;
        }

        public static void ComponentMaxSlow(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x > b.x ? a.x : b.x;
            result.y = a.y > b.y ? a.y : b.y;
            result.z = a.z > b.z ? a.z : b.z;
            result.w = a.w > b.w ? a.w : b.w;
        }

        public static Vector4 ComponentClampSlow(Vector4 vector, Vector4 min, Vector4 max)
        {
            return new Vector4(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z),
                Mathf.Clamp(vector.w, min.w, max.w));
        }

        public static void ComponentClampSlow(ref Vector4 vector, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            result.x = Mathf.Clamp(vector.x, min.x, max.x);
            result.y = Mathf.Clamp(vector.y, min.y, max.y);
            result.z = Mathf.Clamp(vector.z, min.z, max.z);
            result.w = Mathf.Clamp(vector.w, min.w, max.w);
        }

        public static Vector4 AddSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x + b.x;
            a.y = a.y + b.y;
            a.z = a.z + b.z;
            a.w = a.w + b.w;
            return a;
        }

        public static void AddSlow(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            result.z = a.z + b.z;
            result.w = a.w + b.w;
        }

        public static Vector4 SubtractSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x - b.x;
            a.y = a.y - b.y;
            a.z = a.z - b.z;
            a.w = a.w - b.w;
            return a;
        }

        public static void SubtractSlow(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            result.z = a.z - b.z;
            result.w = a.w - b.w;
        }

        public static Vector4 NegateSlow(Vector4 vector)
        {
            vector.x = -vector.x;
            vector.y = -vector.y;
            vector.z = -vector.z;
            vector.w = -vector.w;
            return vector;
        }

        public static void NegateSlow(ref Vector4 vector, out Vector4 result)
        {
            result.x = -vector.x;
            result.y = -vector.y;
            result.z = -vector.z;
            result.w = -vector.w;
        }

        public static Vector4 MultiplySlow(Vector4 vector, float scale)
        {
            vector.x = vector.x * scale;
            vector.y = vector.y * scale;
            vector.z = vector.z * scale;
            vector.w = vector.w * scale;
            return vector;
        }

        public static void MultiplySlow(ref Vector4 vector, float scale, out Vector4 result)
        {
            result.x = vector.x * scale;
            result.y = vector.y * scale;
            result.z = vector.z * scale;
            result.w = vector.w * scale;
        }

        public static Vector4 MultiplySlow(Vector4 a, Vector4 b)
        {
            a.x = a.x * b.x;
            a.y = a.y * b.y;
            a.z = a.z * b.z;
            a.w = a.w * b.w;
            return a;
        }

        public static void MultiplySlow(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x * b.x;
            result.y = a.y * b.y;
            result.z = a.z * b.z;
            result.w = a.w * b.w;
        }

        public static Vector4 DivideSlow(Vector4 vector, float scale)
        {
            vector.x = vector.x / scale;
            vector.y = vector.y / scale;
            vector.z = vector.z / scale;
            vector.w = vector.w / scale;
            return vector;
        }

        public static void DivideSlow(ref Vector4 vector, float scale, out Vector4 result)
        {
            result.x = vector.x / scale;
            result.y = vector.y / scale;
            result.z = vector.z / scale;
            result.w = vector.w / scale;
        }

        public static Vector4 DivideSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x / b.x;
            a.y = a.y / b.y;
            a.z = a.z / b.z;
            a.w = a.w / b.w;
            return a;
        }

        public static void DivideSlow(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result.x = a.x / b.x;
            result.y = a.y / b.y;
            result.z = a.z / b.z;
            result.w = a.w / b.w;
        }

        public static float DotSlow(Vector4 a, Vector4 b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        public static float DotSlow(ref Vector4 a, ref Vector4 b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
        }

        public static Vector4 ProjectSlow(Vector4 vector, Vector4 normal)
        {
            float sqrLen = vector.LengthSquared;
            if (sqrLen < float.Epsilon)
            {
                return Zero;
            }
            else
            {
                return Divide(Multiply(normal, Dot(vector, normal)), sqrLen);
            }
        }

        public static void ProjectSlow(ref Vector4 vector, ref Vector4 normal, out Vector4 result)
        {
            float sqrLen = MagnitudeSquared(ref vector);
            if (sqrLen < float.Epsilon)
            {
                result = Zero;
            }
            else
            {
                Multiply(ref normal, Dot(ref vector, ref normal), out Vector4 projNorm);
                result = Divide(projNorm, sqrLen);
            }
        }

        public static float DistanceSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x - b.x;
            a.y = a.y - b.y;
            a.z = a.z - b.z;
            a.w = a.w - b.w;
            return Mathf.Sqrt((a.x * a.x) + (a.y * a.y) + (a.z * a.z) + (a.w * a.w));
        }

        public static float DistanceSlow(ref Vector4 a, ref Vector4 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            float dw = a.w - b.w;
            return Mathf.Sqrt((dx * dx) + (dy * dy) + (dz * dz) + (dw * dw));
        }

        public static float DistanceSquaredSlow(Vector4 a, Vector4 b)
        {
            a.x = a.x - b.x;
            a.y = a.y - b.y;
            a.z = a.z - b.z;
            a.w = a.w - b.w;
            return (a.x * a.x) + (a.y * a.y) + (a.z * a.z) + (a.w * a.w);
        }

        public static float DistanceSquaredSlow(ref Vector4 a, ref Vector4 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            float dw = a.w - b.w;
            return (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        public static Vector4 LerpSlow(Vector4 a, Vector4 b, float t)
        {
            a.x = (t * (b.x - a.x)) + a.x;
            a.y = (t * (b.y - a.y)) + a.y;
            a.z = (t * (b.z - a.z)) + a.z;
            a.w = (t * (b.w - a.w)) + a.w;
            return a;
        }

        public static void LerpSlow(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
            result.z = (t * (b.z - a.z)) + a.z;
            result.w = (t * (b.w - a.w)) + a.w;
        }

        public static Vector4 LerpClampedSlow(Vector4 a, Vector4 b, float t)
        {
            t = Mathf.Clamp01(t);
            a.x = (t * (b.x - a.x)) + a.x;
            a.y = (t * (b.y - a.y)) + a.y;
            a.z = (t * (b.z - a.z)) + a.z;
            a.w = (t * (b.w - a.w)) + a.w;
            return a;
        }

        public static void LerpClampedSlow(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            t = Mathf.Clamp01(t);
            result.x = (t * (b.x - a.x)) + a.x;
            result.y = (t * (b.y - a.y)) + a.y;
            result.z = (t * (b.z - a.z)) + a.z;
            result.w = (t * (b.w - a.w)) + a.w;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static Vector4 MoveTowardsSlow(Vector4 a, Vector4 b, float maxDelta)
        {
            Vector4 delta = b - a;
            if (delta.LengthSquared <= maxDelta * maxDelta)
            {
                return b;
            }
            else
            {
                return a + delta.Normalized * maxDelta;
            }
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static void MoveTowardsSlow(ref Vector4 a, ref Vector4 b, float maxDelta, out Vector4 result)
        {
            Vector4 delta = b - a;
            if (delta.LengthSquared <= maxDelta * maxDelta)
            {
                result = b;
            }
            else
            {
                result = a + delta.Normalized * maxDelta;
            }
        }
    }
}
#endif
