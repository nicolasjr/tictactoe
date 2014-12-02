using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    public class BoardRulesTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void VictoryConditionInHorizontalLine(int lineIndex)
        {
            Board board = CreateBoard();

            for (int i = 0; i < board.Size; i++)
                board.MarkBoard(CreatePlay(lineIndex, i, Marker.X));
            
            const int anyStartingYPosition = 1;

            Play play = CreatePlay(lineIndex, anyStartingYPosition, Marker.X);

            BoardRules ticTacToe = new BoardRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void VictoryConditionInVerticalLine(int columnIndex)
        {
            Board board = CreateBoard();

            for (int i = 0; i < board.Size; i++)
                board.MarkBoard(CreatePlay(i, columnIndex, Marker.X));

            const int anyStartingXPosition = 0;

            Play play = CreatePlay(anyStartingXPosition, columnIndex, Marker.X);

            BoardRules ticTacToe = new BoardRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        [Test]
        public void VictoryConditionDiagonalTopToBottomLeftToRight()
        {
            Board board = CreateBoard();

            for (int i = 0; i < board.Size; i++)
                board.MarkBoard(CreatePlay(i, i, Marker.X));

            Play play = CreatePlay(0, 0, Marker.X);

            BoardRules ticTacToe = new BoardRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        [Test]
        public void VictoryConditionDiagonalBottomToTopLeftToRight()
        {
            Board board = CreateBoard();

            for (int i = 0; i < board.Size; i++)
                board.MarkBoard(CreatePlay(i, board.Size - 1 - i, Marker.X));

            Play play = CreatePlay(0, 2, Marker.X);

            BoardRules ticTacToe = new BoardRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        private Play CreatePlay(int x, int y, Marker marker)
        {
            return new Play
                       {
                           PlayerId = marker,
                           PositionX = x,
                           PositionY = y
                       };
        }

        private Board CreateBoard()
        {
            const int boardSize = 3;

            return new Board(boardSize);
        }
    }
}
