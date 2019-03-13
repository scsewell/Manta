using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Manta.Benchmarks
{
    public class Vector2IntBenchmark : BenchmarkBase
    {
        public override void Run(List<Summary> results)
        {
            results.Add(BenchmarkRunner.Run<Indexer>());

            results.Add(BenchmarkRunner.Run<Length>());
            results.Add(BenchmarkRunner.Run<LengthSquared>());

            results.Add(BenchmarkRunner.Run<ComponentMin>());
            results.Add(BenchmarkRunner.Run<ComponentMax>());
            results.Add(BenchmarkRunner.Run<ComponentClamp>());

            results.Add(BenchmarkRunner.Run<Ceil>());
            results.Add(BenchmarkRunner.Run<Floor>());
            results.Add(BenchmarkRunner.Run<Round>());

            results.Add(BenchmarkRunner.Run<Add>());
            results.Add(BenchmarkRunner.Run<Subtract>());
            results.Add(BenchmarkRunner.Run<Negate>());
            results.Add(BenchmarkRunner.Run<Scale>());
            results.Add(BenchmarkRunner.Run<Multiply>());

            results.Add(BenchmarkRunner.Run<Distance>());
            results.Add(BenchmarkRunner.Run<DistanceSquared>());

            results.Add(BenchmarkRunner.Run<Vector2Cast>());
        }

        public class Indexer : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi2[i + 1].GetValue(i % 2);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi2[i + 1][i % 2];
                }
            }
        }

        public class Length : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vi2[i + 1].LengthSlow;
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vi2[i + 1].Length;
                }
            }
        }

        public class LengthSquared : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi2[i + 1].LengthSquared;
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi2[i + 1].LengthSquaredOptimized;
                }
            }
        }

        public class ComponentMin : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ComponentMinSlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ComponentMin(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ComponentMinSlow(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ComponentMin(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }
        }

        public class ComponentMax : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ComponentMaxSlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ComponentMax(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ComponentMaxSlow(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ComponentMax(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }
        }

        public class ComponentClamp : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ComponentClampSlow(m_vi2[i + 1], m_vi2[i + 2], m_vi2[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ComponentClamp(m_vi2[i + 1], m_vi2[i + 2], m_vi2[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ComponentClampSlow(ref m_vi2[i + 1], ref m_vi2[i + 2], ref m_vi2[i + 3], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ComponentClamp(ref m_vi2[i + 1], ref m_vi2[i + 2], ref m_vi2[i + 3], out m_vi2[i]);
                }
            }
        }

        public class Ceil : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.CeilSlow(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Ceil(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.CeilSlow(ref m_vf2[i + 1], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Ceil(ref m_vf2[i + 1], out m_vi2[i]);
                }
            }
        }

        public class Floor : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.FloorSlow(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Floor(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.FloorSlow(ref m_vf2[i + 1], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Floor(ref m_vf2[i + 1], out m_vi2[i]);
                }
            }
        }

        public class Round : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.RoundSlow(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Round(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.RoundSlow(ref m_vf2[i + 1], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Round(ref m_vf2[i + 1], out m_vi2[i]);
                }
            }
        }

        public class Add : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.AddSlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Add(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.AddSlow(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Add(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }
        }

        public class Subtract : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.SubtractSlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Subtract(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.SubtractSlow(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Subtract(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }
        }

        public class Negate : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.NegateSlow(m_vi2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Negate(m_vi2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.NegateSlow(ref m_vi2[i + 1], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Negate(ref m_vi2[i + 1], out m_vi2[i]);
                }
            }
        }

        public class Scale : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.ScaleSlow(m_vi2[i + 1], m_i[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Scale(m_vi2[i + 1], m_i[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.ScaleSlow(ref m_vi2[i + 1], m_i[i + 1], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Scale(ref m_vi2[i + 1], m_i[i + 1], out m_vi2[i]);
                }
            }
        }

        public class Multiply : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.MultiplySlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi2[i] = Vector2Int.Multiply(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.MultiplySlow(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2Int.Multiply(ref m_vi2[i + 1], ref m_vi2[i + 2], out m_vi2[i]);
                }
            }
        }

        public class Distance : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2Int.DistanceSlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2Int.Distance(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2Int.DistanceSlow(ref m_vi2[i + 1], ref m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2Int.Distance(ref m_vi2[i + 1], ref m_vi2[i + 2]);
                }
            }
        }

        public class DistanceSquared : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector2Int.DistanceSquaredSlow(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector2Int.DistanceSquared(m_vi2[i + 1], m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector2Int.DistanceSquaredSlow(ref m_vi2[i + 1], ref m_vi2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector2Int.DistanceSquared(ref m_vi2[i + 1], ref m_vi2[i + 2]);
                }
            }
        }

        public class EqualsBench : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_b[i] = m_vi2[i + 1].EqualsSlow(m_vi2[i + 2]);
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_b[i] = m_vi2[i + 1].Equals(m_vi2[i + 2]);
                }
            }
        }

        public class Vector2Cast : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2Int.Vector2CastSlow(m_vi2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = (Vector2)m_vi2[i + 1];
                }
            }
        }
    }
}
