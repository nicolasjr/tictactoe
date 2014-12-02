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

                Console.WriteLine(i < board.Size - 1 ? "    __________" : "\n    1   2   3");
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
