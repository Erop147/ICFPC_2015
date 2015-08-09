using System;
using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using System.Linq;

namespace ICFPC2015.Player.Implementation
{
    public static class ReachableStatesGetter
    {
        public static GameUnit[] Get(Board board, GameUnit unit, bool onlyLocked, HashSet<GameUnit> usedUnits = null)
        {
            var newlyUsedUnits = new HashSet<GameUnit>();
            var lockedUnits = onlyLocked ? new HashSet<GameUnit>() : null;
            Dfs(board, unit, newlyUsedUnits, usedUnits ?? new HashSet<GameUnit>(), lockedUnits);

            return onlyLocked ? lockedUnits.ToArray() : newlyUsedUnits.ToArray();
        }

        private static void Dfs(Board board, GameUnit unit, HashSet<GameUnit> newlyUsedUnits, HashSet<GameUnit> previouslyUsedUnits, HashSet<GameUnit> lockedUnits)
        {
            newlyUsedUnits.Add(unit);

            foreach (var command in Enum.GetValues(typeof(Command)).Cast<Command>().Except(new [] { Command.Empty }))
            {
                var nextUnit = unit.MakeStep(command);
                if (!previouslyUsedUnits.Contains(nextUnit) && !newlyUsedUnits.Contains(nextUnit))
                {
                    if (board.IsValid(nextUnit))
                    {
                        Dfs(board, nextUnit, newlyUsedUnits, previouslyUsedUnits, lockedUnits);
                    }
                    else if (lockedUnits != null)
                    {
                        lockedUnits.Add(unit);
                    }
                }
            }
        }
    }
}