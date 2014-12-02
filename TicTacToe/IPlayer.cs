namespace TicTacToe
{
    public interface IPlayer
    {
        Marker Marker { get; set; }
        Play Play(Board board);
    }
}
