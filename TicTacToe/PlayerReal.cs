namespace TicTacToe
{
    public class PlayerReal : IPlayer
    {
        public Marker Marker { get; set; }

        private readonly IConsole console;

        public PlayerReal(Marker marker, IConsole console)
        {
            this.Marker = marker;
            this.console = console;
        }
        
        public Play Play(Board board)
        {
            console.WriteText(GameTexts.Row);
            string stringRow = console.ReadInput();

            int row;
            int.TryParse(stringRow, out row);

            console.WriteText(GameTexts.Column);
            string stringCol = console.ReadInput();

            int col;
            int.TryParse(stringCol, out col);

            return new Play
                       {
                           PlayerId = this.Marker,
                           PositionX = col - 1,
                           PositionY = row - 1
                       };
        }
    }
}
