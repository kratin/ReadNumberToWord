using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumberToWord
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            IConvert thai = new NumToThaiWord();
            IConvert eng = new NumToEngWord();

            if (radioButton1.Checked)
            {
                label1.Text = readLetter(thai, textBox1.Text);
            }
            else
            {
                label1.Text = readLetter(eng, textBox1.Text);
            }
           
        }
        //***** polymorphism ********//
        private String readLetter(IConvert iConvert,String letter)
        {
            String word = iConvert.changeNumericToWords(letter);
            return word;
        }

       
    }
}
