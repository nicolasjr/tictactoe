using System.Collections.Generic;

namespace TicTacToe
{
    public class BoardRules
    {
        public static int[][] PositionsToCheck  =  new int[][]
                                                       {
                                                           new int[] {1, 0},
                                                           new int[] {1, 1},
                                                           new int[] {1, -1},
                                                           new int[] {-1, 0},
                                                           new int[] {-1, 1},
                                                           new int[] {-1, -1},
                                                           new int[] {0, 1},
                                                           new int[] {0, -1}
                                                       };

        public bool FindWinCondition(Board board, Marker marker)
        {
            foreach (int[] position in PositionsToCheck)
            {
                for (int i = 0; i < 3; i++)
                {
                    Play play = new Play
                                    {
                                        PlayerId = board.GetMarkAtPosition(i, i),
                                        PositionX = i,
                                        PositionY = i
                                    };

                    if(CheckBoard(board, play, position, marker))
                        return true;
                }
            }

            return false;
        }

        private bool CheckBoard(Board board, Play play, int[] checkPositions, Marker marker)
        {
            if (play.PlayerId != marker)
                return false;

            int deltaX = checkPositions[0];
            int deltaY = checkPositions[1];

            // List to keep track of plays in the direction that's being searched. 
            List<Play> plays = new List<Play> { play };

            const int middlePos = 1;

            Play firstPlay = play;

            for (int i = 0; i < 2; i++)
            {
                // creates a new play, based on checkPositions values.
                Play nextPlay = new Play
                                    {
                                        PositionX = firstPlay.PositionX + deltaX,
                                        PositionY = firstPlay.PositionY + deltaY
                                    };

                if (board.PlayOutOfRange(nextPlay))
                    return false;

                nextPlay.PlayerId = board.GetMarkAtPosition(nextPlay.PositionX, nextPlay.PositionY);

                if (nextPlay.PlayerId != marker)
                    return false;

                plays.Add(nextPlay);

                // checks if verification's starting from middle of row/column/diagonal.
                if ((firstPlay.PositionX == middlePos && nextPlay.PositionX != middlePos) ||
                    (firstPlay.PositionY == middlePos && nextPlay.PositionY != middlePos))
                {
                    // if so, invert the checking position
                    deltaX *= -1;
                    deltaY *= -1;

                    // and start looking from original one.
                    firstPlay = play;
                }
                // Otherwise, starts looking from new one.
                else
                {
                    firstPlay = nextPlay;
                }
            }

            return true;
        }

        
    }
}
