using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

namespace csoportositas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            open();
            listBox2.AutoSize = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("", "név hozzáadása");
            if (input == "")
            {
                MessageBox.Show("a név mező nem lehet üres", "hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                listBox1.Items.Add(input);
                save();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("a lista üres", "figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult result = MessageBox.Show("biztosan törli?", "megerősítés", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    save();
                }
            }
        }
        void save()
        {
            string file = "names.txt";
            StreamWriter sw = File.CreateText(file);
            foreach (string item in listBox1.Items)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }
        void open()
        {
            string file = "names.txt";
            if (File.Exists(file))
            {
                listBox1.Items.Clear();
                StreamReader sr = File.OpenText(file);
                while (!sr.EndOfStream)
                {
                    listBox1.Items.Add(sr.ReadLine());
                }
                sr.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            int num = 0;
            if (radioButton1.Checked)
            {
                num = int.Parse(Math.Floor(decimal.Parse(listBox1.Items.Count.ToString()) / numericUpDown1.Value).ToString());
            }
            else if (radioButton2.Checked)
            {
                num = int.Parse(Math.Floor(numericUpDown1.Value).ToString());
            }
            int row = int.Parse(Math.Floor(decimal.Parse(listBox1.Items.Count.ToString()) / num).ToString());
            int remain = listBox1.Items.Count % int.Parse(numericUpDown1.Value.ToString());
            if (remain != 0) { row++; }
            try
            {
                listBox2.Items.Clear();
                for (int i = 0; i < row; i++)
                {
                    listBox2.Items.Add("");
                    for (int j = 0; j < num; j++)
                    {
                        int k = new Random().Next(listBox1.Items.Count);
                        if (j != 0 && listBox1.Items[0].ToString() != null)
                        {
                            listBox2.Items[i] = listBox2.Items[i].ToString() + " / ";
                        }
                        listBox2.Items[i] = listBox2.Items[i].ToString() + listBox1.Items[k].ToString();
                        listBox1.Items.RemoveAt(k);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            open();
            listBox2.Size = new Size(0, listBox2.Size.Height);
            listBox2.Visible = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            GC.Collect();
        }
    }
}
