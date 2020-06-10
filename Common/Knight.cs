using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class Knight : Piece
    {
        public Knight(char color) : base(color)
        {
            this.color = color;
            value = 4;
        }
        public override char GetPiece()
        {
            return 'N';
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
        {
            if ((wrapCol(SrcCol + 1) == DestCol) || (wrapCol(SrcCol - 1) == DestCol))
            {
                if ((wrapRow(SrcRow + 2) == DestRow) || (wrapRow(SrcRow - 2) == DestRow))
                {
                    return true;
                }
            }
            if ((wrapCol(SrcCol + 2) == DestCol) || (wrapCol(SrcCol - 2) == DestCol))
            {
                if ((wrapRow(SrcRow + 1) == DestRow) || (wrapRow(SrcRow - 1) == DestRow))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
