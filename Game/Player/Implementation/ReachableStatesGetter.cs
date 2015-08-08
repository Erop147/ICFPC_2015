using System;
using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using System.Linq;

namespace ICFPC2015.Player.Implementation
{
    public static class ReachableStatesGetter
    {
        public static UnitPosition[] Get(Board board, GameUnit unit, bool onlyLocked, HashSet<UnitPosition> usedPositions = null)
        {
            var newlyUsedPositions = new HashSet<UnitPosition>();
            var lockedPositions = onlyLocked ? new HashSet<UnitPosition>() : null;
            Dfs(board, unit, newlyUsedPositions, usedPositions ?? new HashSet<UnitPosition>(), lockedPositions);

            return onlyLocked ? lockedPositions.ToArray() : newlyUsedPositions.ToArray();
        }

        private static void Dfs(Board board, GameUnit unit, HashSet<UnitPosition> newlyUsedPositions, HashSet<UnitPosition> previouslyUsedPositions, HashSet<UnitPosition> lockedPositions)
        {
            newlyUsedPositions.Add(unit.UnitPosition);

            foreach (var command in Enum.GetValues(typeof(Command)).Cast<Command>())
            {
                var nextUnit = unit.MakeStep(command);
                if (!previouslyUsedPositions.Contains(nextUnit.UnitPosition) &&
                    !newlyUsedPositions.Contains(nextUnit.UnitPosition))
                {
                    if (board.IsValid(nextUnit))
                    {
                        Dfs(board, nextUnit, newlyUsedPositions, previouslyUsedPositions, lockedPositions);
                    }
                    else if (lockedPositions != null)
                    {
                        lockedPositions.Add(unit.UnitPosition);
                    }
                }
            }
        }
    }
}