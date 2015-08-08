using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public class GreedyPowerWordCommandStringGenerator : ICommandStringGenerator
    {
        public string Generate(Board board, GameUnit unit, UnitPosition finishPosition)
        {
            var stringBuilder = new StringBuilder();
            var words =
                MagicWordsStore.Words.Concat(
                    Enum.GetValues(typeof (Command))
                        .Cast<Command>()
                        .Select(x => CommandConverter.CovertToAnyChar(x).ToString()))
                        .ToArray();
            var usedPositions = new HashSet<UnitPosition>();
            while (!unit.UnitPosition.Equals(finishPosition))
            {
                foreach (var powerWord in words.OrderByDescending(x => x.Length))
                {
                    var newlyUsedPositions = new HashSet<UnitPosition>();
                    var currentUnit = unit;
                    var fail = false;
                    for (var i = 0; i < powerWord.Length; ++i)
                    {
                        var command = powerWord[i];
                        newlyUsedPositions.Add(currentUnit.UnitPosition);
                        var nextUnit = currentUnit.MakeStep(CommandConverter.Convert(command));
                        var locked = !board.IsValid(nextUnit);
                        if (newlyUsedPositions.Contains(nextUnit.UnitPosition) ||
                            usedPositions.Contains(nextUnit.UnitPosition) ||
                            (locked && i < powerWord.Length - 1))
                        {
                            fail = true;
                            break;
                        }
                        if (!locked)
                        {
                            currentUnit = nextUnit;
                        }
                    }
                    var allUsedPositions = new HashSet<UnitPosition>(usedPositions.Union(newlyUsedPositions));
                    if (!fail && ReachableStatesGetter.Get(board, currentUnit, false, allUsedPositions).Contains(finishPosition))
                    {
                        unit = currentUnit;
                        usedPositions = allUsedPositions;
                        stringBuilder.Append(powerWord);
                        break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}