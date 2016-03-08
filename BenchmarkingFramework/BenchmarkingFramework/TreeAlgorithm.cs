using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class TreeAlgorithm
    {
        public virtual string GetDataType()
        {
            return "TreeAlgorithm";
        }

        /// <summary>
        /// builds the datastructure
        /// </summary>
        /// <param name="array"></param>
        public virtual void Build(int[] array)
        {
            foreach (int i in array)
                Insert(i);
        }
        /// <summary>
        /// inserts one key
        /// </summary>
        /// <param name="key"></param>
        public virtual void Insert(int key)
        {

        }
        /// <summary>
        /// searches and deletes the key
        /// </summary>
        /// <param name="key"></param>
        public virtual bool Delete(int key)
        {
            return false;
        }

        /// <summary>
        /// searches for the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int Lookup( int key)
        {
            return -1;
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
