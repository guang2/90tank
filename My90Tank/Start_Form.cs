using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My90Tank
{
    public partial class Start_Form : Form
    {
        private int type=0;
        public Start_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(type);
            if (type != 0)
            {
                this.Hide();
                form1.ShowDialog();
                if(form1.DialogResult==System.Windows.Forms.DialogResult.Cancel)
                    this.Close();
            }
            else MessageBox.Show("请选择坦克，再开始游戏");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            type = 1;
            this.label4.Text = "轻型坦克";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            type = 2;
            this.label4.Text = "重型坦克";
        }


    }
}
