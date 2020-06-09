using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                    pictureGrid[Row, Col].MouseHover += Picture_Box_MouseHover;
                    pictureGrid[Row, Col].SizeMode = PictureBoxSizeMode.Zoom;
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
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Col = 0; Col < 16; Col++)
                {
                    if (myBoard.board[Row, Col] != null)
                    {
                        if (myBoard.board[Row, Col].color == 'w')
                        {
                            switch (myBoard.board[Row, Col].GetPiece())
                            {
                                case 'I':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_p.png")));
                                    break;
                                case 'O':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_p.png")));
                                    break;
                                case 'Q':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_q.png")));
                                    break;
                                case 'B':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_b.png")));
                                    break;
                                case 'K':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_k.png")));
                                    break;
                                case 'N':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_n.png")));
                                    break;
                                case 'R':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_r.png")));
                                    break;
                                case 'G':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_g.png")));
                                    break;
                            }
                        }
                        else
                        {
                            switch (myBoard.board[Row, Col].GetPiece())
                            {
                                case 'I':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_p.png")));
                                    break;
                                case 'O':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_p.png")));
                                    break;
                                case 'Q':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_q.png")));
                                    break;
                                case 'B':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_b.png")));
                                    break;
                                case 'K':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_k.png")));
                                    break;
                                case 'N':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_n.png")));
                                    break;
                                case 'R':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_r.png")));
                                    break;
                                case 'G':
                                    pictureGrid[Row, Col].Image = Image.FromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_g.png")));
                                    break;
                            }
                        }
                    }
                }
            }

        }

        private void Picture_Box_MouseHover(object sender, EventArgs e)
        {
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Col = 0; Col < 16; Col++)
                {
                    pictureGrid[Row, Col].BackColor = (Color)pictureGrid[Row, Col].Tag;
                }
            }
                    int Colx = panel1.PointToClient(Cursor.Position).X / (panel1.Width / 16);
            int Rowy = panel1.PointToClient(Cursor.Position).Y / (panel1.Height / 8);
            if (myBoard.board[Rowy, Colx] != null)
            {
                for (int Row = 0; Row < 8; Row++)
                {
                    for (int Col = 0; Col < 16; Col++)
                    {
                        if(myBoard.board[Rowy, Colx].IsLegalMove(Rowy,Colx,Row, Col, myBoard.board))
                        {
                            pictureGrid[Row, Col].BackColor = Color.OrangeRed;
                        }
                    }
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
            hoveredPicture.BackColor = Color.Red;
        }
    }
}
