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
        }

        public override void Insert(int key)
        {
            Delete(key); // Insert needs to replace
            int index = GetFreeIndex(key);
            if (index == -1) throw new Exception("No more room in the table");
            containedArray[index] = key;
        }

        public override bool Delete(int key)
        {
            int index = GetIndex(key);
            if (index == -1) return false;
            containedArray[index] = 0;
            return true;
        }

        public override int Lookup(int key)
        {
            int index = GetIndex(key);
            if (index == -1) return -1;
            return containedArray[index];
        }

        public override int GetIndex(int key)
        {
            return GetIndexWithValue(key, key);
        }

        public int GetFreeIndex(int key)
        {
            return GetIndexWithValue(key, 0);
        }

        public int GetIndexWithValue(int key, int value)
        {
            int counter = 0;
            int hashValue = Hash(key);
            while (containedArray[hashValue] != value)
            {
                counter++;
                if (counter > containedArray.Length)
                    return -1;
                hashValue = Chaining(hashValue);
            }
            return hashValue;
          }

        public override object Clone()
        {
            return new HashOpenAdressing();
        }

        int Hash(int key)
        {
            long intermediary = 1 + key * key;
            int maxIndex = containedArray.Length;

            if (intermediary % maxIndex != 0)
                return Math.Abs((int)(intermediary % (long)maxIndex));

            return maxIndex;
        }

        int Chaining(int hashValue)
        {
            if (hashValue < containedArray.Length - 1)
                hashValue++;
            else
                hashValue = 1;

            return hashValue;
        }
    }
}
