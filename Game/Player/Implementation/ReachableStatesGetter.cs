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
            Dfs(board, unit, newlyUsedPositions, usedPositions ?? new HashSet<UnitPosition>(), onlyLocked ? new HashSet<UnitPosition>() : null);

            return newlyUsedPositions.ToArray();
        }

        private static void Dfs(Board board, GameUnit unit, HashSet<UnitPosition> newlyUsedPositions, HashSet<UnitPosition> previouslyUsedPositions, HashSet<UnitPosition> lockedPositions = null)
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
                        Dfs(board, nextUnit, newlyUsedPositions, previouslyUsedPositions);
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