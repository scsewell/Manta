using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Manta.Benchmarks
{
    public class Vector4Benchmark : BenchmarkBase
    {
        public override void Run(List<Summary> results)
        {
            results.Add(BenchmarkRunner.Run<Indexer>());

            results.Add(BenchmarkRunner.Run<Length>());
            results.Add(BenchmarkRunner.Run<LengthSquared>());
            results.Add(BenchmarkRunner.Run<Normalize>());

            results.Add(BenchmarkRunner.Run<ComponentMin>());
            results.Add(BenchmarkRunner.Run<ComponentMax>());
            results.Add(BenchmarkRunner.Run<ComponentClamp>());

            results.Add(BenchmarkRunner.Run<Add>());
            results.Add(BenchmarkRunner.Run<Subtract>());
            results.Add(BenchmarkRunner.Run<Negate>());
            results.Add(BenchmarkRunner.Run<MultiplyScalar>());
            results.Add(BenchmarkRunner.Run<MultiplyVector>());
            results.Add(BenchmarkRunner.Run<DivideScalar>());
            results.Add(BenchmarkRunner.Run<DivideVector>());

            results.Add(BenchmarkRunner.Run<Dot>());
            results.Add(BenchmarkRunner.Run<Project>());

            results.Add(BenchmarkRunner.Run<Distance>());
            results.Add(BenchmarkRunner.Run<DistanceSquared>());

            results.Add(BenchmarkRunner.Run<Lerp>());
            results.Add(BenchmarkRunner.Run<LerpClamped>());
            results.Add(BenchmarkRunner.Run<MoveTowards>());
        }

        public class Indexer : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf4[i + 1].GetValue(i % 4);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf4[i + 1][i % 4];
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
                    m_f[i] = m_vf4[i + 1].LengthSlow;
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf4[i + 1].Length;
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFast()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf4[i + 1].LengthFast;
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
                    m_f[i] = m_vf4[i + 1].LengthSquaredSlow;
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf4[i + 1].LengthSquared;
                }
            }
        }

        public class Normalize : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.NormalizeSlow(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Normalize(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFast()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.NormalizeFast(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.NormalizeSlow(ref m_vf4[i + 1], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Normalize(ref m_vf4[i + 1], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFastRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.NormalizeFast(ref m_vf4[i + 1], out m_vf4[i]);
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
                    m_vf4[i] = Vector4.ComponentMinSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.ComponentMin(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ComponentMinSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ComponentMin(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
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
                    m_vf4[i] = Vector4.ComponentMaxSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.ComponentMax(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ComponentMaxSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ComponentMax(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
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
                    m_vf4[i] = Vector4.ComponentClampSlow(m_vf4[i + 1], m_vf4[i + 2], m_vf4[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.ComponentClamp(m_vf4[i + 1], m_vf4[i + 2], m_vf4[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ComponentClampSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], ref m_vf4[i + 3], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ComponentClamp(ref m_vf4[i + 1], ref m_vf4[i + 2], ref m_vf4[i + 3], out m_vf4[i]);
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
                    m_vf4[i] = Vector4.AddSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Add(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.AddSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Add(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
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
                    m_vf4[i] = Vector4.SubtractSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Subtract(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.SubtractSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Subtract(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
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
                    m_vf4[i] = Vector4.NegateSlow(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Negate(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.NegateSlow(ref m_vf4[i + 1], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Negate(ref m_vf4[i + 1], out m_vf4[i]);
                }
            }
        }

        public class MultiplyScalar : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.MultiplySlow(m_vf4[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Multiply(m_vf4[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.MultiplySlow(ref m_vf4[i + 1], m_f[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Multiply(ref m_vf4[i + 1], m_f[i + 2], out m_vf4[i]);
                }
            }
        }

        public class MultiplyVector : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.MultiplySlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Multiply(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.MultiplySlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Multiply(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }
        }

        public class DivideScalar : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.DivideSlow(m_vf4[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Divide(m_vf4[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.DivideSlow(ref m_vf4[i + 1], m_f[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Divide(ref m_vf4[i + 1], m_f[i + 2], out m_vf4[i]);
                }
            }
        }

        public class DivideVector : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.DivideSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Divide(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.DivideSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Divide(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }
        }

        public class Dot : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.DotSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.Dot(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.DotSlow(ref m_vf4[i + 1], ref m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.Dot(ref m_vf4[i + 1], ref m_vf4[i + 2]);
                }
            }
        }

        public class Project : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.ProjectSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Project(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.ProjectSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Project(ref m_vf4[i + 1], ref m_vf4[i + 2], out m_vf4[i]);
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
                    m_f[i] = Vector4.DistanceSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.Distance(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.DistanceSlow(ref m_vf4[i + 1], ref m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.Distance(ref m_vf4[i + 1], ref m_vf4[i + 2]);
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
                    m_f[i] = Vector4.DistanceSquaredSlow(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.DistanceSquared(m_vf4[i + 1], m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.DistanceSquaredSlow(ref m_vf4[i + 1], ref m_vf4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4.DistanceSquared(ref m_vf4[i + 1], ref m_vf4[i + 2]);
                }
            }
        }

        public class Lerp : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.LerpSlow(m_vf4[i + 1], m_vf4[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.Lerp(m_vf4[i + 1], m_vf4[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.LerpSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], m_f[i], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.Lerp(ref m_vf4[i + 1], ref m_vf4[i + 2], m_f[i], out m_vf4[i]);
                }
            }
        }

        public class LerpClamped : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.LerpClampedSlow(m_vf4[i + 1], m_vf4[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.LerpClamped(m_vf4[i + 1], m_vf4[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.LerpClampedSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], m_f[i], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.LerpClamped(ref m_vf4[i + 1], ref m_vf4[i + 2], m_f[i], out m_vf4[i]);
                }
            }
        }

        public class MoveTowards : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.MoveTowardsSlow(m_vf4[i + 1], m_vf4[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4.MoveTowards(m_vf4[i + 1], m_vf4[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.MoveTowardsSlow(ref m_vf4[i + 1], ref m_vf4[i + 2], m_f[i], out m_vf4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4.MoveTowards(ref m_vf4[i + 1], ref m_vf4[i + 2], m_f[i], out m_vf4[i]);
                }
            }
        }
    }
}
