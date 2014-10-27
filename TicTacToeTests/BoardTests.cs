using System;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class BoardTests
    {
        [Test]
        public void Initialize_With3x3Board_BoardsDimensionsAreCorrect()
        {
            const int boardWidth = 3;
            const int boardHeight = 3;

            Board board = new Board(boardWidth, boardHeight);

            Assert.IsTrue(board.Width == boardWidth && boardHeight == board.Height);
        }

        [Test]
        public void MarkBoardAtPosition_AnyPositionMarkedWithX_PositionMarkedCorrectly()
        {
            const int boardWidth = 3;
            const int boardHeight = 3;

            Board board = new Board(boardWidth, boardHeight);

            Random random = new Random();

            int anyPositionToBeMarkedInX = random.Next(0, board.Width);
            int anyPositionToBeMarkedInY = random.Next(0, board.Height);

            Play play = new Play
                            {
                                PlayerId = BoardMarkerType.X,
                                PositionX = anyPositionToBeMarkedInX,
                                PositionY = anyPositionToBeMarkedInY
                            };

            board.MarkBoard(play);

            BoardMarkerType[,] concreteBoard = board.GetBoard();

            Assert.AreEqual(BoardMarkerType.X, concreteBoard[anyPositionToBeMarkedInX, anyPositionToBeMarkedInY]);
        }


    }
}
