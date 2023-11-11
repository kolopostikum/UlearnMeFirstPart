using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.
            var time = new Stopwatch();

            task.Run();

            time.Restart();

            for (int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }

            time.Stop();

            var result = (double)time.ElapsedMilliseconds/repetitionCount;

            return result;
        }
	}

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var testString = new Benchmark();
            var benchConstruct = new TaskStringConstruct();
            var benchBuilder = new TaskStringBuilder();
            Assert.Less(
                testString.MeasureDurationInMs(benchConstruct, 10000),
                testString.MeasureDurationInMs(benchBuilder, 10000));
        }
    }

    public class TaskStringConstruct : ITask
    {
        public void Run()
        {
            var stringVar = new string('a', 10000);
        }
    }

    public class TaskStringBuilder : ITask
    {
        public void Run()
        {
            var stringBuilderVariable = new StringBuilder();

            for (int i = 0; i < 10000; i++)
            {
                stringBuilderVariable.Append("a");
            }

            stringBuilderVariable.ToString();
        }
    }
}