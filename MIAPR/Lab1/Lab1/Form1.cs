using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        int[] color = new int[100];
        bool flag = true;
        int[,] dots = new int[100, 2];
        int number = 100;
        int[,] clusters = { { 30, 10}, { 40, 70 }, { 80, 10 } };
  
        private int Minimum(int x, int y)
        {
            int a, b, cluster = 0;
            double res, min = 1000000;
            int len = clusters.Length / 2;

            for (int i = 0; i < len; i++)
            {
                
                a = Math.Abs(clusters[i, 0] - x);
                b = Math.Abs(clusters[i, 1] - y);
                res = Math.Sqrt(a * a + b * b);
                if (res < min)
                {
                    min = res;
                    cluster = i;
                }
            }
            return cluster;
        }

        public Form1()
        {
            InitializeComponent();
            int len = clusters.Length / 2;
            Random rnd = new Random();

            for (int i = 0; i < len; i++)
            {
                clusters[i, 0] = rnd.Next(0, 100);
                clusters[i, 1] = rnd.Next(0, 100);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int clusterNumber = 0;
            int x, y;

            for (int i = 0; i < 3; i++)
            {
                chart1.Series[3].Points.AddXY(clusters[i, 0], clusters[i, 1]);
            }

            for (int i = 0; i < clusters.Length / 2; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            for (int i = 0; i < number; i++)
            {
                x = rnd.Next(0, 100);
                y = rnd.Next(0, 100);
                dots[i, 0] = x;
                dots[i, 1] = y;
                clusterNumber = Minimum(x, y);
                chart1.Series[clusterNumber].Points.AddXY(x, y);
                color[i] = clusterNumber;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numb = 0;
            while (flag == true)
            {
                numb++;
                int[] color2 = new int[100];
                int sum1, sum2;
                int numberOfDots;
                for (int i = 0; i < clusters.Length / 2; i++)
                {
                    sum1 = 0;
                    sum2 = 0;
                    numberOfDots = chart1.Series[i].Points.Count;
                    for (int j = 0; j < numberOfDots; j++)
                    {
                        sum1 += (int)Math.Round(chart1.Series[i].Points[j].XValue);
                        sum2 += (int)Math.Round(chart1.Series[i].Points[j].YValues.FirstOrDefault());
                    }
                    clusters[i, 0] = sum1 / numberOfDots;
                    clusters[i, 1] = sum2 / numberOfDots;
                    chart1.Series[i].Points.Clear();
                }
                chart1.Series[3].Points.Clear();

                for (int i = 0; i < 3; i++)
                {
                    chart1.Series[3].Points.AddXY(clusters[i, 0], clusters[i, 1]);
                }

                for (int i = 0; i < number; i++)
                {
                    int clusterNumber = Minimum(dots[i, 0], dots[i, 1]);
                    chart1.Series[clusterNumber].Points.AddXY(dots[i, 0], dots[i, 1]);
                    color2[i] = clusterNumber;
                }
                int k = 0;
                for (int i = 0; i < number; i++)
                {
                    if (color[i] == color2[i])
                    {
                        k++;
                    }
                }
                if ((k == 100) || (numb == 100))
                {
                    break;
                }
            }
        }
    }
}
