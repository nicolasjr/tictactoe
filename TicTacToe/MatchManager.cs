using System;

namespace TicTacToe
{
    public class MatchManager
    {
        public Board Board { get; set; }

        private readonly IPlayer playerReal;
        private readonly IPlayer playerUnbeatable;

        private IPlayer player;

        private readonly GameRules ticTacToe;

        private bool isFinished;

        public MatchManager(IPlayer playerReal, IPlayer playerUnbeatable)
        {
            const int boardWidth = 3;
            const int boardHeight = 3;
            this.Board = new Board(boardWidth, boardHeight);

            this.playerReal = playerReal;
            this.playerUnbeatable = playerUnbeatable;

            ticTacToe = new GameRules();
        }

        public void StartNewMatch()
        {
            this.Board.ClearBoard();

            this.isFinished = false;

            this.player = this.playerReal;
        }

        public void RequestPlay()
        {
            bool didPlay = false;
            Play play = null;

            while (!didPlay)
            {
                play = this.player.Play(this.Board);

                didPlay = Board.MarkBoard(play);
            }

            bool won = CheckForWin(play);

            if (won)
            {
                this.player.SetWin();

                FinishMatch(false);
            }
            else
            {
                if (Board.AreAllPositionMarked())
                {
                    FinishMatch(true);
                }
                else
                {
                    ChangePlayer();
                }
            }
        }

        private void ChangePlayer()
        {
            this.player = (this.player.Equals(this.playerReal)) ? 
                (this.playerUnbeatable) : (this.playerReal);
        }

        private bool CheckForWin(Play play)
        {
            return ticTacToe.FindWinCondition(this.Board, play.PlayerId);
        }

        private void FinishMatch(bool isDraw)
        {
            if (isDraw)
            {
                Console.WriteLine("There are no winners!");
            }

            Console.WriteLine("Start new match? (Y / N)");

            string answer = Console.ReadLine();

            if (answer.Contains("n"))
            {
                this.isFinished = true;
            }
            else
            {
                StartNewMatch();
            }
        }

        public bool IsGameFinished()
        {
            return this.isFinished;
        }
    }
}                                   