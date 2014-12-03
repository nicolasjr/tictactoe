using System.Collections.Generic;

namespace TicTacToe
{
    public class VictoryCondition
    {
        private readonly int[][] directionsToCheck = new int[][]
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

        public bool FoundVictoryCondition(Board board, Marker marker)
        {
            foreach (int[] position in directionsToCheck)
            {
                for (int i = 0; i < board.Size; i++)
                {
                    Play play = new Play
                                    {
                                        PlayerId = board.GetMarkAtPosition(i, i),
                                        PositionX = i,
                                        PositionY = i
                                    };

                    // Verifies if board's position is marked by potential winner.
                    if(play.PlayerId != marker)
                        continue;

                    if(SearchForVictoryCondition(board, play, position, marker))
                        return true;
                }
            }

            return false;
        }

        private bool SearchForVictoryCondition(Board board, Play play, int[] checkPositions, Marker marker)
        {
            // Direction to search
            int directionX = checkPositions[0];
            int directionY = checkPositions[1];

            Play firstPlay = play;

            for (int i = 0; i < board.Size - 1; i++)
            {
                // creates a new play, based on checkPositions values.
                Play nextPlay = new Play
                                    {
                                        PositionX = firstPlay.PositionX + directionX,
                                        PositionY = firstPlay.PositionY + directionY
                                    };

                if (board.PlayOutOfRange(nextPlay))
                    return false;

                Marker nextMarker = board.GetMarkAtPosition(nextPlay.PositionX, nextPlay.PositionY);

                if (nextMarker != marker)
                    return false;

                nextPlay.PlayerId = nextMarker;

                // checks if verification's starting from middle of row/column/diagonal.
                int middlePos = (board.Size - 1) / 2;
                if (NeedToInvertDirection(firstPlay, nextPlay, middlePos))
                {
                    // if so, invert the checking direction
                    directionX *= -1;
                    directionY *= -1;

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

        private bool NeedToInvertDirection(Play firstPlay, Play nextPlay, int middlePos)
        {
            bool columnStartsFromMiddle = (firstPlay.PositionX == middlePos && nextPlay.PositionX != middlePos);
            bool rowStartsFromMiddle = (firstPlay.PositionY == middlePos && nextPlay.PositionY != middlePos);

            return columnStartsFromMiddle || rowStartsFromMiddle;
        }
    }
}
