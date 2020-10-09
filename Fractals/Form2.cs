using System;
using System.Windows.Forms;

namespace Fractals
{
    public partial class Form2 : Form
    {
        #region Initializing form
        public Form2()
        {
            InitializeComponent();
            panel1.Visible = false;
            timer1.Start();
            timer1.Tick += Timer1_Tick;
        }

        #endregion

        #region Timer Event Tick() raised
        private void Timer1_Tick(object sender, EventArgs e)
        {
            panel1.Visible = true;
            pictureBox1.Visible = false;
            timer2.Start();
            timer2.Tick += Timer2_Tick;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            Form1 f = new Fractals.Form1();
            f.Show();
            this.Hide();
            timer1.Stop();
            timer2.Stop();
        }
        #endregion
    }
}
