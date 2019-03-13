using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Manta
{
    /// <summary>
    /// Helper functions for vectorized math.
    /// </summary>
    internal static class VectorIntrinsics
    {
        private const MethodImplOptions METHOD_OPTIONS = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<int> Load(Vector2Int vector)
        {
            return Sse2.LoadScalarVector128((long*)&vector).AsInt32();
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<int> Load(ref Vector2Int vector)
        {
            return Sse2.LoadScalarVector128((long*)Unsafe.AsPointer(ref vector)).AsInt32();
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<int> Load(Vector4Int vector)
        {
            return Sse2.LoadVector128((int*)&vector);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<int> Load(ref Vector4Int vector)
        {
            return Sse2.LoadVector128((int*)Unsafe.AsPointer(ref vector));
        }


        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<float> Load(Vector2 vector)
        {
            return Sse2.LoadScalarVector128((double*)&vector).AsSingle();
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<float> Load(ref Vector2 vector)
        {
            return Sse2.LoadScalarVector128((double*)Unsafe.AsPointer(ref vector)).AsSingle();
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<float> Load(Vector4 vector)
        {
            return Sse.LoadVector128((float*)&vector);
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector128<float> Load(ref Vector4 vector)
        {
            return Sse.LoadVector128((float*)Unsafe.AsPointer(ref vector));
        }

        
        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector2Int Store2(Vector128<int> vector)
        {
            double result;
            Sse2.StoreScalar(&result, vector.AsDouble());
            return *(Vector2Int*)&result;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector4Int Store4(Vector128<int> vector)
        {
            Vector4Int result;
            Sse2.Store((int*)&result, vector);
            return result;
        }


        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector2 Store2(Vector128<float> vector)
        {
            double result;
            Sse2.StoreScalar(&result, vector.AsDouble());
            return *(Vector2*)&result;
        }

        [MethodImpl(METHOD_OPTIONS)]
        public static unsafe Vector4 Store4(Vector128<float> vector)
        {
            Vector4 result;
            Sse.Store((float*)&result, vector);
            return result;
        }
    }
}
