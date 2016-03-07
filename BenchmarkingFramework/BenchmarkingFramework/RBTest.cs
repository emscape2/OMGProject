using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class RBTest
    {
        public static void Main(string[] args)
        {
            RBTree t = new RBTree();

            t.Insert(10);
            t.Insert(9);
            t.Insert(8);
            t.Insert(7);
            t.Insert(6);
            t.Insert(5);
            t.Insert(4);
            t.Insert(3);
            t.Insert(2);
            t.Insert(1);
            t.Delete(5);
        }
    }
}
