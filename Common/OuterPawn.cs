using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class OuterPawn : Piece
    {
        public bool bigFirst { get; set; }
        public OuterPawn(char color) : base(color)
        {
            this.color = color;
            bigFirst = false;
        }
        public override char GetPiece()
        {
            return 'O'; 
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
        {
            Piece destPiece = board[DestRow, DestCol];
            if (destPiece == null)
            {
                //move(BY COLOR)
            }
            else
            {
                //capture + enPassant(BY COLOR)
            }
            return false;
        }
    }
}
