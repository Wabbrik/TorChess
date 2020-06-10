using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class InnerPawn : Piece
    {
        public bool bigFirst { get; set; }
        public InnerPawn(char color) : base(color)
        {
            this.color = color;
            bigFirst = false;
            value = 1;
        }
        public override char GetPiece()
        {
            return 'I'; 
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
        {
          
            Piece destPiece = board[DestRow,DestCol];
            if (destPiece == null)
            {
                if (DestRow == SrcRow)
                {
                    if (color == 'w')
                    {
                        if (DestCol == wrapCol(SrcCol + 1))
                        {
                            bigFirst = false;
                            return true;
                        }
                        else
                        {
                            if (SrcCol == 5 && DestCol == 7)
                            {
                                bigFirst = true;
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (DestCol == wrapCol(SrcCol-1))
                        {
                            bigFirst = false;
                            return true;
                        }
                        else
                        {
                            if (SrcCol == 10 && DestCol == 8)
                            {
                                bigFirst = true;
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                if ((DestRow == wrapRow(SrcRow - 1)) || (DestRow == wrapRow(SrcRow + 1)))
                {
                    if (color=='w')
                    {
                        if (DestCol == wrapCol(SrcCol + 1))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (DestCol == wrapCol(SrcCol - 1))
                        {
                            return true;
                        }
                    }
                }
            }
           

            return false;
        }
    }
}
