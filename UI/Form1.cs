using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorChess
{
    public partial class TorchessForm : Form
    {
        static Board myBoard = new Board();
        public PictureBox[,] pictureGrid = new PictureBox[8, 16];
        public TorchessForm()
        {
            InitializeComponent();
            PopulatePictureGrid();
        }

        private void PopulatePictureGrid()
        {
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Col = 0; Col < 16; Col++)
                {
                    int height = panel1.Height / 8;
                    int width = panel1.Width / 16;
                    pictureGrid[Row, Col] = new PictureBox();
                    pictureGrid[Row, Col].Size = new Size(width, height);
                    pictureGrid[Row, Col].Location = new Point(Col * width, Row * height);
                    if ((Col + Row) % 2 == 0) 
                    { 
                        pictureGrid[Row, Col].BackColor = Color.Black; 
                    }
                    else
                    {
                        pictureGrid[Row, Col].BackColor = Color.White;
                    }
                    panel1.Controls.Add(pictureGrid[Row, Col]);
                }
            }

        }

        private void TorchessForm_Load_1(object sender, EventArgs e)
        {
            // PopulatePictureGrid(70);
            //Bitmap bm = new Bitmap(8 * 100, 8 * 100);
            //Graphics g = Graphics.FromImage(bm);
            //Color color1, color2;
            //for (int i = 0; i < 8; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        color1 = Color.Black;
            //        color2 = Color.White;
            //    }
            //    else
            //    {
            //        color1 = Color.White;
            //        color2 = Color.Black;
            //    }
            //    SolidBrush blackBrush = new SolidBrush(color1);
            //    SolidBrush whiteBrush = new SolidBrush(color2);

            //    for (int j = 0; j < 8; j++)
            //    {
            //        if (j % 2 == 0)
            //            g.FillRectangle(blackBrush, i * 100, j * 100, 100, 100);
            //        else
            //            g.FillRectangle(whiteBrush, i * 100, j * 100, 100, 100);
            //    }
            //}

            //g.DrawImage(bm, 150, 200);
            //BackgroundImage = bm;
        }
    }
}
