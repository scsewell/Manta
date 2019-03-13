using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using System.Numerics;

namespace Manta.Benchmarks
{
    public abstract class BenchmarkBase
    {
        public abstract void Run(List<Summary> results);

        [Config(typeof(Config))]
        [DisassemblyDiagnoser(printAsm: true, printDiff: true)]
        public class CaseBase
        {
            protected const int VARS_PER_ITERATION = 3 + 1;

            /// <summary>
            /// The datasets tested per method call.
            /// </summary>
            public const int COUNT = 1000;

            protected static bool[] m_b;
            protected static int[] m_i;
            protected static long[] m_l;
            protected static double[] m_d;
            protected static float[] m_f;

            protected static Vector2[] m_vf2;
            protected static Vector3[] m_vf3;
            protected static Vector4[] m_vf4;

            protected static Vector2Int[] m_vi2;
            protected static Vector3Int[] m_vi3;
            protected static Vector4Int[] m_vi4;

            public CaseBase()
            {
                m_b = new bool[COUNT * VARS_PER_ITERATION];
                m_i = new int[COUNT * VARS_PER_ITERATION];
                m_l = new long[COUNT * VARS_PER_ITERATION];
                m_d = new double[COUNT * VARS_PER_ITERATION];
                m_f = new float[COUNT * VARS_PER_ITERATION];

                m_vf2 = new Vector2[COUNT * VARS_PER_ITERATION];
                m_vf3 = new Vector3[COUNT * VARS_PER_ITERATION];
                m_vf4 = new Vector4[COUNT * VARS_PER_ITERATION];

                m_vi2 = new Vector2Int[COUNT * VARS_PER_ITERATION];
                m_vi3 = new Vector3Int[COUNT * VARS_PER_ITERATION];
                m_vi4 = new Vector4Int[COUNT * VARS_PER_ITERATION];

                Random random = new Random();

                for (int i = 0; i < COUNT * VARS_PER_ITERATION; i++)
                {
                    m_i[i] = GetInt(random);
                    m_l[i] = GetLong(random);
                    m_f[i] = GetFloat(random);
                    m_d[i] = GetDouble(random);

                    m_vf2[i] = GetVector2(random);
                    m_vf3[i] = GetVector3(random);
                    m_vf4[i] = GetVector4(random);

                    m_vi2[i] = GetVector2Int(random);
                    m_vi3[i] = GetVector3Int(random);
                    m_vi4[i] = GetVector4Int(random);
                }
            }

            private static int GetInt(Random random)
            {
                return (int)(GetFloat(random) * 100f);
            }

            private static long GetLong(Random random)
            {
                return (long)(GetDouble(random) * 4.0 * int.MaxValue);
            }

            private static float GetFloat(Random random)
            {
                return 20f * ((float)random.NextDouble() - 0.5f);
            }

            private static double GetDouble(Random random)
            {
                return 20.0 * (random.NextDouble() - 0.5);
            }

            private static Vector2 GetVector2(Random random)
            {
                return new Vector2(GetFloat(random), GetFloat(random));
            }
            
            private static Vector3 GetVector3(Random random)
            {
                return new Vector3(GetFloat(random), GetFloat(random), GetFloat(random));
            }

            private static Vector4 GetVector4(Random random)
            {
                return new Vector4(GetFloat(random), GetFloat(random), GetFloat(random), GetFloat(random));
            }

            private static Vector2Int GetVector2Int(Random random)
            {
                return new Vector2Int(GetInt(random), GetInt(random));
            }

            private static Vector3Int GetVector3Int(Random random)
            {
                return new Vector3Int(GetInt(random), GetInt(random), GetInt(random));
            }

            private static Vector4Int GetVector4Int(Random random)
            {
                return new Vector4Int(GetInt(random), GetInt(random), GetInt(random), GetInt(random));
            }
        }
    }
}
