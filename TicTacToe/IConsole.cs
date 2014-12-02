namespace TicTacToe
{
    public interface IConsole
    {
        void WriteText(string text);

        void Clear();

        void DrawBoard(Board board);

        string ReadInput();
    }
}