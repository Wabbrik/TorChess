using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TorChess.Common;

namespace TorChess
{
    public class Board
    {
        public Piece[,] board = new Piece[8, 16];
        public Board()
        {
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Col = 0; Col < 16; Col++)
                {
                    board[Row, Col] = null;
                }
            }

            // black inner pawns
            for (int Row = 0; Row < 8; Row++)
            {
                board[Row, 10] = new InnerPawn('b');
            }
            // black outer pawns
            for (int Row = 0; Row < 8; Row++)
            {
                board[Row, 13] = new OuterPawn('b');
            }

            //black pieces
            board[0, 11] = new Rook('b');
            board[0, 12] = new Rook('b');
            board[7, 11] = new Rook('b');
            board[7, 12] = new Rook('b');

            board[1, 11] = new Knight('b');
            board[1, 12] = new Knight('b');
            board[6, 11] = new Knight('b');
            board[6, 12] = new Knight('b');

            board[2, 11] = new Bishop('b');
            board[2, 12] = new Bishop('b');
            board[5, 11] = new Bishop('b');
            board[5, 12] = new Bishop('b');

            board[3, 12] = new Queen('b');
            board[4, 12] = new Queen('b');

            board[3, 11] = new General('b');
            board[4, 11] = new King('b');

            // white inner pawns
            for (int Row = 0; Row < 8; Row++)
            {
                board[Row, 5] = new InnerPawn('w');
            }
            // white outer pawns
            for (int Row = 0; Row < 8; Row++)
            {
                board[Row, 2] = new OuterPawn('w');
            }
            //white pieces
            board[0, 3] = new Rook('w');
            board[0, 4] = new Rook('w');
            board[7, 3] = new Rook('w');
            board[7, 4] = new Rook('w');

            board[1, 3] = new Knight('w');
            board[1, 4] = new Knight('w');
            board[6, 3] = new Knight('w');
            board[6, 4] = new Knight('w');

            board[2, 3] = new Bishop('w');
            board[2, 4] = new Bishop('w');
            board[5, 3] = new Bishop('w');
            board[5, 4] = new Bishop('w');

            board[3, 3] = new Queen('w');
            board[4, 3] = new Queen('w');

            board[3, 4] = new General('w');
            board[4, 4] = new King('w');
        }
        public bool IsInCheck(char color)
        {
            // find KingPiece
            int KingRow = 0;
            int KingCol = 0;
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Col = 0; Col < 16; Col++)
                {
                    if (board[Row, Col] != null)
                    {
                        if (board[Row, Col].color == color)
                        {
                            if (board[Row, Col].GetPiece() == 'K')
                            {
                                KingRow = Row;
                                KingCol = Col;
                            }
                        }
                    }
                }
            }
            // iterate trough opponent's pieces to see if any of them are attacking the KingPiece
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Column = 0; Column < 16; Column++)
                {
                    if (board[Row, Column] != null)
                    {
                        if (board[Row, Column].color != color)
                        {
                            if (board[Row, Column].IsLegalMove(Row, Column, KingRow, KingCol, board))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool isDraw()
        {
            if (!this.IsInCheck('w') && !this.BoardCanMove('w'))
            {
                return true;
            }
            else if (!this.IsInCheck('b') && !this.BoardCanMove('b'))
            {
                return true;
            }
            return false;
        }
        public bool IsMate(char color)
        {
            if (this.IsInCheck(color) && !this.BoardCanMove(color))
            {
                return true;
            }
            return false;
        }
        public bool BoardCanMove(char color)
        {
            // iterate trough all pieces
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Column = 0; Column < 16; Column++)
                {
                    if (board[Row, Column] != null)
                    {
                        //does the piece belong to the player? Check if it is a valid move
                        if (board[Row, Column].color == color)
                        {
                            for (int MoveRow = 0; MoveRow < 8; MoveRow++)
                            {
                                for (int MoveColumn = 0; MoveColumn < 16; MoveColumn++)
                                {
                                    if (board[Row, Column].IsLegalMove(Row, Column, MoveRow, MoveColumn, board))
                                    {
                                        // make the move, after that, check if the KingPiece is in check
                                        Piece bTemp = board[MoveRow, MoveColumn];
                                        board[MoveRow, MoveColumn] = board[Row, Column];
                                        board[Row, Column] = null;
                                        bool bCanMove = !IsInCheck(color); // check if the KingPiece is in check after the move
                                                                           // redo move
                                        board[Row, Column] = board[MoveRow, MoveColumn];
                                        board[MoveRow, MoveColumn] = bTemp;
                                        if (bCanMove)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public int GetBoardScore()
        {
            int scoreW = 0;
            int scoreB = 0;
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Column = 0; Column < 16; Column++)
                {
                    if (board[Row, Column] != null)
                    {
                        if (board[Row, Column].color == 'w') scoreW += board[Row, Column].value;
                        else scoreB += board[Row, Column].value;
                    }
                }
            }
            return scoreW - scoreB;
        }
        public List<Tuple<Board, int>> BoardValidStates(Board b, char color) 
        {
            List<Tuple<Board, int>> validMoves = new List<Tuple<Board, int>>();
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Column = 0; Column < 16; Column++)
                {
                    if (b.board[Row, Column] != null)
                    {
                        if (b.board[Row, Column].color == color)
                        {
                            for (int MoveRow = 0; MoveRow < 8; MoveRow++)
                            {
                                for (int MoveColumn = 0; MoveColumn < 16; MoveColumn++)
                                {
                                    if (b.board[Row, Column].IsLegalMove(Row, Column, MoveRow, MoveColumn, board))
                                    {
                                        Piece bTemp = b.board[MoveRow, MoveColumn];
                                        b.board[MoveRow, MoveColumn] = b.board[Row, Column];
                                        b.board[Row, Column] = null;
                                        bool bCanMove = !IsInCheck(color);
                                        b.board[Row, Column] = b.board[MoveRow, MoveColumn];
                                        b.board[MoveRow, MoveColumn] = bTemp;
                                        if (bCanMove)
                                        {
                                            Tuple<Board, int> temp = new Tuple<Board, int>(b, b.GetBoardScore());
                                            validMoves.Add(temp);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //return validMoves.OrderBy(x => x.GetBoardScore(x)).ToList();
            return validMoves;
        }
        //public int minMaxAb(Board b, int depth, bool white, int alpha = int.MinValue, int beta = int.MaxValue)
        //{
        //    if(depth == 0)
        //    return 1;
        //}
    }
}
