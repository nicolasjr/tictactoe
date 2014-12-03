using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class PlayerUnbeatable : IPlayer
    {
        public Marker Marker { get; set; }

        public PlayerUnbeatable(Marker marker)
        {
            this.Marker = marker;
        }

        public Play Play(Board board)
        {
            Play newPlay = DecidePlay(board);
            newPlay.PlayerId = this.Marker;

            return newPlay;
        }

        private Play DecidePlay(Board board)
        {
            // Ground Rule #1: start marking center position.
            int center = (board.Size - 1) / 2;
            if (board.GetMarkAtPosition(center, center) == Marker.Empty)
            {
                return PlayAtCenter(center);
            }
            // Ground Rule #2: if center's already marked and board's only marked once, mark a corner.
            if (board.GetTotalMarks() == 1)
            {
                return PlayAtAnyCorner(board);
            }
            // Generic move.
            MinMaxScore maximumScore = MinMaxPlay(board, 0, null);
            return maximumScore.Play;
        }

        private Play PlayAtCenter(int center)
        {
            return CreatePlay(center, center, this.Marker);
        }

        private Play PlayAtAnyCorner(Board board)
        {
            Random random = new Random();
            int multiplier = board.Size - 1;

            // position can not be 1.
            int x = random.Next(0, 2)*multiplier;
            int y = random.Next(0, 2)*multiplier;

            return CreatePlay(x, y, this.Marker);
        }

        private MinMaxScore MinMaxPlay(Board board, int depth, Play play)
        {
            Board newBoard = new Board(board);

            if (play != null)
            {
                // make move.
                newBoard.MarkBoard(play);

                // see if play results in final state.
                MinMaxScore minMaxScore = GetMinMaxScore(newBoard, play, depth);

                // if so, return score.
                if (minMaxScore != null)
                {
                    return minMaxScore;
                }
            }

            // update marker type.
            Marker marker = play == null 
                ? Marker.O : (play.PlayerId == Marker.O) 
                ? Marker.X : Marker.O;

            depth++;

            List<Play> possiblePlays = PossiblePlaysInCurrentBoardState(newBoard, marker);
            // Compute every possible move's score and add to list
            List<MinMaxScore> moves = possiblePlays.Select(possibleMove => MinMaxPlay(newBoard, depth, possibleMove)).ToList();

            // Find maximum score on list.
            MinMaxScore maxMinMaxScoreMove = FindMaxScore(moves);
            return maxMinMaxScoreMove;
        }

        private MinMaxScore GetMinMaxScore(Board board, Play play, int depth)
        {
            MinMaxScore minMaxScore = new MinMaxScore
                                          {
                                              Play = play,
                                              Points = 0
                                          };
            // Check win condition for player X
            if (board.IsVictoryCondition(Marker.X))
            {
                minMaxScore.Points = play.PlayerId == Marker.X ? 10 - depth : depth - 10;
                return minMaxScore;
            }
            // Check win condition for player O
            if (board.IsVictoryCondition(Marker.O))
            {
                minMaxScore.Points = play.PlayerId == Marker.O ? 10 - depth : depth - 10;
                return minMaxScore;
            }
            // Check draw condition
            if (board.AreAllPositionMarked())
            {
                return minMaxScore;
            }
            // otherwise...
            return null;
        }

        private MinMaxScore FindMaxScore(List<MinMaxScore> moves)
        {
            return moves.OrderByDescending(i => i.Points).FirstOrDefault();
        }

        private List<Play> PossiblePlaysInCurrentBoardState(Board board, Marker marker)
        {
            List<Play> moves = new List<Play>();
            for (int i = 0; i < board.Size; i++)
                for (int j = 0; j < board.Size; j++)
                    if (board.GetMarkAtPosition(i, j) == Marker.Empty)
                        moves.Add(CreatePlay(i, j, marker));

            return moves;            
        }

        private Play CreatePlay(int x, int y, Marker marker)
        {
            return new Play
                       {
                           PlayerId = marker,
                           PositionX = x,
                           PositionY = y
                       };
        }
    }
}