using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Loggers;

namespace Manta.Benchmarks
{
    public class Benchmark
    {
        public static void Main(string[] args)
        {
            List<Summary> results = new List<Summary>();
            //new MathfBenchmark().Run(results);
            //new Vector2IntBenchmark().Run(results);
            //new Vector4IntBenchmark().Run(results);
            new Vector2Benchmark().Run(results);
            //new Vector4Benchmark().Run(results);

            OutputResults(ConsoleLogger.Default, results);
        }

        private static void OutputResults(ILogger logger, List<Summary> results)
        {
            foreach (Summary summary in results)
            {
                logger.WriteLine();
                logger.WriteLine(LogKind.Header, summary.Title);

                foreach (string[] line in summary.Table.FullContentWithHeader)
                {
                    summary.Table.PrintLine(line, logger, "|", " ");
                }

                BenchmarkReport baseline = summary.Reports.FirstOrDefault(r => r.BenchmarkCase.Descriptor.Baseline);

                foreach (BenchmarkReport report in summary.Reports)
                {
                    if (!report.Success)
                    {
                        logger.WriteLine(LogKind.Error, $"This benchmark did not succeeed!");
                    }

                    if (report != baseline)
                    {
                        double ratio = report.ResultStatistics.Mean / baseline.ResultStatistics.Mean;
                        double difference = (report.ResultStatistics.Mean - baseline.ResultStatistics.Mean) / baseline.ResultStatistics.Mean;

                        if (Math.Abs(difference) < 0.05)
                        {
                            logger.Write(LogKind.Error, $"Optimized method is within ");
                            logger.Write(LogKind.Header, $"{(100 * difference).ToString("0.0")}%");
                            logger.Write(LogKind.Error, $" of the basline, consider simplifying.");
                            logger.WriteLine();
                        }
                        else if (ratio > 1.0)
                        {
                            logger.Write(LogKind.Error, $"Optimized method is ");
                            logger.Write(LogKind.Header, $"{(100 * difference).ToString("0.0")}%");
                            logger.Write(LogKind.Error, $" slower than basline, consider removing.");
                            logger.WriteLine();
                        }
                    }
                }

                logger.WriteLine();
            }
        }
    }
}
