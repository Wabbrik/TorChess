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
        // cached image paths
        static string w_p = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_p.png"));
        static string b_p = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_p.png"));
        static string w_q = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_q.png"));
        static string b_q = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_q.png"));
        static string w_b = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_b.png"));
        static string b_b = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_b.png"));
        static string w_r = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_r.png"));
        static string b_r = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_r.png"));
        static string w_g = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_g.png"));
        static string b_g = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_g.png"));
        static string w_k = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_k.png"));
        static string b_k = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_k.png"));
        static string w_n = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\w_n.png"));
        static string b_n = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\src\\b_n.png"));

        //cached pictures
        static Image w_pImage = Image.FromFile(w_p);
        static Image b_pImage = Image.FromFile(b_p);
        static Image w_qImage = Image.FromFile(w_q);
        static Image b_qImage = Image.FromFile(b_q);
        static Image w_bImage = Image.FromFile(w_b);
        static Image b_bImage = Image.FromFile(b_b);
        static Image w_rImage = Image.FromFile(w_r);
        static Image b_rImage = Image.FromFile(b_r);
        static Image w_gImage = Image.FromFile(w_g);
        static Image b_gImage = Image.FromFile(b_g);
        static Image w_kImage = Image.FromFile(w_k);
        static Image b_kImage = Image.FromFile(b_k);
        static Image w_nImage = Image.FromFile(w_n);
        static Image b_nImage = Image.FromFile(b_n);

        static Game myGame = new Game();
        public PictureBox[,] pictureGrid = new PictureBox[8, 16];
        public TorchessForm()
        {
            InitializeComponent();
            PopulatePictureGrid();
            DrawBoard(myGame.board);
        }
        private void PopulatePictureGrid()
        {
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Col = 0; Col < 16; Col++)
                {
                    int height = panel1.Height >> 3;
                    int width = panel1.Width >> 4;
                    pictureGrid[Row, Col] = new PictureBox();
                    pictureGrid[Row, Col].Size = new Size(width, height);
                    pictureGrid[Row, Col].Location = new Point(Col * width + 1, Row * height + 1);
                    pictureGrid[Row, Col].MouseHover += Picture_Box_MouseHover;
                    pictureGrid[Row, Col].MouseEnter += Picture_Box_MouseEnter;
                    pictureGrid[Row, Col].MouseLeave += Picture_Box_MouseLeave;
                    pictureGrid[Row, Col].MouseDown += Picture_Box_MouseDown;
                    pictureGrid[Row, Col].MouseUp += Picture_Box_MouseUp;
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
        }
        public static int newX, newY;
        private void Picture_Box_MouseUp(object sender, MouseEventArgs e)
        {
            newX = panel1.PointToClient(Cursor.Position).X / (panel1.Width >> 4);
            newY = panel1.PointToClient(Cursor.Position).Y / (panel1.Height >> 3);
            if(myGame.MakeMove(currentY, currentX, newY, newX)) 
            {
                pictureGrid[newY, newX].Image = pictureGrid[currentY, currentX].Image;
                pictureGrid[currentY, currentX].Image = null;
                if (myGame.board.IsMate(myGame.GetPlayerTurn())) { MessageBox.Show("Ai castigat!"); }
            }
        }

        public static int currentX, currentY;
        private void Picture_Box_MouseDown(object sender, MouseEventArgs e)
        {
            currentX = panel1.PointToClient(Cursor.Position).X / (panel1.Width >> 4);
            currentY = panel1.PointToClient(Cursor.Position).Y / (panel1.Height >> 3);
        }
        private void Picture_Box_MouseHover(object sender, EventArgs e)
        {
            PictureBox hoveredPicture = (PictureBox)sender;
            int Colx = panel1.PointToClient(Cursor.Position).X / (panel1.Width >> 4);
            int Rowy = panel1.PointToClient(Cursor.Position).Y / (panel1.Height >> 3);
            if (myGame.board.board[Rowy, Colx] != null)
            {
                for (int Row = 0; Row < 8; Row++)
                {
                    for (int Col = 0; Col < 16; Col++)
                    {
                        if (!myGame.board.board[Rowy, Colx].IsLegalMove(Rowy, Colx, Row, Col, myGame.board.board) || 
                            myGame.board.board[Rowy, Colx].color != myGame.GetPlayerTurn())
                        {
                            continue;
                        }
                        if (pictureGrid[Row, Col].Image == null)
                            pictureGrid[Row, Col].BackColor = Color.CornflowerBlue;
                        else
                            pictureGrid[Row, Col].BackColor = Color.DarkBlue;
                    }
                }
            }
        }
        private void Picture_Box_MouseLeave(object sender, EventArgs e)
        {
            PictureBox hoveredPicture = (PictureBox)sender;

            if (hoveredPicture.Image != null)
            {
                for (int Row = 0; Row < 8; Row++)
                {
                    for (int Col = 0; Col < 16; Col++)
                    {
                        if (pictureGrid[Row, Col].BackColor != (Color)pictureGrid[Row, Col].Tag)    //optimized
                            pictureGrid[Row, Col].BackColor = (Color)pictureGrid[Row, Col].Tag;
                    }
                }
            }
            hoveredPicture.BackColor = (Color)hoveredPicture.Tag;
        }
        private void Picture_Box_MouseEnter(object sender, EventArgs e)
        {
            PictureBox hoveredPicture = (PictureBox)sender;
            if (hoveredPicture.Image == null)
                hoveredPicture.BackColor = Color.CornflowerBlue;
        }
        public void DrawBoard(Board myBoard)
        {
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
                                    pictureGrid[Row, Col].Image = w_pImage;
                                    break;
                                case 'O':
                                    pictureGrid[Row, Col].Image = w_pImage;
                                    break;
                                case 'Q':
                                    pictureGrid[Row, Col].Image = w_qImage;
                                    break;
                                case 'B':
                                    pictureGrid[Row, Col].Image = w_bImage;
                                    break;
                                case 'K':
                                    pictureGrid[Row, Col].Image = w_kImage;
                                    break;
                                case 'N':
                                    pictureGrid[Row, Col].Image = w_nImage;
                                    break;
                                case 'R':
                                    pictureGrid[Row, Col].Image = w_rImage;
                                    break;
                                case 'G':
                                    pictureGrid[Row, Col].Image = w_gImage;
                                    break;
                            }
                        }
                        else
                        {
                            switch (myBoard.board[Row, Col].GetPiece())
                            {
                                case 'I':
                                    pictureGrid[Row, Col].Image = b_pImage;
                                    break;
                                case 'O':
                                    pictureGrid[Row, Col].Image = b_pImage;
                                    break;
                                case 'Q':
                                    pictureGrid[Row, Col].Image = b_qImage;
                                    break;
                                case 'B':
                                    pictureGrid[Row, Col].Image = b_bImage;
                                    break;
                                case 'K':
                                    pictureGrid[Row, Col].Image = b_kImage;
                                    break;
                                case 'N':
                                    pictureGrid[Row, Col].Image = b_nImage;
                                    break;
                                case 'R':
                                    pictureGrid[Row, Col].Image = b_rImage;
                                    break;
                                case 'G':
                                    pictureGrid[Row, Col].Image = b_gImage;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
