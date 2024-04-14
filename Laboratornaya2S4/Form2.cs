using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;


namespace Laboratornaya2S4
{
    public partial class Form2 : Form
    {
        private Logical LogicObj;
        public Form1 Form1 { get; set; }
        
        public Form2()
        {
            InitializeComponent();
            LogicObj = new Logical();
            richTextBox1.Text = LogicObj.GetPhrase()[0];
            richTextBox2.Text = LogicObj.GetPhrase()[1];
            richTextBox3.Text = LogicObj.GetPhrase()[2];
            richTextBox4.Text = LogicObj.GetPhrase()[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogicObj.UploadingToAFile(richTextBox1.Text, 1);
            LogicObj.UploadingToAFile(richTextBox2.Text, 2);
            LogicObj.UploadingToAFile(richTextBox3.Text, 3);
            LogicObj.UploadingToAFile(richTextBox4.Text, 4);
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox3_TextChanged(object sender, EventArgs e)
        { 

        }
        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
