using NSubstitute;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class PlayerRealTests
    {
        [Test]
        public void PlayerMakeMove()
        {
            IConsole console = Substitute.For<IConsole>();
            const Marker marker = Marker.X;

            PlayerReal player = new PlayerReal(marker, console);

            const int boardSize = 3;
            Board board = new Board(boardSize);

            console.ReadInput().Returns("1");

            Play play = player.Play(board);

            board.MarkBoard(play);

            Marker boardMarked = board.GetMarkAtPosition(0, 0);

            Assert.AreEqual(marker, boardMarked);
        }
    }
}
