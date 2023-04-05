using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THA_W6_Cevin
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
        private void btEnter_Click(object sender, EventArgs e)
        {
            if (txtGuess.Text != "")
            {
                Form2 f2 = new Form2();
                f2.Guesses = Convert.ToInt32(txtGuess.Text);
                f2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fill it!!!");
            }
        }
    }
}
