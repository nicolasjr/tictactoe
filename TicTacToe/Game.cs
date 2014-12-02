namespace TicTacToe
{
    public class Game
    {
        public Board Board { get; set; }

        private readonly IPlayer player1;
        private readonly IPlayer player2;

        private IPlayer currentPlayer;
        private IPlayer matchesFirstPlayer;

        private readonly IConsole console;

        private bool isFinished;

        public Game(int boardSize, IPlayer player1, IPlayer player2, IConsole console)
        {
            //const int boardSize = 3;
            this.Board = new Board(boardSize);

            this.console = console;

            this.player1 = player1;

            this.player2 = player2;

            this.isFinished = false;

            this.currentPlayer = this.player1;
            this.matchesFirstPlayer = this.currentPlayer;
        }

        public void RestartMatch()
        {
            this.isFinished = false;

            this.matchesFirstPlayer = this.matchesFirstPlayer.Equals(this.player1) ? this.player2 : this.player1;
            this.currentPlayer = this.matchesFirstPlayer;

            this.Board.ClearBoard();

            GameLoop();
        }

        public void GameLoop()
        {
            DrawBoard();

            while (this.isFinished == false)
            {
                RequestPlay();
            }
        }

        public void RequestPlay()
        {
            bool canActWithPlay = false;
            Play play = null;

            while (!canActWithPlay)
            {
                play = this.currentPlayer.Play(this.Board);

                canActWithPlay = this.Board.CanActWithPlay(play);
            }

            this.Board.MarkBoard(play);

            DrawBoard();

            bool won = CheckForVictory(play);

            if (won)
            {
                string winningText = (this.currentPlayer is PlayerReal)
                    ? (GameTexts.HumanPlayerWins)
                    : (GameTexts.UnbeatablePlayerWins);

                console.WriteText(winningText);

                FinishMatch();
            }
            else
            {
                if (Board.AreAllPositionMarked())
                {
                    console.WriteText(GameTexts.Draw);

                    FinishMatch();
                }
                else
                {
                    ChangePlayer();
                }
            }
        }

        public void ChangePlayer()
        {
            this.currentPlayer = (this.currentPlayer.Equals(this.player1)) ? 
                (this.player2) : (this.player1);
        }

        private bool CheckForVictory(Play play)
        {
            return this.Board.IsVictoryCondition(play.PlayerId);
        }

        private void FinishMatch()
        {
            console.WriteText(GameTexts.StartNewMatch);

            string answer = this.console.ReadInput();

            if (answer.Contains("n"))
            {
                this.isFinished = true;
            }
            else
            {
                RestartMatch();
            }
        }

        private void DrawBoard()
        {
            this.console.Clear();
            this.console.DrawBoard(this.Board);
        }

        public IPlayer GetCurrentPlayer()
        {
            return this.currentPlayer;
        }
    }
}                                   