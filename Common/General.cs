using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class General : Piece
    {
        public General(char color) : base(color)
        {
            this.color = color;
            value = 16;
        }
        public override char GetPiece()
        {
            return 'G';
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
        {
            Knight k = new Knight(this.color);
            Queen q = new Queen(this.color);
            bool qCanMove = q.CanMove(SrcRow, SrcCol, DestRow, DestCol, board);
            bool kCanMove = k.CanMove(SrcRow, SrcCol, DestRow, DestCol, board);

            if (qCanMove || kCanMove)
            {
                return true;
            }
            return false;
        }
    }
}

