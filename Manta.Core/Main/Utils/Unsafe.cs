/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Engine
{
    /// <summary>
    /// Utility methods useful for making high-performance code and helping with calls to unmanaged code.
    /// </summary>
    internal static class Unsafe
    {
        /// <summary>
        /// Casts one type to another by changing the reference type, leaving the data unmodified. 
        /// This means any changes to the values made to the reinterpereted instance will affect
        /// the values of the original type.
        /// </summary>
        /// <typeparam name="TSrc">The source type. Must be blittable and have the same size as the destination type.</typeparam>
        /// <typeparam name="TDest">The destination type. Must be blittable and have the same size as the source type</typeparam>
        /// <param name="source">The value to cast.</param>
        /// <returns>A reference with the new type pointing to the original value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe TDest ReinterpretCast<TSrc, TDest>(TSrc source)
            where TSrc : unmanaged
            where TDest : unmanaged
        {
            // check the types are the same size
            Debug.Assert(SizeOf<TSrc>() == SizeOf<TDest>(), $"Can't reinterperet cast, \"{typeof(TSrc).FullName}\" has size {SizeOf<TSrc>()} but \"{typeof(TDest).FullName}\" has size {SizeOf<TDest>()}!");

            TDest dest = default;
            TypedReference sourceRef = __makeref(source);
            TypedReference destRef = __makeref(dest);
            *(IntPtr*)&destRef = *(IntPtr*)&sourceRef;
            return __refvalue(destRef, TDest);
        }

        /// <summary>
        /// Gets the number of contiguous bytes an instance of a given type occupies in memory.
        /// </summary>
        /// <typeparam name="T">The type to get the size of.</typeparam>
        /// <returns>The size in bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SizeOf<T>() where T : unmanaged
        {
            return SizeOfCache<T>.VALUE;
        }

        private static class SizeOfCache<T> where T : unmanaged
        {
            public static readonly int VALUE = SizeOf();

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct DoubleStruct
            {
                public T value0;
                public T value1;
            }

            public static unsafe int SizeOf()
            {
                DoubleStruct doubleStruct = default;

                TypedReference tRef0 = __makeref(doubleStruct.value0);
                TypedReference tRef1 = __makeref(doubleStruct.value1);

                IntPtr ptrToT0 = *(IntPtr*)&tRef0;
                IntPtr ptrToT1 = *(IntPtr*)&tRef1;

                return (int)(((byte*)ptrToT1) - ((byte*)ptrToT0));
            }
        }
    }
}
