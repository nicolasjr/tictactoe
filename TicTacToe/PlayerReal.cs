using System;

namespace TicTacToe
{
    public class PlayerReal : IPlayer
    {

        public BoardMarkerType Identifier()
        {
            return BoardMarkerType.X;
        }

        public Play Play(Board board)
        {
            Console.WriteLine("\nChoose the column and row to play.");

            Console.WriteLine("Row:");
            string stringCol = Console.ReadLine();

            int col;
            int.TryParse(stringCol, out col);

            Console.WriteLine("Column:");
            string stringRow = Console.ReadLine();

            int row;
            int.TryParse(stringRow, out row);

            return new Play
                       {
                           PlayerId = Identifier(),
                           PositionX = row - 1,
                           PositionY = col - 1
                       };
        }

        public void SetWin()
        {
            Console.WriteLine("Human player wins");
        }
    }
}
