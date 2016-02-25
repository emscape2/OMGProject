using System;
using System.Collections.Generic;
using System.Linq;

namespace BenchmarkingFramework
{
    /// <summary>
    /// base class for generating arrays
    /// </summary>
    class ArrayGenerator
    {
        public ArrayGenerator()
        {

        }
        /// <summary>
        /// generates the array given a size and seed
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public virtual int[] GenerateArray(int size, int seed)
        {
            return null;
        }
    }
    /// <summary>
    /// Array generator wjich stars counting at the given seed
    /// </summary>
    class CountingArrayGenerator : ArrayGenerator
    {
        public CountingArrayGenerator() : base()
        {

        }
        /// <summary>
        /// returns array, with values counting starting at the seed
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public override int[] GenerateArray(int size, int seed)
        {
            int[] toReturn = new int[size];

            for (int i = 0; i < size; i++)
            {
                toReturn[i] = i + seed;
            }

            return toReturn;

        }
    }

    /// <summary>
    /// Array generator wich fills an array with random values
    /// </summary>
    class RandomArrayGenerator : ArrayGenerator
    {
        public RandomArrayGenerator() : base()
        {

        }

        public override int[] GenerateArray(int size, int seed)
        {
            int[] toReturn = new int[size];
            Random random = new Random(seed);
            for (int i = 0; i < size; i++)
            {
                toReturn[i] = random.Next();
            }

            return toReturn;

        }
    }

    /// <summary>
    /// An array generator wich sorts the random values it generates.
    /// </summary>
    class SortedRandomArrayGenerator : RandomArrayGenerator
    {
        public SortedRandomArrayGenerator() : base()
        {

        }

        public override int[] GenerateArray(int size, int seed)
        {
            int[] toReturn = base.GenerateArray(size, seed);

            List<int> l = toReturn.ToList<int>();
            l.Sort();

            return l.ToArray<int>();

        }
    }

}
