using System;
using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using System.Linq;

namespace ICFPC2015.Player.Implementation
{
    public static class ReachableStatesGetter
    {
        public static GameUnit[] Get(Board board, GameUnit unit, bool onlyLocked, HashSet<GameUnit> usedPositions = null)
        {
            var newlyUsedPositions = new HashSet<GameUnit>();
            var lockedPositions = onlyLocked ? new HashSet<GameUnit>() : null;
            Dfs(board, unit, newlyUsedPositions, usedPositions ?? new HashSet<GameUnit>(), lockedPositions);

            return onlyLocked ? lockedPositions.ToArray() : newlyUsedPositions.ToArray();
        }

        private static void Dfs(Board board, GameUnit unit, HashSet<GameUnit> newlyUsedPositions, HashSet<GameUnit> previouslyUsedPositions, HashSet<GameUnit> lockedPositions)
        {
            newlyUsedPositions.Add(unit);

            foreach (var command in Enum.GetValues(typeof(Command)).Cast<Command>().Except(new [] { Command.Empty }))
            {
                var nextUnit = unit.MakeStep(command);
                if (!previouslyUsedPositions.Contains(nextUnit) &&
                    !newlyUsedPositions.Contains(nextUnit))
                {
                    if (board.IsValid(nextUnit))
                    {
                        Dfs(board, nextUnit, newlyUsedPositions, previouslyUsedPositions, lockedPositions);
                    }
                    else if (lockedPositions != null)
                    {
                        lockedPositions.Add(unit);
                    }
                }
            }
        }
    }
}