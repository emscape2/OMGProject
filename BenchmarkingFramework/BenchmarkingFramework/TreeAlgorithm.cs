using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class TreeAlgorithm : ICloneable
    {
        /// <summary>
        /// builds the datastructure
        /// </summary>
        /// <param name="array"></param>
        public virtual void Build(int[] array)
        {

        }
        /// <summary>
        /// inserts one value
        /// </summary>
        /// <param name="value"></param>
        public virtual void Insert(int value)
        {

        }
        /// <summary>
        /// searches and deletes the value
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Delete(int value)
        {
            return true;
        }

        /// <summary>
        /// searches for the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual int Lookup( int value)
        {
            return 0;
        }

        /// <summary>
        /// returns empty datastructure of the same type
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new TreeAlgorithm();
        }
    }
}
