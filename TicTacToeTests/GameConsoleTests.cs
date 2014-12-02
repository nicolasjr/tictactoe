using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class GameConsoleTests
    {

        [Test]
        public void VerifyConsoleOutput()
        {
            StringBuilder fakeOutput = CreateStringBuilder();

            IConsole console = new GameConsole();

            console.WriteText(GameTexts.RequestPlayerAction);

            Assert.IsTrue(fakeOutput.ToString().Contains(GameTexts.RequestPlayerAction));
        }

        [Test]
        public void BoardGetsDrawn()
        {
            StringBuilder fakeOutput = CreateStringBuilder();

            IConsole console = new GameConsole();

            const int boardSize = 3;
            Board board = new Board(boardSize);

            console.DrawBoard(board);
            
            Assert.IsTrue(fakeOutput.ToString().Contains("1"));
            Assert.IsFalse(fakeOutput.ToString().Contains("X"));
        }

        private StringBuilder CreateStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            Console.SetOut(sw);

            return sb;
        }
    }
}
