using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Тредс_Сортировки
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void lst_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(listBox2.Items[e.Index].ToString(), listBox2.Font, listBox2.Width).Height;
        }


        private static void Quick_Sort(int[] arr, int left, int right, int peres3)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right, peres3);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1, peres3);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right, peres3);
                }
            }

        }
        private static int Partition(int[] arr, int left, int right, int peres3)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    peres3 += 1;


                }
                else
                {
                   
                    return right;
                }
            }
        }
        delegate void RenderArray(ListBox listbox, int[] arr, ListBox listBox_2, string time);
        private void button1_Click(object sender, EventArgs e)
        {
           
          
            listBox2.HorizontalScrollbar = true; // Горизонтальный скролл
            listBox2.ScrollAlwaysVisible = true; // Вертикальный скролл
            listBox3.HorizontalScrollbar = true;
            listBox4.HorizontalScrollbar = true;
            listBox5.HorizontalScrollbar = true;//listBox2.WordWrap = true;
            int N = 10;
            N = Convert.ToInt32(textBox1.Text);
            int[] m0 = new int[N];
            int[] mas10 = new int[N];
            int[] mas20 = new int[N];
            int[] mas30 = new int[N];
            Random rand = new Random();
            int peres1 = 0;
            int peres2 = 0;
            int peres3 = 0;
            for (int i = 0; i < m0.Length; i++)
            {
                int x = rand.Next(1000);
                m0[i] = x;
                mas10[i] = x;
                mas20[i] = x;
                mas30[i] = x;
            }
            int[] m = m0.Distinct().ToArray();
            int[] mas1 = mas10.Distinct().ToArray();
            int[] mas2 = mas20.Distinct().ToArray();
            int[] mas3 = mas30.Distinct().ToArray();
            //label1.Text = string.Join(" ", m);
            listBox2.Items.Add(string.Join(" ", m));

            Stopwatch stopWatch = new Stopwatch();
            Stopwatch stopWatch2 = new Stopwatch();
            Stopwatch stopWatch3 = new Stopwatch();

            Thread Bubble = new System.Threading.Thread(() => {



                //Bubble
                stopWatch.Start();
                for (int i = 0; i < mas1.Length; i++)
                {
                    for (int j = 0; j < mas1.Length - 1; j++)
                    {
                        if (mas1[j] > mas1[j + 1])
                        {
                            int t = mas1[j + 1];
                            mas1[j + 1] = mas1[j];
                            mas1[j] = t;
                            peres1 += 1;
                        }
                    }
                }
                
                string s = Convert.ToString(stopWatch.ElapsedMilliseconds);
                stopWatch.Stop();
                renderArrayToListBox(listBox3, mas1, listBox8, s);
            }

            );
            Thread Input = new System.Threading.Thread(() =>
            {
               
                stopWatch2.Start();
                int u=0;
                for (int i = 1; i < 1000000; i++)
                {
                    u += 1000;
                }
                for (int i = 1; i < mas2.Length; i++)
                {
                    int k = mas2[i];
                    int j = i - 1;

                    while (j >= 0 && mas2[j] > k)
                    {
                        mas2[j + 1] = mas2[j];
                       
                        j--;
                        
                    }
                    mas2[j + 1] = k;
                    peres2 += 1;
                }
                string s1 = Convert.ToString(stopWatch2.ElapsedMilliseconds);
                stopWatch2.Stop();
                renderArrayToListBox(listBox4, mas2, listBox6, s1);

            });


            Thread Shell = new System.Threading.Thread(() =>
            {

                Quick_Sort(mas3, 0, mas3.Length - 1, peres3);
                string s2 = Convert.ToString(stopWatch3.ElapsedMilliseconds);
                stopWatch3.Stop();
                renderArrayToListBox(listBox5, mas3, listBox7, s2);
            });
           
            
            Bubble.Start();
            Input.Start();    
            listBox4.Items.Add(string.Join(" ", mas2));
            label22.Text = peres2.ToString();
            listBox7.Items.Add(stopWatch2.ElapsedMilliseconds);
            stopWatch2.Stop();
            stopWatch3.Start();
            Shell.Start();
            listBox5.Items.Add(string.Join(" ", mas3));
            listBox8.Items.Add(stopWatch3.ElapsedMilliseconds);
            label24.Text = peres3.ToString();
            stopWatch3.Stop();
        }
        public void renderArrayToListBox(ListBox listBox, int[] ar, ListBox listBox_2, string time)
        {
            if (listBox.InvokeRequired)
            {
                RenderArray delForRrenderArrayToListBox = new RenderArray(renderArrayToListBox);
                listBox.Invoke(delForRrenderArrayToListBox, new object[] { listBox, ar , listBox_2, time});
            }
            else
            {
                listBox.Items.Clear();
                listBox.Items.Add(string.Join(" ", ar));
                listBox_2.Items.Add(time);
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Text.Length > 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label18.Visible = false;
            label20.Visible = false;
            label22.Visible = false; 
            label24.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Text.Length > 0 && Regex.IsMatch(textBox1.Text, @"^\d+$");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Load(null, EventArgs.Empty);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            textBox1.Text = ""; label20.Text = ""; label22.Text = ""; label24.Text = "";
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

