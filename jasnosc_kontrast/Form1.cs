using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace jasnosc_kontrast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        
    
        

        private void button1_Click(object sender, EventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                // label1.Content = openFileDialog.FileName;
                // img1.Source = new BitmapImage(new Uri(openFileDialog.FileName));
               
               Bitmap bm = new Bitmap(Image.FromFile(openFileDialog.FileName));
                Bitmap jasnosc = new Bitmap(Image.FromFile(openFileDialog.FileName));
                int x, y;
                int v1 = 0, v2, v3;
                double s;
                int max = 0;

                int[] tabR = new int[256];
                int[] tabG = new int[256];
                int[] tabB = new int[256];
              
                pictureBox1.Image = bm;
                
               
                label3.Text = trackBar1.Value.ToString();
                for (x = 0; x < bm.Width; x++)
                {
                    for (y = 0; y < bm.Height; y++)
                    {
                        Color pixelColor = bm.GetPixel(x, y);

                       
                        int r = pixelColor.R;
                        int g = pixelColor.G;
                        int b = pixelColor.B;
                        tabR[r]++;
                        tabG[g]++;
                        tabB[b]++;

                       
                        
                        // s = 0.299 * r + 0.587 * g + 0.114 * b;
                        v1 = trackBar1.Value;
                        r += v1;
                        g += v1;
                        b += v1;

                        if (r > 255)
                        {
                            int pomoc;
                            pomoc = r - 255;
                            r = r - pomoc;
                        }
                        else if (r < 0)
                        {
                            int pomoc;
                            pomoc = r - r;
                            r = pomoc;
                        }

                        if (g > 255)
                        {
                            int pomoc;
                            pomoc = g - 255;
                            g = g - pomoc;
                        }
                        else if (g < 0)
                        {
                            int pomoc;
                            pomoc = g - g;
                            g = pomoc;
                        }
                        if (b > 255)
                        {
                            int pomoc;
                            pomoc = b - 255;
                            b = b - pomoc;
                        }
                        else if (b < 0)
                        {
                            int pomoc;
                            pomoc = b - b;
                            b = pomoc;
                        }
                        jasnosc.SetPixel(x, y, Color.FromArgb(r, g, b));

                        pictureBox2.Image = jasnosc;

                        



                    }
                }

                max = tabR.Max();
                for (int i=0; i< tabR.Length;i++)
                {

                }



                        chart1.Series[0].Points.Clear();
                chart2.Series["Green"].Points.Clear();
                chart3.Series["Blue"].Points.Clear();
                chart4.Series["R"].Points.Clear();
                chart4.Series["G"].Points.Clear();
                chart4.Series["B"].Points.Clear();
                for (int i = 0; i < 255; i++)
                {
                    chart1.Series[0].Points.AddXY(i, tabR[i]);
                    chart2.Series["Green"].Points.AddXY(i, tabG[i]);
                    chart3.Series["Blue"].Points.AddXY(i, tabB[i]);
                    chart4.Series["R"].Points.AddXY(i, tabR[i]);
                    chart4.Series["G"].Points.AddXY(i, tabG[i]);
                    chart4.Series["B"].Points.AddXY(i, tabB[i]);
                }
                chart1.Invalidate();

                //kontrast-------------------------------------
                Bitmap bm2  = new Bitmap(Image.FromFile(openFileDialog.FileName));

                double contrast = Math.Pow((100.0 + trackBar2.Value)/100.0,2);
                label4.Text = trackBar2.Value.ToString();
                for(y = 0; y < bm.Height; y++)
                {
                    for(x = 0; x < bm.Width; x++)
                    {
                        Color pixelColor = bm.GetPixel(x, y);
                        double r = ((((pixelColor.R / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                        double g = ((((pixelColor.G / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
                        double b = ((((pixelColor.B / 255.0) - 0.5) * contrast) + 0.5) * 255.0;

                        if (r > 255) r = 255;
                        if (r < 0) r = 0;
                        if (g > 255) g = 255;
                        if (g < 0) g = 0;
                        if (b > 255) b = 255;
                        if (b < 0) b = 0;

                        Color nowy = Color.FromArgb(pixelColor.A, (int)r, (int)g, (int)b);

                        bm2.SetPixel(x, y, nowy);
                        pictureBox3.Image = bm2;
                    }

                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           
        }
    }
}
