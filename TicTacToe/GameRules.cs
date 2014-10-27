using System.Collections.Generic;

namespace TicTacToe
{
    public class GameRules
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

        public bool FindWinCondition(Board board, BoardMarkerType markerType)
        {
            foreach (int[] position in GameRules.PositionsToCheck)
            {
                for (int i = 0; i < 3; i++)
                {
                    var play = new Play
                    {
                        PlayerId = board.GetMarkAtPosition(i, i),
                        PositionX = i,
                        PositionY = i
                    };

                    if(CheckBoard(board, play, position, markerType))
                        return true;
                }
            }

            return false;
        }

        private bool CheckBoard(Board board, Play play, int[] checkPositions, BoardMarkerType markerType)
        {
            if (play.PlayerId != markerType)
                return false;

            int deltaX = checkPositions[0];
            int deltaY = checkPositions[1];

            // List to keep track of plays in the direction that's being searched. 
            var plays = new List<Play>();
            plays.Add(play);

            const int middlePos = 1;

            var firstPlay = play;

            for (int i = 0; i < 2; i++)
            {
                // checks if new positions are out of range.
                if (firstPlay.PositionX + deltaX > board.Width - 1 || firstPlay.PositionX + deltaX < 0 ||
                    firstPlay.PositionY + deltaY > board.Height - 1 || firstPlay.PositionY + deltaY < 0)
                    return false;

                // creates a new play, based on checkPositions values.
                var nextPlay = new Play
                                    {
                                        PlayerId = board.GetMarkAtPosition(firstPlay.PositionX + deltaX, firstPlay.PositionY + deltaY),
                                        PositionX = firstPlay.PositionX + deltaX,
                                        PositionY = firstPlay.PositionY + deltaY
                                    };

                if (nextPlay.PlayerId != markerType)
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

        public List<Play> PossibleMoves(Board board, BoardMarkerType markerType)
        {
            var moves = new List<Play>();
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (board.GetMarkAtPosition(i, j) == BoardMarkerType.Empty)
                    {
                        moves.Add(new Play
                                      {
                                          PlayerId = markerType,
                                          PositionX = i,
                                          PositionY = j
                                      });
                    }
                }
            }

            return moves;
        }
    }
}
