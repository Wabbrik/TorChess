using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChess
{
    class Board
    {
        public Piece[,] board = new Piece[8, 16];
        public Board()
        {
            for (int Row = 0; Row < 8; Row++)
            {
                for (int Column = 0; Column < 16; Column++)
                {
                    board[Row, Column] = null;
                }
            }

            // Black pieces

            // White pieces
            
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
    }
}
