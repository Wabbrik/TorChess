using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess
{
    public abstract class Piece
    {
        public int wrapCol(int ix)
        {
            ix %= 16;
            if (ix < 0) return ix + 16;

            return ix;
        }
        public int wrapRow(int iy)
        {
            iy %= 8;
            if (iy < 0) return iy + 8;

            return iy;
        }
        public int value { get; set; }
        public char color { get; set; }
        public abstract bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board);
        public abstract char GetPiece();
        public bool IsLegalMove(int SrcRow, int SrcColumn, int DestRow, int DestColumn, Piece[,] Board)
        {
            if (null == Board[DestRow, DestColumn] || this.color != Board[DestRow, DestColumn].color)
            {
                return CanMove(SrcRow, SrcColumn, DestRow, DestColumn, Board);
            }
            return false;
        }
        public Piece(char color)
        {
            this.color = color;
        }
    }
}
