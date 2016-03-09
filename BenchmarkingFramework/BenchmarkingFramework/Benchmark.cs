using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BenchmarkingFramework
{/// <summary>
/// overalll benchmarking class, generates loose tests for every selected option
/// </summary>
    class Benchmark
    {
        int[] arraySizes;
        public static int GCCounter = 0;
        ArrayGenerator[] arrayGenerators;
        TreeAlgorithm[] treeAlgorithms;
        
        public Benchmark(int[] sizes, ArrayGenerator[] generators, TreeAlgorithm[] algorithms)
        {
            arraySizes = sizes;
            arrayGenerators = generators;
            treeAlgorithms = algorithms;
            //TestRun.startingMemory = GC.GetTotalMemory(true);
            RunAll();
        }
        /// <summary>
        /// runs all tests
        /// </summary>
        public void RunAll()
        {
            foreach (ArrayGenerator gen in arrayGenerators)
            {
                foreach (int size in arraySizes)
                {

                    int[] testArray = gen.GenerateArray(size, 0);


                    foreach (TreeAlgorithm alg in treeAlgorithms)
                    {

                        //GC.Collect();
                        //GCCounter++;
                        //Thread.Sleep(100);
                        //GC.WaitForPendingFinalizers();

                        //TestRun.startingMemory = GC.GetTotalMemory(false);


                        for (int i = 0; i < 5; i++)
                        {
                            TreeAlgorithm algorithm = alg.Clone() as TreeAlgorithm;
                            TestRun testRun = new TestRun(algorithm);
                            testRun.Run(testArray, gen.GetType().ToString());
                            
                            //values to make the gc not claim data too early
                            int gchelp = testArray.Length;
                        }
                    }
                }
            }
        }
    }
}
