using System;
using System.Collections.Generic;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public class GameHistoryValidator
    {
        public HistoryValidateResult Validate(Game game, string commands)
        {
            var usedPositions = new Dictionary<int, HashSet<string>>();
            for (var i = 0; i <= game.UnitsSequence.Length; i ++)
            {
                usedPositions.Add(i, new HashSet<string>());
            }

            InsertPosition(usedPositions, game);

            for (var i = 0; i < commands.Length; i ++)
            {
                game = game.TryMakeStep(commands[i]);

                if (game.State == GameState.GameOver && i != commands.Length - 1)
                {
                    return new HistoryValidateResult
                    {
                        IsValid = false,
                        WrongMoveNumber = i + 1,
                        Reason = "Game is over"
                    };
                }
                if (game.State == GameState.GameOver && i == commands.Length - 1)
                {
                    return new HistoryValidateResult
                    {
                        IsValid = true
                    };
                }

                if (IsPositionUsed(usedPositions, game))
                {
                    return new HistoryValidateResult
                    {
                        IsValid = false,
                        WrongMoveNumber = i,
                        Reason = "Position is already used"
                    };
                }

                InsertPosition(usedPositions, game);
            }

            if (game.State != GameState.GameOver)
            {
                return new HistoryValidateResult
                {
                    IsValid = false,
                    WrongMoveNumber = null,
                    Reason = "Game is not over"
                };
            }
            
            throw new Exception("Something strange");
        }

        private static void InsertPosition(Dictionary<int, HashSet<string>> usedPositions, Game game)
        {
            usedPositions[game.CurrentUnitNumber].Add(GetPositionString(game));
        }

        private static bool IsPositionUsed(Dictionary<int, HashSet<string>> usedPosition, Game game)
        {
            return usedPosition[game.CurrentUnitNumber].Contains(GetPositionString(game));
        }

        private static string GetPositionString(Game game)
        {
            var points = game.Current.GetAbsolutePoints();
            return string.Join(";", points.OrderBy(x => x.Col).ThenBy(x => x.Row).Concat(new [] { game.Current.UnitPosition.PivotLocation }).Select(x => x.ToString()));
        }
    }
}