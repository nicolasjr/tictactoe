using System;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    public class GameRulesTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CheckBoard_BoardWithLine_ReturnsVictoryCondition(int lineIndex)
        {
            const int boardWidth = 3;
            const int boardHeight = 3;

            var board = new Board(boardWidth, boardHeight);

            for (int i = 0; i < 3; i++)
            {
                board.MarkBoard(new Play
                                    {
                                        PlayerId = BoardMarkerType.X,
                                        PositionX = lineIndex,
                                        PositionY = i
                                    });
            }
            
            Random random = new Random();
            int anyStartingYPosition = random.Next(0, boardHeight);

            var play = new Play
                           {
                               PlayerId = BoardMarkerType.X,
                               PositionX = lineIndex,
                               PositionY = anyStartingYPosition
                           };

            var ticTacToe = new GameRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CheckBoard_BoardWithColumn_ReturnsVictoryCondition(int columnIndex)
        {
            const int boardWidth = 3;
            const int boardHeight = 3;

            var board = new Board(boardWidth, boardHeight);

            for (int i = 0; i < 3; i++)
            {
                board.MarkBoard(new Play
                                    {
                                        PlayerId = BoardMarkerType.X,
                                        PositionX = i,
                                        PositionY = columnIndex
                                    });
            }

            Random random = new Random();
            int anyStartingXPosition = 0;

            var play = new Play
                           {
                               PlayerId = BoardMarkerType.X,
                               PositionX = anyStartingXPosition,
                               PositionY = columnIndex
                           };

            var ticTacToe = new TicTacToe.GameRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        [Test]
        public void CheckBoard_BoardWithDiagonalTopToBottomLeftToRight_ReturnsVictoryCondition()
        {
            const int boardWidth = 3;
            const int boardHeight = 3;

            var board = new Board(boardWidth, boardHeight);

            for (int i = 0; i < 3; i++)
            {
                board.MarkBoard(new Play
                                    {
                                        PlayerId = BoardMarkerType.X,
                                        PositionX = i,
                                        PositionY = i
                                    });
            }

            var play = new Play
                           {
                               PlayerId = BoardMarkerType.X,
                               PositionX = 0,
                               PositionY = 0
                           };

            var ticTacToe = new TicTacToe.GameRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }

        [Test]
        public void CheckBoard_BoardWithDiagonalBottomToTopLeftToRight_ReturnsVictoryCondition()
        {
            const int boardWidth = 3;
            const int boardHeight = 3;

            var board = new Board(boardWidth, boardHeight);

            for (int i = 0; i < 3; i++)
            {
                board.MarkBoard(new Play
                                    {
                                        PlayerId = BoardMarkerType.X,
                                        PositionX = i,
                                        PositionY = board.Height - 1 - i
                                    });
            }

            var play = new Play
                           {
                               PlayerId = BoardMarkerType.X,
                               PositionX = 0,
                               PositionY = 2
                           };

            var ticTacToe = new TicTacToe.GameRules();

            bool isWinCondition = ticTacToe.FindWinCondition(board, play.PlayerId);

            Assert.AreEqual(true, isWinCondition);
        }
    }
}
