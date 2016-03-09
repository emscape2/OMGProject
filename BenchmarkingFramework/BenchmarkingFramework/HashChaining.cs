using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    /// <summary>
    /// Author: Oscar
    /// A Hashtable that uses seperate chaining to deal with colisions.
    /// </summary>
    class HashChaining : TreeAlgorithm
    {
        private LinkedListNode[] _table;
        public const int DEFAULT_SIZE = 500009;
        double GROW_THRESHOLD = 2.0; // Average of 2 entries per bucket
        double GROW_RATE = 2.0;

        int length;


        public override void Build(int[] array)
        {
            int size = DEFAULT_SIZE;
            while (array.Length > size * GROW_THRESHOLD)
            {
                size = (int)(size * GROW_RATE);
            }
            _table = new LinkedListNode[size];

            length = 0;
            foreach (int i in array)
            {
                Insert(i);
            }
        }

        /// <summary>
        /// Lookup a value under a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override int Lookup(int key)
        {
            int hash = key % _table.Length;
            if (_table[hash] == null)
                return -1;
            else
            {
                LinkedListNode currentNode = _table[hash];
                while (currentNode != null && currentNode.key != key)
                    currentNode = currentNode.next;
                if (currentNode.key == key)
                    return currentNode.value;
                else
                    return -1;
            }
        }

        /// <summary>
        /// This is the general overridden Insert method that does not support passing 
        /// an actual value. I've taken the liberty to overload it with one that does
        /// actually allow passing both a key/value
        /// </summary>
        /// <param name="key"></param>
        public override void Insert(int key)
        {
            int value = getAValue();
            Insert(new LinkedListNode(key, value));
        }

        /// <summary>
        /// Insert method that supports passing a key/value pair
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert (LinkedListNode node)
        {
            // Grow();

            int key = node.key;
            int value = node.value;
            // largest possible hash is SIZE-1
            int hash = key % _table.Length;
            LinkedListNode currentNode = _table[hash];
            if (currentNode != null && currentNode.key == key)
            {
                length++;
                _table[hash].value = value;
                return;
            }

            
            //Emiels code checks for empty nodes
            if (currentNode == null)
            {
                length++;
                _table[hash] = node;
                return;
            }


            while (currentNode.next != null && currentNode.next.key != key)
                currentNode = currentNode.next;
            if (currentNode.next == null)
            {
                length++;
                currentNode.next = node;
            }
            else
                currentNode.next.value = value;
            return;
        }

        /*
        void Grow()
        {
            if (length <= _table.Length * GROW_THRESHOLD) return;

            LinkedListNode[] oldArray = (LinkedListNode[])_table.Clone();
            _table = new LinkedListNode[(int)(oldArray.Length * GROW_RATE)];
            length = 0;
            foreach (LinkedListNode node in oldArray)
            {
                // Doesn't work right, because each of the connected entries should be reinserted
                Insert(node);
            }
        }
        */

        /// <summary>
        /// Tries to delete the node with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>returns true on success, false on failure.</returns>
        public override bool Delete(int key)
        {
            int hash = key % _table.Length;
            LinkedListNode currentNode = _table[hash];

            //failcheck by emiel
            if (currentNode == null)
                return false;

            if (currentNode.key == key)
            {
                length--;
                _table[hash] = null;
                return true;
            }
            while (currentNode.next != null && currentNode.next.key != key)
                currentNode = currentNode.next;
            if (currentNode.next != null && currentNode.next.key == key)
            {
                length--;
                currentNode.next = null;
                return true;
            }
            return false;
        }


        /// <summary>
        /// gives back "a" value for storing
        /// </summary>
        /// <returns></returns>
        private int getAValue()
        {
            return 42;
        }


        public override string GetDataType()
        {
            return "HashChaining, length = " + length;
        }

        public override object Clone()
        {
            return new HashChaining();
        }
    }
}
