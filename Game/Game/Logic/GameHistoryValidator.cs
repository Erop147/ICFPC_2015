﻿using System.Collections.Generic;

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

            return new HistoryValidateResult {IsValid = true};
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
            return game.Current.ToCoordinatesString();
        }
    }
}