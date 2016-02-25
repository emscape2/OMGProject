using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{/// <summary>
/// overalll benchmarking class, generates loose tests for every selected option
/// </summary>
    class Benchmark
    {
        int[] arraySizes;
        ArrayGenerator[] arrayGenerators;
        TreeAlgorithm[] treeAlgorithms;
        
        public Benchmark(int[] sizes, ArrayGenerator[] generators, TreeAlgorithm[] algorithms)
        {
            arraySizes = sizes;
            arrayGenerators = generators;
            treeAlgorithms = algorithms;
            TestRun.startingMemory = GC.GetTotalMemory(true);
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
                        TreeAlgorithm algorithm = alg.Clone() as TreeAlgorithm;

                        TestRun testRun = new TestRun(alg, alg.GetType().ToString());
                        testRun.Run(testArray, gen.GetType().ToString());
                        GC.Collect();
                    }
                }
            }
        }
    }
}
