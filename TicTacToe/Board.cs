using System;

namespace TicTacToe
{
    public class Board
    {
        private readonly BoardMarkerType[,] board;

        public int Height
        {
            get
            {
                return this.board.GetUpperBound(1) + 1;
            }
        }
        public int Width
        {
            get
            {
                return this.board.GetUpperBound(0) + 1;
            }
        }

        public Board(int width, int height)
        {
            this.board = new BoardMarkerType[width, height];

            ClearBoard();
        }

        public Board(Board other)
        {
            this.board = new BoardMarkerType[other.Width, other.Height];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    this.board[j, i] = other.GetMarkAtPosition(j, i);
                }
            }
        }

        public void ClearBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    this.board[i, j] = BoardMarkerType.Empty;
                }
            }

            Draw();
        }

        public bool MarkBoard(Play play)
        {
            if (play.PositionX >= Width || play.PositionX < 0 ||
                play.PositionY >= Height || play.PositionY < 0)
            {
                return false;
            }

            if (this.board[play.PositionX, play.PositionY] != BoardMarkerType.Empty)
                return false;

            this.board[play.PositionX, play.PositionY] = play.PlayerId;

            Draw();

            return true;
        }

        public BoardMarkerType[,] GetBoard()
        {
            return this.board;
        }

        public BoardMarkerType GetMarkAtPosition(int x, int y)
        {
            return this.board[x, y];
        }

        public bool AreAllPositionMarked()
        {
            return GetTotalMarks() == Width * Height;
        }

        public bool IsEmpty()
        {
            return GetTotalMarks() == 0;
        }

        public int GetTotalMarks()
        {
            int totalEmptyPositions = 0;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (this.board[i, j] == BoardMarkerType.Empty)
                        totalEmptyPositions++;
                }
            }

            return (Width * Height) - totalEmptyPositions;
        }

        private void Draw()
        {
#if DEBUG
            Console.Clear();
#endif  
            for (int i = 0; i < Height; i++)
            {
                string line = (i + 1) + "   ";
                for (int j = 0; j < Width; j++)
                {
                    line += (board[j, i] == BoardMarkerType.Empty) ? " " : board[j, i].ToString();
                    if (j < Width - 1)
                    {
                        line += " | ";
                    }
                }

                Console.WriteLine(line);

                if (i < Height - 1)
                {
                    Console.WriteLine("    __________");
                }
                else
                {
                    Console.WriteLine("\n    1   2   3");
                }
            }
        }
    }
}
