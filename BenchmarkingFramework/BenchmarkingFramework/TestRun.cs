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
        public TimeSpan buildTime;
        public TimeSpan insertTime;
        public TimeSpan deleteTime;
        public TimeSpan lookupTime;
        public static long startingMemory;
        public long finishingMemory;
        public string  arrayType;
        public int size;


        public TestRun(TreeAlgorithm testAlgorithm)
        {
            algorithm = testAlgorithm;
        }
        /// <summary>
        /// runs the test and takes the benchmark values
        /// </summary>
        /// <param name="testArray"></param>
        /// <param name="arraytype"></param>
        public void Run(int[] testArray, string arraytype)
        {
            size = testArray.Length;
            arrayType = arraytype;
            Stopwatch timer = new Stopwatch();

            GC.Collect();
            startingMemory = GC.GetTotalMemory(true);

            timer.Start();

            algorithm.Build(testArray);

            timer.Stop();

            finishingMemory = GC.GetTotalMemory(false);

            buildTime = timer.Elapsed;

            int lookupValuesCount = Math.Min(1000, testArray.Length);
            int[] lookupValues = new int[lookupValuesCount];
            for (int i = 0; i < lookupValuesCount; i++)
            {
                lookupValues[i] = testArray[i * (testArray.Length / lookupValuesCount)];
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
            

            Finish();
        }

        public void Finish()
        {
            TestData data = new TestData(arrayType, algorithm.GetDataType(), size, buildTime.TotalMilliseconds, insertTime.TotalMilliseconds, deleteTime.TotalMilliseconds, lookupTime.TotalMilliseconds, finishingMemory - startingMemory);
            DatabaseConnection temp = new DatabaseConnection();
            temp.AddResults(data);
            
        }

    }
}
