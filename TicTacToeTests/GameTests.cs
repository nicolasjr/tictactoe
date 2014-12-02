using NSubstitute;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class GameTests
    {
        [Test]
        public void PlayerMarkedBoard ()
        {
            IPlayer player1 = CreatePlayer();
            IPlayer player2 = CreatePlayer();

            IConsole console = CreateConsole();

            Game game = CreateGame(player1, player2, console);

            const int anyPositionToBeMarkedInX = 2;
            const int anyPositionToBeMarkedInY = 1;
            player1.Play(game.Board).Returns(new Play
                                                 {
                                                     PlayerId = Marker.X,
                                                     PositionX = anyPositionToBeMarkedInX,
                                                     PositionY = anyPositionToBeMarkedInY
                                                 });

            game.RequestPlay();

            Assert.AreEqual(Marker.X, game.Board.GetMarkAtPosition(anyPositionToBeMarkedInX, anyPositionToBeMarkedInY));
        }

        [Test]
        public void ChangedFromPlayer1ToPlayer2()
        {
            IPlayer player1 = CreatePlayer();
            IPlayer player2 = CreatePlayer();

            IConsole console = CreateConsole();

            Game game = CreateGame(player1, player2, console);

            IPlayer firstPlayer = game.GetCurrentPlayer();

            game.ChangePlayer();

            IPlayer secondPlayer = game.GetCurrentPlayer();

            Assert.AreEqual(player1, firstPlayer);
            Assert.AreEqual(player2, secondPlayer);
        }

        [Test]
        public void PlayForWinCondition()
        {
            IPlayer player1 = CreatePlayer();
            IPlayer player2 = CreatePlayer();

            IConsole console = CreateConsole();
            console.ReadInput().Returns("n");

            Game game = CreateGame(player1, player2, console);

            const Marker marker = Marker.X;
            game.Board.MarkBoard(CreatePlay(0, 0, marker));
            game.Board.MarkBoard(CreatePlay(1, 1, marker));

            player1.Play(game.Board).Returns(CreatePlay(2, 2, marker));
            
            game.RequestPlay();

            console.Received().WriteText(GameTexts.UnbeatablePlayerWins);
        }

        [Test]
        public void PlayForDrawCondition()
        {
            IPlayer player1 = CreatePlayer();
            IPlayer player2 = CreatePlayer();

            IConsole console = CreateConsole();
            console.ReadInput().Returns("n");

            Game game = CreateGame(player1, player2, console);

            game.Board.MarkBoard(CreatePlay(0, 1, Marker.X));
            game.Board.MarkBoard(CreatePlay(1, 1, Marker.X));
            game.Board.MarkBoard(CreatePlay(1, 0, Marker.X));
            game.Board.MarkBoard(CreatePlay(2, 0, Marker.X));
            game.Board.MarkBoard(CreatePlay(0, 0, Marker.O));
            game.Board.MarkBoard(CreatePlay(0, 2, Marker.O));
            game.Board.MarkBoard(CreatePlay(1, 2, Marker.O));
            game.Board.MarkBoard(CreatePlay(2, 1, Marker.O));

            player1.Play(game.Board).Returns(CreatePlay(2, 2, Marker.X));

            game.RequestPlay();

            console.Received().WriteText(GameTexts.Draw);
        }

        [Test]
        public void PlayAndChangeCurrentPlayer()
        {
            IPlayer player1 = CreatePlayer();
            IPlayer player2 = CreatePlayer();

            IConsole console = CreateConsole();

            Game game = CreateGame(player1, player2, console);

            const Marker marker = Marker.X;
            player1.Play(game.Board).Returns(CreatePlay(2, 2, marker));

            game.RequestPlay();

            Assert.AreEqual(game.GetCurrentPlayer(), player2);
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

        private IPlayer CreatePlayer ()
        {
            return Substitute.For<IPlayer>();
        }

        private IConsole CreateConsole()
        {
            return Substitute.For<IConsole>();
        }

        private Game CreateGame (IPlayer playerReal, IPlayer playerUnbeatable, IConsole console)
        {
            const int boardSize = 3;

            return new Game(boardSize, playerReal, playerUnbeatable, console);
        }
    }
}
