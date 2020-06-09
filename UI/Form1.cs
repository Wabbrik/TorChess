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
                    pictureGrid[Row, Col].MouseEnter += Picture_Box_MouseEnter;
                    pictureGrid[Row, Col].MouseLeave += Picture_Box_MouseLeave;
                    if ((Col + Row) % 2 == 0)
                    {
                        pictureGrid[Row, Col].BackColor = Color.DarkGreen;
                    }
                    else
                    {
                        pictureGrid[Row, Col].BackColor = Color.Honeydew;
                    }
                    pictureGrid[Row, Col].Tag = pictureGrid[Row, Col].BackColor;
                    panel1.Controls.Add(pictureGrid[Row, Col]);
                }
            }

        }
        private void Picture_Box_MouseLeave(object sender, EventArgs e)
        {
            PictureBox hoveredPicture = (PictureBox)sender;
            hoveredPicture.BackColor = (Color)hoveredPicture.Tag;
        }
        private void Picture_Box_MouseEnter(object sender, EventArgs e)
        {
            PictureBox hoveredPicture = (PictureBox)sender;
            hoveredPicture.BackColor = Color.LightGoldenrodYellow;
        }
    }
}
