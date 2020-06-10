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
            if (SrcRow == DestRow)
            {
                int ColOffset = (DestCol - SrcCol > 0) ? 1 : -1;
                for (int CheckCol = wrapCol(SrcCol + ColOffset); CheckCol != DestCol; CheckCol = wrapCol(CheckCol + ColOffset))
                {
                    if (board[SrcRow, CheckCol] != null)
                    {
                        ColOffset *= -1;
                        for (CheckCol = wrapCol(SrcCol + ColOffset); CheckCol != DestCol; CheckCol = wrapCol(CheckCol + ColOffset))
                        {
                            if (board[SrcRow, CheckCol] != null)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                return true;
            }
            else if (DestCol == SrcCol)
            {
                int RowOffset = (DestRow - SrcRow > 0) ? 1 : -1;
                for (int CheckRow = wrapRow(SrcRow + RowOffset); CheckRow != DestRow; CheckRow = wrapRow(CheckRow + RowOffset))
                {
                    if (board[CheckRow, SrcCol] != null)
                    {
                        RowOffset *= -1;
                        for (CheckRow = wrapRow(SrcRow + RowOffset); CheckRow != DestRow; CheckRow = wrapRow(CheckRow + RowOffset))
                        {
                            if (board[CheckRow, SrcCol] != null)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                return true;
            }
            else if ((DestCol - SrcCol == DestRow - SrcRow) || (DestCol - SrcCol == SrcRow - DestRow))
            {
                // make sure there aren't pieces in-between
                int RowOffset = (DestRow - SrcRow > 0) ? 1 : -1;
                int ColOffset = (DestCol - SrcCol > 0) ? 1 : -1;
                for (int CheckRow = SrcRow + RowOffset, CheckCol = SrcCol + ColOffset;
                    CheckRow != DestRow;
                    CheckRow = wrapRow(CheckRow + RowOffset), CheckCol = wrapCol(CheckCol + ColOffset))
                {
                    if (board[CheckRow, CheckCol] != null)
                    {
                        // change direction
                        RowOffset *= -1;
                        ColOffset *= -1;
                        for (CheckRow = wrapRow(SrcRow + RowOffset), CheckCol = wrapCol(SrcCol + ColOffset);
                            CheckRow != DestRow;
                            CheckRow = wrapRow(CheckRow + RowOffset), CheckCol = wrapCol(CheckCol + ColOffset))
                        {
                            if (board[CheckRow, CheckCol] != null)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
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
