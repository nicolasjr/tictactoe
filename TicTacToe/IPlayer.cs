namespace TicTacToe
{
    public interface IPlayer
    {
        BoardMarkerType Identifier();
        Play Play(Board board);
        void SetWin();
    }
}
