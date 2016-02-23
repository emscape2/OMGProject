using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class TreeAlgorithm : ICloneable
    {
        public virtual void Build(int[] array)
        {

        }
        public virtual void Insert(int value)
        {

        }

        public virtual void Delete(int value)
        {

        }

        public virtual int Lookup( int value)
        {
            return 0;
        }

        public virtual object Clone()
        {
            return new TreeAlgorithm();
        }
    }
}
