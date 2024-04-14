using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Laboratornaya2S4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Form1 = this;
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logical LogicObj = new Logical();
            LogicObj.GenerateText(Convert.ToInt32(textBox1.Text), checkBox1.Checked, comboBox1.Text);
            string text = LogicObj.GenerationText;
            if (text == "")
            {
                MessageBox.Show("Словарь данных пустой! Загрузите данные.");
            }
            else
            {
                richTextBox1.Text = text;
                //Количество символов
                label3.Text = LogicObj.NumberOfCharacters(); ;
                //Количество слов
                label4.Text = LogicObj.NumberOfWords();
                //Количество уникальных слов
                label5.Text = LogicObj.UniqueWords();

                int item = 0;
                chart1.Series[0].Points.Clear();
                foreach (var p in LogicObj.CommonWords())
                {
                    if (item < 5)
                    {
                        chart1.Series[0].Points.AddXY(p.Text, p.Number);
                    }
                    item++;
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
