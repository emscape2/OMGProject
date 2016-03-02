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
        bool random, sorted, counting, grouped;
        bool thousand, tenK, hundredK, oneMille;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            random = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            sorted = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            counting = checkBox3.Checked;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            grouped = checkBox8.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            thousand = checkBox6.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            tenK = checkBox4.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            hundredK = checkBox7.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            oneMille = checkBox5.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> sizes = new List<int>();
            if (thousand)
            {
                sizes.Add(1000);
            }
            if (tenK)
            {
                sizes.Add(10000);
            }
            if (hundredK)
            {
                sizes.Add(100000);
            }
            if (oneMille)
            {
                sizes.Add(1000000);
            }
            List<ArrayGenerator> gens = new List<ArrayGenerator>();
            if (random)
            {
                gens.Add(new RandomArrayGenerator());
            }
            if (sorted)
            {
                gens.Add(new SortedRandomArrayGenerator());
            }
            if (counting)
            {
                gens.Add(new CountingArrayGenerator());
            }
            if (grouped)
            {
                gens.Add(new GroupedArrayGenerator());
            }
            TreeAlgorithm[] algs = new TreeAlgorithm[1];
            algs[0] = new HashOpenAdressing();
            Benchmark benchmark = new Benchmark(sizes.ToArray(), gens.ToArray(), algs) ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            location = textBox1.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
