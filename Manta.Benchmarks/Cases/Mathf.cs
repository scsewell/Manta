using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;

namespace Manta.Benchmarks
{
    public class MathfBenchmark : BenchmarkBase
    {
        public override void Run(List<Summary> results)
        {
            results.Add(BenchmarkRunner.Run<MinFloat>());
            results.Add(BenchmarkRunner.Run<MinDouble>());

            results.Add(BenchmarkRunner.Run<MaxFloat>());
            results.Add(BenchmarkRunner.Run<MaxDouble>());

            results.Add(BenchmarkRunner.Run<ClampFloat>());
            results.Add(BenchmarkRunner.Run<ClampDouble>());
            
            results.Add(BenchmarkRunner.Run<InvSqrtFastFloat>());

            results.Add(BenchmarkRunner.Run<NextPowerOfTwoInt>());
            results.Add(BenchmarkRunner.Run<NextPowerOfTwoLong>());

            results.Add(BenchmarkRunner.Run<Lerp>());
        }
        
        public class MinFloat : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.MinSlow(m_f[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.Min(m_f[i + 1], m_f[i + 2]);
                }
            }
        }
        
        public class MinDouble : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_d[i] = Mathf.MinSlow(m_d[i + 1], m_d[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_d[i] = Mathf.Min(m_d[i + 1], m_d[i + 2]);
                }
            }
        }
        
        public class MaxFloat : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.MaxSlow(m_f[i + 1], m_f[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.Max(m_f[i + 1], m_f[i + 2]);
                }
            }
        }
        
        public class MaxDouble : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_d[i] = Mathf.MaxSlow(m_d[i + 1], m_d[i + 2]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_d[i] = Mathf.Max(m_d[i + 1], m_d[i + 2]);
                }
            }
        }
        
        public class ClampFloat : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.ClampSlow(m_f[i + 1], m_f[i + 2], m_f[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.Clamp(m_f[i + 1], m_f[i + 2], m_f[i + 3]);
                }
            }
        }
        
        public class ClampDouble : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_d[i] = Mathf.ClampSlow(m_d[i + 1], m_d[i + 2], m_d[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_d[i] = Mathf.Clamp(m_d[i + 1], m_d[i + 2], m_d[i + 3]);
                }
            }
        }
        
        public class InvSqrtFastFloat : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.InvSqrtSlow(m_f[i + 1]);
                }
            }
            
            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.InvSqrtFast(m_f[i + 1]);
                }
            }
        }
        
        public class NextPowerOfTwoInt : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Mathf.NextPowerOfTwoSlow(m_i[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_i[i] = Mathf.NextPowerOfTwo(m_i[i + 1]);
                }
            }
        }
        
        public class NextPowerOfTwoLong : CaseBase
        {
            [Benchmark(OperationsPerInvoke = COUNT, Baseline = true)]
            public void Simple()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_l[i] = Mathf.NextPowerOfTwoSlow(m_l[i + 1]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_l[i] = Mathf.NextPowerOfTwo(m_l[i + 1]);
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
                    m_f[i] = Mathf.LerpSlow(m_f[i + 1], m_f[i + 2], m_f[i + 3]);
                }
            }

            [Benchmark(OperationsPerInvoke = COUNT)]
            public void Optimized()
            {
                for (int i = 0; i < COUNT; i += VARS_PER_ITERATION)
                {
                    m_f[i] = Mathf.Lerp(m_f[i + 1], m_f[i + 2], m_f[i + 3]);
                }
            }
        }
    }
}
