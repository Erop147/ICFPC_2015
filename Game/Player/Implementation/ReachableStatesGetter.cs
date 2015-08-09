using System;
using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using System.Linq;

namespace ICFPC2015.Player.Implementation
{
    public static class ReachableStatesGetter
    {
        private readonly static IEnumerable<Command> Commands = Enum.GetValues(typeof(Command)).Cast<Command>().Except(new[] { Command.Empty });

        public static GameUnit[] Get(Board board, GameUnit unit, bool onlyLocked, HashSet<GameUnit> usedUnits = null)
        {
            var newlyUsedUnits = new HashSet<GameUnit>();
            var lockedUnits = onlyLocked ? new HashSet<GameUnit>() : null;
            Dfs(board, unit, newlyUsedUnits, usedUnits ?? new HashSet<GameUnit>(), lockedUnits);

            return onlyLocked
                ? lockedUnits.ToArray()
                : newlyUsedUnits.ToArray();
        }

        public static bool CanReach(Board board, GameUnit unit, bool onlyLocked, HashSet<GameUnit> usedUnits, GameUnit finishUnit)
        {
            var newlyUsedUnits = new HashSet<GameUnit>();
            var lockedUnits = onlyLocked ? new HashSet<GameUnit>() : null;
            return Dfs2(board, unit, newlyUsedUnits, usedUnits ?? new HashSet<GameUnit>(), lockedUnits, finishUnit);
        }

        private static bool Dfs2(Board board, GameUnit unit, HashSet<GameUnit> newlyUsedUnits, HashSet<GameUnit> previouslyUsedUnits, HashSet<GameUnit> lockedUnits, GameUnit finishUnit)
        {
            if (unit.Equals(finishUnit))
                return true;
            newlyUsedUnits.Add(unit);

            foreach (var command in Commands)
            {
                var nextUnit = unit.MakeStep(command);
                if (!previouslyUsedUnits.Contains(nextUnit) && !newlyUsedUnits.Contains(nextUnit))
                {
                    if (board.IsValid(nextUnit) && Dfs2(board, nextUnit, newlyUsedUnits, previouslyUsedUnits, lockedUnits, finishUnit))
                    {
                        return true;
                    }
                    else if (lockedUnits != null)
                    {
                        lockedUnits.Add(unit);
                    }
                }
            }
            return false;
        }

        private static void Dfs(Board board, GameUnit unit, HashSet<GameUnit> newlyUsedUnits, HashSet<GameUnit> previouslyUsedUnits, HashSet<GameUnit> lockedUnits)
        {
            newlyUsedUnits.Add(unit);

            foreach (var command in Commands)
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