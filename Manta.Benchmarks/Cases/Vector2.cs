using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Manta.Benchmarks
{
    public class Vector2Benchmark : BenchmarkBase
    {
        public override void Run(List<Summary> results)
        {
            //results.Add(BenchmarkRunner.Run<Indexer>());

            results.Add(BenchmarkRunner.Run<ComponentMin>());
            results.Add(BenchmarkRunner.Run<ComponentMax>());
            results.Add(BenchmarkRunner.Run<ComponentClamp>());

            results.Add(BenchmarkRunner.Run<Length>());
            results.Add(BenchmarkRunner.Run<LengthSquared>());
            results.Add(BenchmarkRunner.Run<Normalize>());

            results.Add(BenchmarkRunner.Run<Add>());
            results.Add(BenchmarkRunner.Run<Subtract>());
            results.Add(BenchmarkRunner.Run<Negate>());
            results.Add(BenchmarkRunner.Run<MultiplyScalar>());
            results.Add(BenchmarkRunner.Run<MultiplyVector>());
            results.Add(BenchmarkRunner.Run<DivideScalar>());
            results.Add(BenchmarkRunner.Run<DivideVector>());

            results.Add(BenchmarkRunner.Run<Dot>());
            //results.Add(BenchmarkRunner.Run<Project>());

            //results.Add(BenchmarkRunner.Run<Distance>());
            //results.Add(BenchmarkRunner.Run<DistanceSquared>());

            //results.Add(BenchmarkRunner.Run<Lerp>());
            //results.Add(BenchmarkRunner.Run<LerpClamped>());
            //results.Add(BenchmarkRunner.Run<MoveTowards>());
        }

        public class Indexer : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf2[i + 1].GetValue(i % 4);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vf2[i + 1][i % 4];
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
                    m_vf2[i] = Vector2.ComponentMinSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.ComponentMin(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ComponentMinSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ComponentMin(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.ComponentMaxSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.ComponentMax(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ComponentMaxSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ComponentMax(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.ComponentClampSlow(m_vf2[i + 1], m_vf2[i + 2], m_vf2[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.ComponentClamp(m_vf2[i + 1], m_vf2[i + 2], m_vf2[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ComponentClampSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], ref m_vf2[i + 3], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ComponentClamp(ref m_vf2[i + 1], ref m_vf2[i + 2], ref m_vf2[i + 3], out m_vf2[i]);
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
                    m_f[i] = Vector2.MagnitudeSlow(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.Magnitude(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFast()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.MagnitudeFast(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.MagnitudeSlow(ref m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.Magnitude(ref m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFastRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.MagnitudeFast(ref m_vf2[i + 1]);
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
                    m_f[i] = Vector2.MagnitudeSquared(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.MagnitudeSquared(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.MagnitudeSquaredSlow(ref m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.MagnitudeSquared(ref m_vf2[i + 1]);
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
                    m_vf2[i] = Vector2.NormalizeSlow(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Normalize(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFast()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.NormalizeFast(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.NormalizeSlow(ref m_vf2[i + 1], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Normalize(ref m_vf2[i + 1], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedFastRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.NormalizeFast(ref m_vf2[i + 1], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.AddSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Add(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.AddSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Add(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.SubtractSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Subtract(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.SubtractSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Subtract(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.NegateSlow(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Negate(m_vf2[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.NegateSlow(ref m_vf2[i + 1], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Negate(ref m_vf2[i + 1], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.MultiplySlow(m_vf2[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Multiply(m_vf2[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.MultiplySlow(ref m_vf2[i + 1], m_f[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Multiply(ref m_vf2[i + 1], m_f[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.MultiplySlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Multiply(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.MultiplySlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Multiply(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.DivideSlow(m_vf2[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Divide(m_vf2[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.DivideSlow(ref m_vf2[i + 1], m_f[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Divide(ref m_vf2[i + 1], m_f[i + 2], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.DivideSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Divide(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.DivideSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Divide(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_f[i] = Vector2.DotSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.Dot(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.DotSlow(ref m_vf2[i + 1], ref m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.Dot(ref m_vf2[i + 1], ref m_vf2[i + 2]);
                }
            }
        }
        /*
        public class Project : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.ProjectSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Project(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.ProjectSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Project(ref m_vf2[i + 1], ref m_vf2[i + 2], out m_vf2[i]);
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
                    m_f[i] = Vector2.DistanceSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.Distance(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.DistanceSlow(ref m_vf2[i + 1], ref m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.Distance(ref m_vf2[i + 1], ref m_vf2[i + 2]);
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
                    m_f[i] = Vector2.DistanceSquaredSlow(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.DistanceSquared(m_vf2[i + 1], m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.DistanceSquaredSlow(ref m_vf2[i + 1], ref m_vf2[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector2.DistanceSquared(ref m_vf2[i + 1], ref m_vf2[i + 2]);
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
                    m_vf2[i] = Vector2.LerpSlow(m_vf2[i + 1], m_vf2[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.Lerp(m_vf2[i + 1], m_vf2[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.LerpSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], m_f[i], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.Lerp(ref m_vf2[i + 1], ref m_vf2[i + 2], m_f[i], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.LerpClampedSlow(m_vf2[i + 1], m_vf2[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.LerpClamped(m_vf2[i + 1], m_vf2[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.LerpClampedSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], m_f[i], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.LerpClamped(ref m_vf2[i + 1], ref m_vf2[i + 2], m_f[i], out m_vf2[i]);
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
                    m_vf2[i] = Vector2.MoveTowardsSlow(m_vf2[i + 1], m_vf2[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf2[i] = Vector2.MoveTowards(m_vf2[i + 1], m_vf2[i + 2], m_f[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void SimpleRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.MoveTowardsSlow(ref m_vf2[i + 1], ref m_vf2[i + 2], m_f[i], out m_vf2[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void OptimizedRef()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector2.MoveTowards(ref m_vf2[i + 1], ref m_vf2[i + 2], m_f[i], out m_vf2[i]);
                }
            }
        }
        */
    }
}
