using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenchmarkingFramework
{
    public partial class Form1 : Form
    {
        public static string location;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] sizes = new int[] {
                1000,
                10000,
                100000,
                215000,
                464000,
                1000000
            };
            ArrayGenerator[] gens = new ArrayGenerator[] {
                new RandomArrayGenerator(),
                new SortedRandomArrayGenerator(),
                new CountingArrayGenerator(),
                new GroupedArrayGenerator()
            };
            TreeAlgorithm[] algs = new TreeAlgorithm[] {
                new HashOpenAddressing(),
                new HashChaining(),
                new AVLTree(),
                //new RBTree()
                //new LinkedList()
            };
            Benchmark benchmark = new Benchmark(sizes, gens, algs) ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            location = textBox1.Text;
        }
    }
}
