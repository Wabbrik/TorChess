using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class King : Piece
    {
        public King(char color) : base(color)
        {
            this.color = color;
            value = 32000;
        }
        public override char GetPiece()
        {
            return 'K';
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((wrapRow(SrcRow + i) == DestRow) && (wrapCol(SrcCol + j) == DestCol))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
