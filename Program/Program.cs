namespace Program
{
    using TicTacToe;

    class Program
    {
        static void Main(string[] args)
        {
            IConsole console = new GameConsole();

            IPlayer playerReal = new PlayerReal(Marker.X, console);
            IPlayer playerUnbeatable = new PlayerUnbeatable(Marker.O);

            const int boardSize = 3;

            Game game = new Game(boardSize, playerReal, playerUnbeatable, console);
            
            game.GameLoop();
        }
    }
}
