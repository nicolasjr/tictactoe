using System;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class BoardTests
    {

        [Test]
        public void PositionMarkedCorrectly()
        {
            Board board = CreateBoard();

            const int anyPositionToBeMarkedInX = 2;
            const int anyPositionToBeMarkedInY = 1;

            const Marker expectecMarker = Marker.X;

            Play play = CreatePlay(anyPositionToBeMarkedInX, anyPositionToBeMarkedInY, expectecMarker);

            board.MarkBoard(play);

            Marker markerAtPosition = board.GetMarkAtPosition(anyPositionToBeMarkedInX, anyPositionToBeMarkedInY);

            Assert.AreEqual(expectecMarker, markerAtPosition);
        }

        [Test]
        public void ClearBoard()
        {
            Board board = CreateBoard();

            const int anyPositionToBeMarkedInX = 2;
            const int anyPositionToBeMarkedInY = 1;

            const Marker expectecMarker = Marker.X;

            Play play = CreatePlay(anyPositionToBeMarkedInX, anyPositionToBeMarkedInY, expectecMarker);

            board.MarkBoard(play);

            Assert.AreNotEqual(true, board.IsEmpty());

            board.ClearBoard();

            Assert.AreEqual(true, board.IsEmpty());
        }

        [TestCase(9)]
        [TestCase(-1)]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void GetMarkInIndexOutOfRange(int index)
        {
            Board board = CreateBoard();

            board.GetMarkAtIndex(index);
        }

        [TestCase(0, 3)]
        [TestCase(2, -1)]
        [TestCase(3, 2)]
        [TestCase(-1, 0)]
        public void PlayIsOutRange(int x, int y)
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;

            Play play = CreatePlay(x, y, marker);

            bool isOutOfRange = board.PlayOutOfRange(play);

            Assert.IsTrue(isOutOfRange);
        }

        [Test]
        public void PlayInRange()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;

            const int x = 1;
            const int y = 2;

            Play play = CreatePlay(x, y, marker);

            bool isOutOfRange = board.PlayOutOfRange(play);

            Assert.IsFalse(isOutOfRange);
        }

        [Test]
        public void AllPositionsMarked()
        {
            Board board = CreateBoard();

            for(int i = 0; i < board.Size; i++)
                for(int j = 0; j < board.Size; j++)
                    board.MarkBoard(CreatePlay(i, j, Marker.X));

            Assert.IsTrue(board.AreAllPositionMarked());
        }

        [Test]
        public void PlayInRangeWithPositionMarked()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;

            const int x = 1;
            const int y = 2;

            Play play = CreatePlay(x, y, marker);

            board.MarkBoard(play);

            bool canActWithPlay = board.CanActWithPlay(play);

            Assert.IsFalse(canActWithPlay);
        }

        [Test]
        public void PlayInRangeWithPositionFree()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;

            const int x = 1;
            const int y = 2;

            Play play = CreatePlay(x, y, marker);

            bool canActWithPlay = board.CanActWithPlay(play);

            Assert.IsTrue(canActWithPlay);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void GetTotalMarks(int expectedMarks)
        {
            Board board = CreateBoard();

            const Marker marker = Marker.O;

            for (int i = 0; i < expectedMarks; i++)
            {
                int y = i%board.Size;
                int x = i/board.Size;

                Play play = CreatePlay(x, y, marker);

                board.MarkBoard(play);
            }

            int totalMarks = board.GetTotalMarks();

            Assert.AreEqual(expectedMarks, totalMarks);
        }

        #region Creation

        private Board CreateBoard()
        {
            const int size = 3;
            return new Board(size);
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

        #endregion
    }
}
