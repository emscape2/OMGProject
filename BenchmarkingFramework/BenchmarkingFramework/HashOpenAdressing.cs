using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class HashOpenAdressing : TreeAlgorithm
    {
        int[] containedArray;

        public HashOpenAdressing()
        {
            containedArray = new int[10000020];
            for( int i = 0; i < containedArray.Length; i++)
            {
                containedArray[i] = 0;
            }
        }

        public override void Build(int[] array)
        {
            foreach (int i in array)
            {
                Insert(i);
            }
        }

        public override void Insert(int value)
        {
            int counter = 0;
            int hashValue = Hash(value);
            while (containedArray[hashValue] != 0)
            {
                counter++;
                if (counter > containedArray.Length)
                {
                    throw new Exception("Hash Table either Full or Malfunctioning");
                }
                hashValue = Chaining(hashValue);
            }
            containedArray[hashValue] = value;
        }

        public override bool Delete(int value)
        {
            int index = Lookup(value);
            containedArray[index] = 0;
            return true;
        }

        public override int Lookup(int value)
        {
            int counter = 0;
            int hashValue = Hash(value);
            while (containedArray[hashValue] != value)
            {
                counter++;
                if (counter > containedArray.Length)
                {
                    throw new Exception("Value not present in tablle");
                }
                hashValue = Chaining(hashValue);
            }
            return hashValue;
        }

        public override object Clone()
        {
            return new HashOpenAdressing();
        }

        int Hash(int value)
        {
            long intermediary = 1 + value * value;
            if (intermediary % (containedArray.Length-1) != 0)
                return Math.Abs((int)(intermediary % (long)(containedArray.Length-1)));
            return containedArray.Length - 1;
        }

        int Chaining(int tempvalue)
        {
            int value = tempvalue;
            if (value < containedArray.Length)
            {
                value++;
            }
            else
                value = 1;
            return value;
        }
    }
}
