using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class PlayerUnbeatable : IPlayer
    {
        public BoardMarkerType Identifier()
        {
            return BoardMarkerType.O;
        }

        public Play Play(Board board)
        {
            Play newPlay = DecidePlay(board);
            newPlay.PlayerId = Identifier();

            return newPlay;
        }

        private Play DecidePlay(Board board)
        {
            // Ground Rule #1: start marking center position.
            if (board.GetMarkAtPosition(1, 1) == BoardMarkerType.Empty)
            {
                return PlayAtCenter();
            }
            // Ground Rule #2: if center's already marked, mark a corner.
            else if (board.GetTotalMarks() == 1)
            {
                return PlayAtCorner();
            }
            // Generic move.
            var score = DecidePlay(board, 0, null);
            return score.Play;
        }

        private Play PlayAtCenter()
        {
            return new Play
                       {
                           PlayerId = Identifier(),
                           PositionX = 1,
                           PositionY = 1
                       };
        }

        private Play PlayAtCorner()
        {
            Random random = new Random();
            const int multiplier = 2;

            // position can not be 1.
            int x = random.Next(0, 2)*multiplier;
            int y = random.Next(0, 2)*multiplier;

            return new Play
                   {
                       PlayerId = Identifier(),
                       PositionX = x,
                       PositionY = y
                   };
        }

        private Score DecidePlay(Board board, int depth, Play play)
        {
            var rules = new GameRules();

            var newBoard = new Board(board);

            if (play != null)
            {
                // make move.
                newBoard.GetBoard()[play.PositionX, play.PositionY] = play.PlayerId;

                // see if play results in final state.
                var score = GetScore(newBoard, play, rules, depth);

                // if so, return score.
                if (score != null)
                {
                    return score;
                }
            }

            // update marker type.
            BoardMarkerType markerType = play == null ? BoardMarkerType.O : (play.PlayerId == BoardMarkerType.O) ? BoardMarkerType.X : BoardMarkerType.O;

            depth++;

            var moves = new List<Score>();
            // Compute every possible move's score and add to list
            foreach (var possibleMove in rules.PossibleMoves(newBoard, markerType))
            {
                moves.Add(
                    DecidePlay(newBoard, depth, possibleMove));
            }

            // Find maximum score on list.
            var maxScoreMove = FindMaxScore(moves);
            return maxScoreMove;
        }

        private Score GetScore(Board board, Play play, GameRules rules, int depth)
        {
            var score = new Score
                            {
                                Play = play,
                                Points = 0
                            };

            // Check win condition for player X
            if (rules.FindWinCondition(board, BoardMarkerType.X))
            {
                score.Points = play.PlayerId == BoardMarkerType.X ? 10 - depth : depth - 10;
                return score;
            }
            // Check win condition for player O
            if (rules.FindWinCondition(board, BoardMarkerType.O))
            {
                score.Points = play.PlayerId == BoardMarkerType.O ? 10 - depth : depth - 10;
                return score;
            }
            // Check draw condition
            if (board.AreAllPositionMarked())
            {
                return score;
            }
            // otherwise...
            return null;
        }

        private Score FindMaxScore(List<Score> moves)
        {
            var maxScoreMove = new List<Score>();
            maxScoreMove.Add(moves.First());
            foreach (Score move in moves)
            {
                if (move.Points > maxScoreMove.First().Points)
                {
                    maxScoreMove.Clear();
                    maxScoreMove.Add(move);
                }
            }

            return maxScoreMove.First();
        }

        public void SetWin()
        {
            Console.WriteLine("Unbeatable Player Strikes again...");
        }
    }
}