using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    struct KeyValue
    {
        public int key;
        public int value;
        public KeyValue(int Key, int Value)
        {
            key = Key;
            value = Value;
        }

        public bool IsDeleted
        {
            get 
            {
                return this.key == -1;
            }
        }

        public bool IsFree
        {
            get 
            {
                return this.key == 0;
            }
        }

        public bool IsOccupied
        {
            get 
            {
                return this.key > 0;
            }
        }

        public static KeyValue Deleted {
            get
            {
                return new KeyValue(-1, -1);
            }
        }
    }

    class HashOpenAddressing : TreeAlgorithm
    {
        int DEFAULT_SIZE = 10000;
        double GROW_THRESHOLD = 0.8;
        double GROW_RATE = 2.0;

        KeyValue[] containedArray;
        int length;

        public HashOpenAddressing()
        {
            
        }

        public override void Build(int[] array)
        {
            int size = DEFAULT_SIZE;
            while (array.Length > size * GROW_THRESHOLD)
            {
                size = (int)(size * GROW_RATE);
            }
            containedArray = new KeyValue[size];

            length = 0;
            foreach (int i in array)
            {
                Insert(i);
            }
        }

        public override void Insert(int key)
        {
            Insert(new KeyValue(key, key));
        }

        public void Insert(KeyValue node)
        {
            if (node.key == 0)
                return;
            int index = GetIndex(node.key);
            if (index == -1)
            {
                Grow();
                length++;
                index = GetFreeIndex(node.key);
            }

            containedArray[index] = node;
        }

        void Grow()
        {
            if (length <= containedArray.Length * GROW_THRESHOLD) return;

            KeyValue[] oldArray = (KeyValue[])containedArray.Clone();
            containedArray = new KeyValue[(int)(oldArray.Length * GROW_RATE)];
            length = 0;
            foreach (KeyValue node in oldArray)
            {
                Insert(node);
            }
        }

        public override bool Delete(int key)
        {
            if (key == 0)
                return false;
            int index = GetIndex(key);
            if (index == -1) 
                return false;

            length--;
            containedArray[index] = KeyValue.Deleted;

            return true;
        }

        public override int Lookup(int key)
        {
            if (key == 0)
                return 0;

            int index = GetIndex(key);
            if (index == -1) 
                return -1;

            return containedArray[index].value;
        }

        int GetIndex(int key)
        {
            int counter = 0;
            int hashValue = Hash(key);
            while (containedArray[hashValue].key != key)
            {
                counter++;
                if (containedArray[hashValue].IsFree || counter > containedArray.Length)
                    return -1;
                hashValue = Probe(hashValue);
            }
            return hashValue;
        }

        int GetFreeIndex(int key)
        {
            int counter = 0;
            int hashValue = Hash(key);
            while (containedArray[hashValue].IsOccupied)
            {
                counter++;
                if (counter > containedArray.Length)
                    return -1;
                hashValue = Probe(hashValue);
            }
            return hashValue;
        }

        int Hash(int key)
        {
            long intermediary = 1 + key * key ;
            int maxIndex = containedArray.Length - 1;

            if (intermediary % maxIndex != 0)
                return Math.Abs((int)(intermediary % (long)maxIndex));

            return maxIndex;
        }

        int Probe(int hashValue)
        {
            if (hashValue < containedArray.Length-1)
                hashValue++;
            else
                hashValue = 1;

            return hashValue;
        }

        public override object Clone()
        {
            return new HashOpenAddressing();
        }

        public override string GetDataType()
        {
            return "Hash Open Addressing, length = " + length;
        }
    }
}
