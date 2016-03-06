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
        private int SIZE;
        private const int DEFAULT_SIZE = 10000020;

        /// <summary>
        /// Constructor with default table size of 10000020.
        /// </summary>
        public HashChaining()
        {
            new HashChaining(DEFAULT_SIZE);
        }

        /// <summary>
        /// Call this constructor to set the size manually.
        /// </summary>
        /// <param name="size">The required size.</param>
        public HashChaining(int size)
        {
            _table = new LinkedListNode[SIZE];
        }

        /// <summary>
        /// Lookup a value under a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override int Lookup(int key)
        {
            int hash = key % SIZE;
            if (_table[SIZE] == null)
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
            Insert(key, value);
        }

        /// <summary>
        /// Insert method that supports passing a key/value pair
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert (int key, int value)
        {
            // largest possible hash is SIZE-1
            int hash = key % SIZE;
            LinkedListNode currentNode = _table[hash];
            if (currentNode != null && currentNode.key == key)
                _table[hash].value = value;
            while (currentNode.next != null && currentNode.next.key != key)
                currentNode = currentNode.next;
            if (currentNode.next == null)
                currentNode.next = new LinkedListNode(key, value);
            else
                currentNode.next.value = value;
            return;
        }

        /// <summary>
        /// Tries to delete the node with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>returns true on success, false on failure.</returns>
        public override bool Delete(int key)
        {
            int hash = key % SIZE;
            LinkedListNode currentNode = _table[hash];
            if (currentNode.key == key)
            {
                _table[hash] = null;
                return true;
            }
            while (currentNode.next != null && currentNode.next.key != key)
                currentNode = currentNode.next;
            if (currentNode.next != null && currentNode.next.key == key)
            {
                currentNode.next = null;
                return true;
            }
            return false;
        }

        public override int GetIndex(int key)
        {
            return base.GetIndex(key);
        }

        /// <summary>
        /// gives back "a" value for storing
        /// </summary>
        /// <returns></returns>
        private int getAValue()
        {
            return 42;
        }
    }
}
