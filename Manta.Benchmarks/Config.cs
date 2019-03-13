using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;

namespace Manta.Benchmarks
{
    public class Config : ManualConfig
    {
        public Config()
        {
            Add(
                new Job("Core", RunMode.Short, EnvironmentMode.RyuJitX64)
                {
                    Environment = { Runtime = Runtime.Core },
                }.With(new[] { new MsBuildArgument("/p:DefineConstants=BENCHMARK") })
            );
        }
    }
}