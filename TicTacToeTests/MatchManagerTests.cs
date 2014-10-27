using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NSubstitute;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class MatchManagerTests
    {
        [Test]
        public void StartNewMatch_PlayWithPlayerReal_PlayerActedOnBoard ()
        {
            var playerReal = MockPlayer();
            var playerUnbeatable = MockPlayer();

            var matchManager = CreateMatchManager(playerReal, playerUnbeatable);

            Random random = new Random();

            int anyPositionToBeMarkedInX = random.Next(0, matchManager.Board.Width);
            int anyPositionToBeMarkedInY = random.Next(0, matchManager.Board.Height);
            playerReal.Play(matchManager.Board).Returns(new Play
                                                        {
                                                            PlayerId = BoardMarkerType.X,
                                                            PositionX = anyPositionToBeMarkedInX,
                                                            PositionY = anyPositionToBeMarkedInY
                                                        });

            matchManager.StartNewMatch();

            matchManager.RequestPlay();

            Assert.AreEqual(BoardMarkerType.X, matchManager.Board.GetMarkAtPosition(anyPositionToBeMarkedInX, anyPositionToBeMarkedInY));
        }

        [Test]
        public void RequestPlay_RequestPlayAfterStartedGame_PlayerUnbeatableActedOnBoard()
        {
            var playerReal = MockPlayer();
            var playerUnbeatable = MockPlayer();

            var matchManager = CreateMatchManager(playerReal, playerUnbeatable);

            Random random = new Random();

            int anyPositionToBeMarkedInX = random.Next(0, matchManager.Board.Width);
            int anyPositionToBeMarkedInY = random.Next(0, matchManager.Board.Height);
            playerReal.Play(matchManager.Board).Returns(new Play
                                                        {
                                                            PlayerId = BoardMarkerType.X,
                                                            PositionX = anyPositionToBeMarkedInX,
                                                            PositionY = anyPositionToBeMarkedInY
                                                        });

            int anyOtherPositionToBeMarkedInX = anyPositionToBeMarkedInX;
            int anyOtherPositionToBeMarkedInY = anyPositionToBeMarkedInY;

            while (anyOtherPositionToBeMarkedInX == anyPositionToBeMarkedInX &&
                   anyOtherPositionToBeMarkedInY == anyPositionToBeMarkedInY)
            {
                anyOtherPositionToBeMarkedInX = random.Next(0, matchManager.Board.Width);
                anyOtherPositionToBeMarkedInY = random.Next(0, matchManager.Board.Height);
            }

            playerUnbeatable.Play(matchManager.Board).Returns(new Play
                                                              {
                                                                  PlayerId = BoardMarkerType.O,
                                                                  PositionX = anyOtherPositionToBeMarkedInX,
                                                                  PositionY = anyOtherPositionToBeMarkedInY
                                                              });

            matchManager.StartNewMatch();
            matchManager.RequestPlay();
            matchManager.RequestPlay();

            Assert.AreEqual(BoardMarkerType.O, matchManager.Board.GetMarkAtPosition(anyOtherPositionToBeMarkedInX, anyOtherPositionToBeMarkedInY));
        }

        private IPlayer MockPlayer ()
        {
            return Substitute.For<IPlayer>();
        }

        private MatchManager CreateMatchManager (IPlayer playerReal, IPlayer playerUnbeatable)
        {
            return new MatchManager(playerReal, playerUnbeatable);
        }
    }
}
