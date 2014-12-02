using System.ComponentModel.DataAnnotations;

namespace TicTacToe
{
    public class Play
    {
        [Range(0, 2)]
        public int PositionX { get; set; }

        [Range(0, 2)]
        public int PositionY { get; set; }

        public Marker PlayerId { get; set; }
    }
}
