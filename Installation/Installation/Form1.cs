using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nextButtonClick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show("Succes", "Warning");
                Form2 Form = new Form2();
                Form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You should agree", "Error");
            }
        }

        private void exitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
