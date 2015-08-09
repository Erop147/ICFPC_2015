﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICFPC2015.GameLogic.Implementation;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public class GreedyPowerWordCommandStringGenerator : ICommandStringGenerator
    {
        private static readonly IEnumerable<string> SpecialWords = Enum.GetValues(typeof (Command))
                                                       .Cast<Command>()
                                                       .Select(x => CommandConverter.CovertToAnyChar(x).ToString());

        public string Generate(Board board, GameUnit unit, GameUnit finishUnit)
        {
            var stringBuilder = new StringBuilder();
            var words = MagicWordsStore.Words
                                       .Concat(SpecialWords)
                                       .ToArray();
            var usedUnits = new HashSet<GameUnit>();
            while (!unit.Equals(finishUnit))
            {
                if (TimeLimiter.NeedStop())
                    break;

                foreach (var powerWord in words.OrderByDescending(x => x.Length))
                {
                    if (TimeLimiter.NeedStop())
                        break;

                    var newlyUsedUnits = new HashSet<GameUnit>();
                    var currentUnit = unit;
                    var fail = false;
                    for (var i = 0; i < powerWord.Length; ++i)
                    {
                        var command = powerWord[i];
                        newlyUsedUnits.Add(currentUnit);
                        var nextUnit = currentUnit.MakeStep(CommandConverter.Convert(command));
                        var locked = !board.IsValid(nextUnit);
                        if (newlyUsedUnits.Contains(nextUnit) ||
                            usedUnits.Contains(nextUnit) ||
                            (locked && i < powerWord.Length - 1) ||
                            (locked && !nextUnit.Equals(finishUnit)))
                        {
                            fail = true;
                            break;
                        }
                        if (!locked)
                        {
                            currentUnit = nextUnit;
                        }
                    }
                    var allUsedUnits = new HashSet<GameUnit>(usedUnits.Union(newlyUsedUnits));
                    if (!fail && ReachableStatesGetter.CanReach(board, currentUnit, false, allUsedUnits, finishUnit))
                    {
                        unit = currentUnit;
                        usedUnits = allUsedUnits;
                        stringBuilder.Append(powerWord);
                        break;
                    }
                }
            }
            foreach (var command in Enum.GetValues(typeof(Command)).Cast<Command>().Except(new[] { Command.Empty }))
            {
                if (!board.IsValid(unit.MakeStep(command)))
                {
                    stringBuilder.Append(CommandConverter.CovertToAnyChar(command));
                    break;
                }
            }
            return stringBuilder.ToString();
        }
    }
}