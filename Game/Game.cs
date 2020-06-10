using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorChess
{
    class Game
    {
        private char playerTurn;
        public Board board = new Board();
        public Game()
        {
            playerTurn = 'w';
        }
        public char GetPlayerTurn()
        {
            return playerTurn;
        }
        private void AlternateTurn() 
        {
            playerTurn = (playerTurn == 'w') ? 'b' : 'w';
        } 
        public bool MakeMove(int srcRow, int srcCol, int destRow, int destCol)
        {
            if(board.board[srcRow, srcCol] != null && board.board[srcRow, srcCol].color == playerTurn)
            {
                if(board.board[srcRow, srcCol].IsLegalMove(srcRow, srcCol, destRow, destCol, board.board))
                {
                    Piece tempPiece = board.board[destRow, destCol];
                    board.board[destRow, destCol] = board.board[srcRow, srcCol];
                    board.board[srcRow, srcCol] = null;
                    if (!board.IsInCheck(playerTurn))
                    {
                        AlternateTurn();
                        return true;
                    }
                    else
                    {
                        board.board[srcRow, srcCol] = board.board[destRow, destCol];
                        board.board[destRow, destCol] = tempPiece;
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
