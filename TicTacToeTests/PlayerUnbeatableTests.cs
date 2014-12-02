using NSubstitute;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class PlayerUnbeatableTests
    {

        [Test]
        public void BoardGetsFilledWithMarks()
        {
            IPlayer playerReal = CreatePlayer();
            IPlayer playerUnbeatable = CreatePlayer();

            const int boardSize = 3;
            Game game = new Game(boardSize, playerReal, playerUnbeatable, Substitute.For<IConsole>());

            playerReal.Play(game.Board).Returns(CreatePlay(1, 1, Marker.X));

            playerUnbeatable.Play(game.Board).Returns(CreatePlay(0, 0, Marker.O));

            game.RequestPlay();
            game.RequestPlay();

            Assert.IsTrue(game.Board.GetMarkAtPosition(1, 1) == Marker.X 
                && game.Board.GetMarkAtPosition(0, 0) == Marker.O);
        }

        [Test]
        public void PlayerUnbeatablePlaysFirstAndMarksCenter()
        {
            Board board = CreateBoard();

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(1, 1));
        }

        [Test]
        public void PlayerUnbeatablePlaysSecondWithCenterEmptyAndMarksCenter()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;
            board.MarkBoard(CreatePlay(1, 0, marker));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(1, 1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void AvoidDefeatInVerticalLine(int verticalLine)
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;
            board.MarkBoard(CreatePlay(verticalLine, 0, marker));
            board.MarkBoard(CreatePlay(verticalLine, 1, marker));

            if (verticalLine != 1)
                board.MarkBoard(CreatePlay(1, 1, Marker.O));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(verticalLine, 2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void AvoidDefeatInHorizontalLine(int horizontalLine)
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;
            board.MarkBoard(CreatePlay(0, horizontalLine, marker));
            board.MarkBoard(CreatePlay(1, horizontalLine, marker));

            if (horizontalLine != 1)
                board.MarkBoard(CreatePlay(1, 1, Marker.O));

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable(Marker.O);

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(2, horizontalLine));
        }

        [Test]
        public void AvoidDefeatInDiagonalLineUpToDownLeftToRight()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;
            board.MarkBoard(CreatePlay(0, 0, marker));
            board.MarkBoard(CreatePlay(1, 1, marker));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(2, 2));
        }

        [Test]
        public void AvoidDefeatInDiagonalLineUpToDownRightToLeft()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.X;

            board.MarkBoard(CreatePlay(2, 0, marker));
            board.MarkBoard(CreatePlay(1, 1, marker));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(0, 2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void WinInVerticalLine(int verticalLine)
        {
            Board board = CreateBoard();

            const Marker marker = Marker.O;
            board.MarkBoard(CreatePlay(verticalLine, 0, marker));
            board.MarkBoard(CreatePlay(verticalLine, 1, marker));

            if (verticalLine != 1)
                board.MarkBoard(CreatePlay(1, 1, Marker.X));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(verticalLine, 2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void WinInHorizontalLine(int horizontalLine)
        {
            Board board = CreateBoard();

            const Marker marker = Marker.O;
            board.MarkBoard(CreatePlay(0, horizontalLine, marker));
            board.MarkBoard(CreatePlay(1, horizontalLine, marker));

            if (horizontalLine != 1)
                board.MarkBoard(CreatePlay(1, 1, Marker.X));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(2, horizontalLine));
        }

        [Test]
        public void WinInDiagonalUpToDownLeftToRight()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.O;
            board.MarkBoard(CreatePlay(0, 0, marker));
            board.MarkBoard(CreatePlay(1, 1, marker));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(2, 2));
        }

        [Test]
        public void WinInDiagonalUpToDownRightToLeft()
        {
            Board board = CreateBoard();

            const Marker marker = Marker.O;
            board.MarkBoard(CreatePlay(2, 0, marker));
            board.MarkBoard(CreatePlay(1, 1, marker));

            PlayerUnbeatable playerUnbeatable = CreatePlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Marker, board.GetMarkAtPosition(0, 2));
        }

        private Board CreateBoard()
        {
            const int size = 3;

            return new Board(size);
        }

        private PlayerUnbeatable CreatePlayerUnbeatable()
        {
            return new PlayerUnbeatable(Marker.O);
        }

        private IPlayer CreatePlayer()
        {
            return Substitute.For<IPlayer>();
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
    }
}
