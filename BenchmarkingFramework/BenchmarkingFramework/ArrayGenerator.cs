using System;
using System.Collections.Generic;
using System.Linq;

namespace BenchmarkingFramework
{
    class ArrayGenerator
    {
        public ArrayGenerator()
        {

        }

        public virtual int[] GenerateArray(int size, int seed)
        {
            return null;
        }
    }

    class CountingArrayGenerator : ArrayGenerator
    {
        public CountingArrayGenerator() : base()
        {

        }

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

    class RandomArrayGenerator : ArrayGenerator
    {
        public RandomArrayGenerator() : base()
        {

        }

        public override int[] GenerateArray(int size, int seed)
        {
            int[] toReturn = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                toReturn[i] = random.Next();
            }

            return toReturn;

        }
    }

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
