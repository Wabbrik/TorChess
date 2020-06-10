using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess.Common
{
    class Rook : Piece
    {
        public Rook(char color) : base(color)
		{
			this.color = color;
			value = 7;
		}
		public override char GetPiece()
        {
            return 'R';
        }
        public override bool CanMove(int SrcRow, int SrcCol, int DestRow, int DestCol, Piece[,] board)
		{
			if (SrcRow == DestRow)
			{
				int ColOffset = (DestCol - SrcCol > 0) ? 1 : -1;
				for (int CheckCol = wrapCol(SrcCol + ColOffset); CheckCol != DestCol; CheckCol = wrapCol(CheckCol + ColOffset)) 
				{
					if (board[SrcRow,CheckCol] != null)
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
					if (board[CheckRow,SrcCol] != null)
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
			return false;
		}
	}
}
