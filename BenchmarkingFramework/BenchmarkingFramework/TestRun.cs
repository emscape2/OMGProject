using System.Diagnostics;
using System;

namespace BenchmarkingFramework
{
    /// <summary>
    /// Single test, with one array.
    /// </summary>
    class TestRun
    {
        public TreeAlgorithm algorithm;
        public TimeSpan build;
        public TimeSpan insertTime;
        public TimeSpan deleteTime;
        public TimeSpan lookupTime;
        public static long startingMemory;
        public long finishingMemory;
        public string size, arrayType, algorithmType;


        public TestRun(TreeAlgorithm testAlgorithm, string algorithmtype)
        {
            algorithm = testAlgorithm;
            algorithmType = algorithmtype;
        }
        /// <summary>
        /// runs the test and takes the benchmark values
        /// </summary>
        /// <param name="testArray"></param>
        /// <param name="arraytype"></param>
        public void Run(int[] testArray, string arraytype)
        {
            size = testArray.Length.ToString();
            arrayType = arraytype;

            Stopwatch timer = new Stopwatch();

            timer.Start();
            algorithm.Build(testArray);
            timer.Stop();
            build = timer.Elapsed;

            int[] lookupValues = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                lookupValues[i] = testArray[i * (testArray.Length / 1000)];
            }

            timer.Restart();
            foreach(int j in lookupValues)
            {
                algorithm.Delete(j);
            }
            timer.Stop();
            deleteTime = timer.Elapsed;


            timer.Restart();
            foreach (int j in lookupValues)
            {
                algorithm.Insert(j);
            }
            timer.Stop();
            insertTime = timer.Elapsed;


            timer.Restart();
            foreach (int j in lookupValues)
            {
                algorithm.Lookup(j);
            }
            timer.Stop();
            lookupTime = timer.Elapsed;

            finishingMemory = GC.GetTotalMemory(false);
        }

    }
}
