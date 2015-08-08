using System;
using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using System.Linq;

namespace ICFPC2015.Player.Implementation
{
    public static class ReachableStatesGetter
    {
        public static UnitPosition[] Get(Board board, GameUnit unit)
        {
            var used = new HashSet<UnitPosition>();
            Dfs(board, unit, used);

            return used.ToArray();
        }

        private static void Dfs(Board board, GameUnit unit, HashSet<UnitPosition> used)
        {
            used.Add(unit.UnitPosition);

            foreach (var command in Enum.GetValues(typeof(Command)).Cast<Command>())
            {
                var nextUnit = unit.MakeStep(command);
                if (!used.Contains(nextUnit.UnitPosition) && board.IsValid(nextUnit))
                {
                    Dfs(board, nextUnit, used);
                }
            }
        }
    }
}