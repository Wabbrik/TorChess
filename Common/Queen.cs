using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class Queen : Piece
    {
        public Queen(char color) : base(color)
        {
            this.color = color;
            value = 13;
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
        {
            Bishop b = new Bishop(this.color);
            Rook r = new Rook(this.color);
            bool rCanMove = r.CanMove(SrcRow, SrcCol, DestRow, DestCol, board);
            bool bCanMove = b.CanMove(SrcRow, SrcCol, DestRow, DestCol, board);

            if (rCanMove || bCanMove)
            {
                return true;
            }
            return false;
        }

        public override char GetPiece()
        {
            return 'Q';
        }
    }
}
