using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Manta.Benchmarks
{
    public class Vector4IntBenchmark : BenchmarkBase
    {
        public override void Run(List<Summary> results)
        {
            results.Add(BenchmarkRunner.Run<Indexer>());

            results.Add(BenchmarkRunner.Run<Length>());
            results.Add(BenchmarkRunner.Run<LengthSquared>());

            results.Add(BenchmarkRunner.Run<ComponentMin>());
            results.Add(BenchmarkRunner.Run<ComponentMinRef>());
            results.Add(BenchmarkRunner.Run<ComponentMax>());
            results.Add(BenchmarkRunner.Run<ComponentMaxRef>());
            results.Add(BenchmarkRunner.Run<ComponentClamp>());
            results.Add(BenchmarkRunner.Run<ComponentClampRef>());

            results.Add(BenchmarkRunner.Run<Ceil>());
            results.Add(BenchmarkRunner.Run<CeilRef>());
            results.Add(BenchmarkRunner.Run<Floor>());
            results.Add(BenchmarkRunner.Run<FloorRef>());
            results.Add(BenchmarkRunner.Run<Round>());
            results.Add(BenchmarkRunner.Run<RoundRef>());

            results.Add(BenchmarkRunner.Run<Add>());
            results.Add(BenchmarkRunner.Run<AddRef>());
            results.Add(BenchmarkRunner.Run<Subtract>());
            results.Add(BenchmarkRunner.Run<SubtractRef>());
            results.Add(BenchmarkRunner.Run<Negate>());
            results.Add(BenchmarkRunner.Run<NegateRef>());
            results.Add(BenchmarkRunner.Run<Scale>());
            results.Add(BenchmarkRunner.Run<ScaleRef>());
            results.Add(BenchmarkRunner.Run<Multiply>());
            results.Add(BenchmarkRunner.Run<MultiplyRef>());

            results.Add(BenchmarkRunner.Run<Distance>());
            results.Add(BenchmarkRunner.Run<DistanceRef>());
            results.Add(BenchmarkRunner.Run<DistanceSquared>());
            results.Add(BenchmarkRunner.Run<DistanceSquaredRef>());

            //results.Add(BenchmarkRunner.Run<EqualsBench>());

            results.Add(BenchmarkRunner.Run<Vector4Cast>());
        }
        
        public class Indexer : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi4[i + 1].GetValue(i % 4);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi4[i + 1][i % 4];
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
                    m_f[i] = m_vi4[i + 1].LengthSlow;
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = m_vi4[i + 1].Length;
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
                    m_i[i] = m_vi4[i + 1].LengthSquared;
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = m_vi4[i + 1].LengthSquaredOptimized;
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
                    m_vi4[i] = Vector4Int.ComponentMinSlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.ComponentMin(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class ComponentMinRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ComponentMinSlow(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ComponentMin(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.ComponentMaxSlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.ComponentMax(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class ComponentMaxRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ComponentMaxSlow(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ComponentMax(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.ComponentClampSlow(m_vi4[i + 1], m_vi4[i + 2], m_vi4[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.ComponentClamp(m_vi4[i + 1], m_vi4[i + 2], m_vi4[i + 3]);
                }
            }
        }
        
        public class ComponentClampRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ComponentClampSlow(ref m_vi4[i + 1], ref m_vi4[i + 2], ref m_vi4[i + 3], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ComponentClamp(ref m_vi4[i + 1], ref m_vi4[i + 2], ref m_vi4[i + 3], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.CeilSlow(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Ceil(m_vf4[i + 1]);
                }
            }
        }
        
        public class CeilRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.CeilSlow(ref m_vf4[i + 1], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Ceil(ref m_vf4[i + 1], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.FloorSlow(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Floor(m_vf4[i + 1]);
                }
            }
        }
        
        public class FloorRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.FloorSlow(ref m_vf4[i + 1], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Floor(ref m_vf4[i + 1], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.RoundSlow(m_vf4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Round(m_vf4[i + 1]);
                }
            }
        }
        
        public class RoundRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.RoundSlow(ref m_vf4[i + 1], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Round(ref m_vf4[i + 1], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.AddSlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Add(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class AddRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.AddSlow(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Add(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.SubtractSlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Subtract(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class SubtractRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.SubtractSlow(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Subtract(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.NegateSlow(m_vi4[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Negate(m_vi4[i + 1]);
                }
            }
        }
        
        public class NegateRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.NegateSlow(ref m_vi4[i + 1], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Negate(ref m_vi4[i + 1], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.ScaleSlow(m_vi4[i + 1], m_i[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Scale(m_vi4[i + 1], m_i[i + 1]);
                }
            }
        }
        
        public class ScaleRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.ScaleSlow(ref m_vi4[i + 1], m_i[i + 1], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Scale(ref m_vi4[i + 1], m_i[i + 1], out m_vi4[i]);
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
                    m_vi4[i] = Vector4Int.MultiplySlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vi4[i] = Vector4Int.Multiply(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class MultiplyRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.MultiplySlow(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    Vector4Int.Multiply(ref m_vi4[i + 1], ref m_vi4[i + 2], out m_vi4[i]);
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
                    m_f[i] = Vector4Int.DistanceSlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4Int.Distance(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class DistanceRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4Int.DistanceSlow(ref m_vi4[i + 1], ref m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Vector4Int.Distance(ref m_vi4[i + 1], ref m_vi4[i + 2]);
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
                    m_i[i] = Vector4Int.DistanceSquaredSlow(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector4Int.DistanceSquared(m_vi4[i + 1], m_vi4[i + 2]);
                }
            }
        }
        
        public class DistanceSquaredRef : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector4Int.DistanceSquaredSlow(ref m_vi4[i + 1], ref m_vi4[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Vector4Int.DistanceSquared(ref m_vi4[i + 1], ref m_vi4[i + 2]);
                }
            }
        }
        
        /*
        public class EqualsBench : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_b[i] = m_vi4[i + 1].EqualsSlow(m_vi4[i + 2]);
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_b[i] = m_vi4[i + 1].Equals(m_vi4[i + 2]);
                }
            }
        }
        */
        
        public class Vector4Cast : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = Vector4Int.Vector4CastSlow(m_vi4[i + 1]);
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_vf4[i] = (Vector4)m_vi4[i + 1];
                }
            }
        }
    }
}
