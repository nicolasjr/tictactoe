using System;
namespace TicTacToe
{
    public class GameConsole : IConsole
    {
        public void WriteText(string text)
        {
            Console.WriteLine(text);
        }

        public void DrawBoard(Board board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                string line = (i + 1) + "   ";
                for (int j = 0; j < board.Size; j++)
                {
                    Marker marker = board.GetMarkAtPosition(j, i);
                    line += (marker == Marker.Empty) ? " " : marker.ToString();
                    if (j < board.Size - 1)
                    {
                        line += " | ";
                    }
                }

                Console.WriteLine(line);

                string row = i < board.Size - 1 ? "    " : "\n    ";

                for (int k = 0; k < board.Size; k++)
                {
                    if (i < board.Size - 1)
                        row += "___";
                    else
                        row += (k + 1) + "   ";
                }

                //Console.WriteLine(i < board.Size - 1 ? "    _________" : "\n    1   2   3");
                Console.WriteLine(row);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
