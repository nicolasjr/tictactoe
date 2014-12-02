using System;

namespace TicTacToe
{
    public class Board
    {
        public Board(int size)
        {
            this.board = new Marker[size * size];
            boardRules = new BoardRules();

            ClearBoard();
        }

        public Board(Board other)
        {
            this.board = new Marker[other.Size * other.Size];

            boardRules = new BoardRules();

            for (int i = 0; i < this.board.Length; i++)
                this.board[i] = other.GetMarkAtIndex(i);
        }

        private readonly Marker[] board; 

        private readonly BoardRules boardRules;

        public int Size
        {
            get { return (int)Math.Sqrt(this.board.Length); }
        }

        public void ClearBoard()
        {
            for (int i = 0; i < this.board.Length; i++)
                this.board[i] = Marker.Empty;
        }

        public void MarkBoard(Play play)
        {
            int index = GetBoardPositionIndex(play.PositionX, play.PositionY);
            
            this.board[index] = play.PlayerId;
        }

        public bool CanActWithPlay(Play play)
        {
            bool positionIsWithinRange = PlayOutOfRange(play) == false;

            if(!positionIsWithinRange)
                return false;

            bool positionIsEmpty = GetMarkAtPosition(play.PositionX, play.PositionY) == Marker.Empty;

            return positionIsEmpty;
        }

        public Marker GetMarkAtPosition(int x, int y)
        {
            int index = GetBoardPositionIndex(x, y);

            return GetMarkAtIndex(index);
        }

        public Marker GetMarkAtIndex(int index)
        {
            if (index < 0 || index >= this.board.Length)
                throw new ArgumentOutOfRangeException();

            return this.board[index];
        }

        public bool AreAllPositionMarked()
        {
            return GetTotalMarks() == this.board.Length;
        }

        public bool IsEmpty()
        {
            return GetTotalMarks() == 0;
        }

        public int GetBoardPositionIndex(int x, int y)
        {
            return x + (y * this.Size);
        }

        public int GetTotalMarks()
        {
            int totalEmptyPositions = 0;
            foreach (Marker marker in this.board)
                if (marker == Marker.Empty)
                    totalEmptyPositions++;

            return board.Length - totalEmptyPositions;
        }

        public bool PlayOutOfRange(Play play)
        {
            bool isOutOfRangeXAxis = play.PositionX >= this.Size || play.PositionX < 0;
            bool isOutOfRangeYAxis = play.PositionY >= this.Size || play.PositionY < 0;

            return isOutOfRangeXAxis || isOutOfRangeYAxis;
        }

        public bool IsVictoryCondition(Marker marker)
        {
            return boardRules.FindWinCondition(this, marker);
        }
    }
}
