using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Fractals
{
    public partial class Form1 : Form
    {
        #region // Defining variables and constants.
        double a;
        double b;
        Bitmap bm;
        private int zoom = 1;
        private const int limit = 200;
        private Size new_Size;
        private Bitmap bm2;
        private Color color1;
        private Color color2;
    
        int picturebox_width = 1366;
        int picturebox_height = 741;
        #endregion

        #region Form properties
        // Initializing Form.
        public Form1()
        {
            InitializeComponent();
            zoomOutToolStripMenuItem.Enabled = false;

        }
        
        public void Form1_Shown(object sender, EventArgs e)
        {
            Calculations(Color.Green , Color.Black);

        }
        #endregion

        #region Functions
        // Random Color Generator
        private Color rcg1()
        {
            Random rand = new Random();
            color1 = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            if (color1 == Color.Black)
            {
                rcg1();
            }
            return color1;
        }

        // Calculations
        private void Calculations(Color color_a, Color color_b)
        {

            bm = new Bitmap(picturebox_width, picturebox_height);

            for (int x = 0; x < 1366; x++)
            {
                for (int y = 0; y < 741; y++)
                {
                    a = (double)(x - (pictureBox1.Width / 2)) / (double)(pictureBox1.Width / 4);
                    b = (double)(y - (pictureBox1.Height / 2)) / (double)(pictureBox1.Height / 4);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);
                    int it = 0;
                    do
                    {
                        it++;
                        z.Square();
                        z.Add(c);
                        if (z.Magnitude() > 2.0) break;
                    }
                    while (it < 100);
                    bm.SetPixel(x, y, it < 100 ? color_a : color_b);

                }
                pictureBox1.Image = bm;
            }
        }
        #endregion

        #region Tool strip nenu programming

        // Menu Strip Initialized

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Bitmap (*.bmp)| *.bmp";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;
                bm.Save(path, ImageFormat.Bmp);
                sfd.Dispose();
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd2 = new SaveFileDialog();
            sfd2.Filter = "Bitmap (*.bmp)| *.bmp | JPEG (*.jpeg)| *.jpeg | PNG (*.png)| *.png | Icon (*.icon)|*.icon | Tiff (*.Tiff)| *.tiff";
            if (sfd2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd2.FileName;
                switch (sfd2.FilterIndex)
                {
                    case 1:
                        bm.Save(path, ImageFormat.Bmp);
                        break;
                    case 2:
                        bm.Save(path, ImageFormat.Jpeg);
                        break;
                    case 3:
                        bm.Save(path, ImageFormat.Png);
                        break;
                    case 4:
                        bm.Save(path, ImageFormat.Icon);
                        break;
                    case 5:
                        bm.Save(path, ImageFormat.Tiff);
                        break;
                }
            }
        }

        private void startAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            timer1.Start();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Powered by Kon/nos Papanagnou. \r\nCopyright © Kon/nos Papanagnou.\r\nFor Technical Issues communicate with me at konstantinos_gr1@yahoo.com", "Help");
        }
        // Timer option
        private void timer1_Tick(object sender, EventArgs e)
        {
            Calculations(Color.Green,Color.Black);
            timer1.Stop();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom += 50;
            new_Size = new Size(bm.Width + zoom, bm.Height + zoom);
            bm2 = new Bitmap(bm, new_Size);
            pictureBox1.Image = bm2;
            if (zoom > 1)
            {
                zoomOutToolStripMenuItem.Enabled = true;
            }
            else if (zoom >= limit)
            {
                zoomInToolStripMenuItem.Enabled = false;
            }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom -= 50;
            new_Size = new Size(bm2.Width - zoom, bm2.Height - zoom);
            bm2 = new Bitmap(bm2, new_Size);
            pictureBox1.Image = bm2;
            if (zoom <= 1)
            {
                zoomOutToolStripMenuItem.Enabled = false;
            }
            else if (zoom <= limit)
            {
                zoomInToolStripMenuItem.Enabled = true;
            }
        }



        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculations(rcg1(), Color.Black);
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculations(Color.Green, Color.Black);
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = false;
            cd.AllowFullOpen = false;
            cd.ShowHelp = true;
            cd.HelpRequest += Cd_HelpRequest;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                color1 = cd.Color;
            }

            ColorDialog cd2 = new ColorDialog();
            cd2.FullOpen = false;
            cd2.AllowFullOpen = false;
            cd2.ShowHelp = true;
            cd2.HelpRequest += Cd2_HelpRequest;
            if (cd2.ShowDialog() == DialogResult.OK)
            {
                color2 = cd2.Color;
            }

            Calculations(color1, color2);
        }

        private void Cd2_HelpRequest(object sender, EventArgs e)
        {
            MessageBox.Show("Choose a color for the shape (Mandelbrot set) of the image", "Shape Color", MessageBoxButtons.OK);
        }

        private void Cd_HelpRequest(object sender, EventArgs e)
        {
            MessageBox.Show("Choose a Color for the background of the image", "Background Color", MessageBoxButtons.OK);
        }

        private void randomByTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Start();
            timer2.Tick += Timer2_Tick;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            Calculations(rcg1(), Color.Black);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
    #endregion 
}
