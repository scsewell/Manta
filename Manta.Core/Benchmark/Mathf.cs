/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

#if BENCHMARK
using System;
using System.Runtime.CompilerServices;

namespace Manta
{
    public static partial class Mathf
    {
        [MethodImpl(METHOD_OPTIONS)]
        public static float MinSlow(float x, float y) => MathF.Min(x, y);

        [MethodImpl(METHOD_OPTIONS)]
        public static double MinSlow(double x, double y) => Math.Min(x, y);

        [MethodImpl(METHOD_OPTIONS)]
        public static float MaxSlow(float x, float y) => MathF.Max(x, y);

        [MethodImpl(METHOD_OPTIONS)]
        public static double MaxSlow(double x, double y) => Math.Max(x, y);

        [MethodImpl(METHOD_OPTIONS)]
        public static float ClampSlow(float x, float min, float max) => MathF.Max(MathF.Min(x, max), min);

        [MethodImpl(METHOD_OPTIONS)]
        public static double ClampSlow(double x, double min, double max) => Math.Max(Math.Min(x, max), min);
        
        [MethodImpl(METHOD_OPTIONS)]
        public static float InvSqrtSlow(float x) => 1f / MathF.Sqrt(x);

        [MethodImpl(METHOD_OPTIONS)]
        public static int NextPowerOfTwoSlow(int x) => (int)MathF.Pow(2f, MathF.Ceiling(MathF.Log(x, 2f)));

        [MethodImpl(METHOD_OPTIONS)]
        public static long NextPowerOfTwoSlow(long x) => (long)MathF.Pow(2f, MathF.Ceiling(MathF.Log(x, 2f)));

        [MethodImpl(METHOD_OPTIONS)]
        public static float LerpSlow(float a, float b, float t) => a + ((b - a) * t);
    }
}
#endif