using NSubstitute;
using NUnit.Framework;
using TicTacToe;

namespace TicTacToeTests
{
    [TestFixture]
    class PlayerTests
    {

        [Test]
        public void Play_SimulateMatch_BoardGetsFilled()
        {
            IPlayer playerReal = CreatePlayer();
            IPlayer playerUnbeatable = CreatePlayer();

            MatchManager matchManager = new MatchManager(playerReal, playerUnbeatable);

            playerReal.Play(matchManager.Board).Returns(new Play
                                                        {
                                                            PlayerId = BoardMarkerType.X,
                                                            PositionX = 1,
                                                            PositionY = 1
                                                        });

            playerUnbeatable.Play(matchManager.Board).Returns(new Play
                                                              {
                                                                  PlayerId = BoardMarkerType.O,
                                                                  PositionX = 0,
                                                                  PositionY = 0
                                                              });

            matchManager.StartNewMatch();

            matchManager.RequestPlay();
            matchManager.RequestPlay();

            Assert.IsTrue(matchManager.Board.GetMarkAtPosition(1, 1) == BoardMarkerType.X 
                && matchManager.Board.GetMarkAtPosition(0, 0) == BoardMarkerType.O);
        }

        [Test]
        public void Play_PlayerUnbeatablePlaysFirst_CenterGetsFilled()
        {
            Board board = CreateBoard();

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(1, 1));
        }

        [Test]
        public void Play_PlayerUnbeatablePlaysSecondWithCenterEmpty_CenterGetsFilled()
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.X,
                                PositionX = 1,
                                PositionY = 0
                            });

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(1, 1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Play_PlayerUnbeatablePlaysAboutToLoseInVerticalLine_AvoidDefeat(int verticalLine)
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.X,
                                PositionX = verticalLine,
                                PositionY = 0
                            });

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.X,
                                PositionX = verticalLine,
                                PositionY = 1
                            });

            if (verticalLine != 1)
            {
                board.MarkBoard(new Play
                                {
                                    PlayerId = BoardMarkerType.O,
                                    PositionX = 1,
                                    PositionY = 1
                                });
            }

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(verticalLine, 2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Play_PlayerUnbeatablePlaysAboutToLoseInHorizontalLine_AvoidDefeat(int horizontalLine)
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.X,
                                PositionX = 0,
                                PositionY = horizontalLine
                            });

            board.MarkBoard(new Play
                                {
                                    PlayerId = BoardMarkerType.X,
                                    PositionX = 1,
                                    PositionY = horizontalLine
                                });

            if (horizontalLine != 1)
            {
                board.MarkBoard(new Play
                                    {
                                        PlayerId = BoardMarkerType.O,
                                        PositionX = 1,
                                        PositionY = 1
                                    });
            }

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(2, horizontalLine));
        }

        [Test]
        public void Play_PlayerUnbeatablePlaysAboutToLoseInDiagonalUpToDownLeftToRight_AvoidDefeat()
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
                                {
                                    PlayerId = BoardMarkerType.X,
                                    PositionX = 0,
                                    PositionY = 0,
                                });

            board.MarkBoard(new Play
                                {
                                    PlayerId = BoardMarkerType.X,
                                    PositionX = 1,
                                    PositionY = 1
                                });

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(2, 2));
        }

        [Test]
        public void Play_PlayerUnbeatablePlaysAboutToLoseInDiagonalUpToDownRightToLeft_AvoidDefeat()
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
            {
                PlayerId = BoardMarkerType.X,
                PositionX = 2,
                PositionY = 0,
            });

            board.MarkBoard(new Play
            {
                PlayerId = BoardMarkerType.X,
                PositionX = 1,
                PositionY = 1
            });

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(0, 2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Play_PlayerUnbeatablePlaysAboutToWinInVerticalLine_AvoidDefeat(int verticalLine)
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
                                {
                                    PlayerId = BoardMarkerType.O,
                                    PositionX = verticalLine,
                                    PositionY = 0
                                });

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.O,
                                PositionX = verticalLine,
                                PositionY = 1
                            });

            if (verticalLine != 1)
            {
                board.MarkBoard(new Play
                                {
                                    PlayerId = BoardMarkerType.X,
                                    PositionX = 1,
                                    PositionY = 1
                                });
            }

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(verticalLine, 2));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Play_PlayerUnbeatablePlaysAboutToWinInHorizontalLine_AvoidDefeat(int horizontalLine)
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.O,
                                PositionX = 0,
                                PositionY = horizontalLine
                            });

            board.MarkBoard(new Play
                            {
                                PlayerId = BoardMarkerType.O,
                                PositionX = 1,
                                PositionY = horizontalLine
                            });

            if (horizontalLine != 1)
            {
                board.MarkBoard(new Play
                                    {
                                        PlayerId = BoardMarkerType.X,
                                        PositionX = 1,
                                        PositionY = 1
                                    });
            }

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(2, horizontalLine));
        }

        [Test]
        public void Play_PlayerUnbeatablePlaysAboutToWinInDiagonalUpToDownLeftToRight_AvoidDefeat()
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
            {
                PlayerId = BoardMarkerType.O,
                PositionX = 0,
                PositionY = 0,
            });

            board.MarkBoard(new Play
            {
                PlayerId = BoardMarkerType.O,
                PositionX = 1,
                PositionY = 1
            });

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(2, 2));
        }

        [Test]
        public void Play_PlayerUnbeatablePlaysAboutToWinInDiagonalUpToDownRightToLeft_AvoidDefeat()
        {
            Board board = CreateBoard();

            board.MarkBoard(new Play
            {
                PlayerId = BoardMarkerType.O,
                PositionX = 2,
                PositionY = 0,
            });

            board.MarkBoard(new Play
            {
                PlayerId = BoardMarkerType.O,
                PositionX = 1,
                PositionY = 1
            });

            PlayerUnbeatable playerUnbeatable = new PlayerUnbeatable();

            board.MarkBoard(playerUnbeatable.Play(board));

            Assert.AreEqual(playerUnbeatable.Identifier(), board.GetMarkAtPosition(0, 2));
        }

        private Board CreateBoard()
        {
            const int width = 3;
            const int height = 3;
            
            return new Board(width, height);
        }

        private IPlayer CreatePlayer()
        {
            return Substitute.For<IPlayer>();
        }
    }
}
