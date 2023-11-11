using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            for (int i = 16; i < 1024;)
            {
                ClassTestArray(classesTimes, benchmark, repetitionsCount, i);
                StructTestArray(structuresTimes, benchmark, repetitionsCount, i);
                i *= 2;
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        private static void StructTestArray(
            List<ExperimentResult> structuresTimes, IBenchmark benchmark, int repetitionsCount, int i)
        {
            var structTest = new StructArrayCreationTask(i);
            var structExperement = new ExperimentResult
                (i, benchmark.MeasureDurationInMs
                    (structTest, repetitionsCount));
            structuresTimes.Add(structExperement);
        }

        private static void ClassTestArray(
            List<ExperimentResult> classesTimes, IBenchmark benchmark, int repetitionsCount, int i)
        {
            var classTest = new ClassArrayCreationTask(i);
            var classesExperement = new ExperimentResult
                (i, benchmark.MeasureDurationInMs
                    (classTest, repetitionsCount));
            classesTimes.Add(classesExperement);
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            for (int i = 16; i < 1024;)
            {
                ClassTestMethod(classesTimes, benchmark, repetitionsCount, i);
                StructTestMeethod(structuresTimes, benchmark, repetitionsCount, i);

                i *= 2;
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        private static void StructTestMeethod(
            List<ExperimentResult> structuresTimes, IBenchmark benchmark, int repetitionsCount, int i)
        {
            var structTest = new MethodCallWithStructArgumentTask(i);
            var structExperement = new ExperimentResult
                (i, benchmark.MeasureDurationInMs
                    (structTest, repetitionsCount));
            structuresTimes.Add(structExperement);
        }

        private static void ClassTestMethod(
            List<ExperimentResult> classesTimes, IBenchmark benchmark, int repetitionsCount, int i)
        {
            var classTest = new MethodCallWithClassArgumentTask(i);
            var classesExperement = new ExperimentResult
                (i, benchmark.MeasureDurationInMs
                    (classTest, repetitionsCount));
            classesTimes.Add(classesExperement);
        }
    }
}