using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class LinkedListNode
    {
        public LinkedListNode next;
        public int key;
        public int value;
        public LinkedListNode(int key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }

    class LinkedList : TreeAlgorithm
    {
        LinkedListNode first;
        LinkedListNode last;

        public LinkedList()
        {
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
            Delete(key);

            LinkedListNode node = new LinkedListNode(key, key);

            if (this.last != null)
                this.last.next = node;

            this.last = node;

            if (this.first == null)
                this.first = node;
        }

        public override bool Delete(int key)
        {
            LinkedListNode prevNode = null;
            LinkedListNode node = this.first;

            while (node != null)
            {
                if (node.key == key)
                {
                    if (prevNode == null)
                        this.first = node.next;
                    else
                        prevNode.next = node.next;

                    if (node.next == null)
                        this.last = prevNode;
                    else
                        node.next = null;

                    return true;
                }

                prevNode = node;
                node = node.next;
            }

            return false;
        }

        public override int Lookup(int key)
        {
            LinkedListNode node = this.first;

            while (node != null)
            {
                if (node.key == key)
                    return node.value;

                node = node.next;
            }

            return -1;
        }

        public override object Clone()
        {
            return new LinkedList();
        }

        public override string GetDataType()
        {
            return "Linked List" ;
        }
    }
}
