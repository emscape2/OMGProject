﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    class TestData
    {
        public string arrayType;
        public int arraySize;
        public double buildTime;
        public double insertTime;
        public double deleteTime;
        public double lookupTime;
        public long memorySize;

        public TestData(string type, int size, double build, double insert, double delete, double lookup, long memSize)
        {
            arrayType = type;
            arraySize = size;
            buildTime = build;
            insertTime = insert;
            deleteTime = delete;
            lookupTime = lookup;
            memorySize = memSize;
        }


    }
}
