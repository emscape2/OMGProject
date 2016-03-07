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

        public override void Build(int[] array)
        {
            foreach (int i in array)
            {
                Insert(i);
            }
        }

        public override void Insert(int key)
        {
            if (key == 0)
                return;
            Delete(key); // Insert needs to replace
            int index = GetFreeIndex(key);
            if (index == -1) throw new Exception("No more room in the table");
            containedArray[index] = key;
        }

        public override bool Delete(int key)
        {
            if (key == 0)
                return false;
            int index = GetIndexWithValue(key,key,true);
            if (index == -1) return false;
            containedArray[index] = -1;
            return true;
        }

        public override int Lookup(int key)
        {
            if (key == 0)
                return 0;
            int index = GetIndex(key);
            if (index == -1) return -1;
            return containedArray[index];
        }

        public int GetIndex(int key)
        {
            return GetIndexWithValue(key, key,false);
        }

        public int GetFreeIndex(int key)
        {
            return GetIndexWithValue(key, 0,false);
        }

        public int GetIndexWithValue(int key, int value, bool deletion)
        {
            int counter = 0;
            int hashValue = Hash(key);
            if (key == 0)
            {

                while (containedArray[hashValue] != 0 || containedArray[hashValue] != -1)
                {
                    counter++;
                    if (counter > containedArray.Length)
                        return -1;
                    hashValue = Chaining(hashValue);
                }
            }
            else
            {
                while (containedArray[hashValue] != value)
                {

                    counter++;
                    if ((deletion && containedArray[hashValue] == 0) || counter > containedArray.Length)
                        return -1;
                    hashValue = Chaining(hashValue);
                }
            }
            return hashValue;
          }

        public override object Clone()
        {
            return new HashOpenAdressing();
        }

        int Hash(int key)
        {
            long intermediary = 1 + key * key ;
            int maxIndex = containedArray.Length - 1;

            if (intermediary % maxIndex != 0)
                return Math.Abs((int)(intermediary % (long)maxIndex));

            return maxIndex;
        }

        int Chaining(int hashValue)
        {
            if (hashValue < containedArray.Length-1)
                hashValue++;
            else
                hashValue = 1;

            return hashValue;
        }

        public override string GetDataType()
        {
            return "Hash Open Adressing";
        }
    }
}
