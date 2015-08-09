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
            var newlyUsedUnits = new HashSet<Position>();
            var lockedUnits = onlyLocked ? new HashSet<GameUnit>() : null;
            Dfs(board, unit, newlyUsedUnits, usedUnits ?? new HashSet<GameUnit>(), lockedUnits);

            return onlyLocked
                ? lockedUnits.ToArray()
                : newlyUsedUnits.Select(x => new GameUnit(unit.Unit, new UnitPosition(new Point(x.Col, x.Row), x.Rotation))).ToArray();
        }

        public static bool CanReach(Board board, GameUnit unit, bool onlyLocked, HashSet<GameUnit> usedUnits, GameUnit finishUnit)
        {
            var newlyUsedUnits = new HashSet<Position>();
            var lockedUnits = onlyLocked ? new HashSet<GameUnit>() : null;
            return Dfs2(board, unit, newlyUsedUnits, usedUnits ?? new HashSet<GameUnit>(), lockedUnits, finishUnit);
        }

        private static bool Dfs2(Board board, GameUnit unit, HashSet<Position> newlyUsedUnits, HashSet<GameUnit> previouslyUsedUnits, HashSet<GameUnit> lockedUnits, GameUnit finishUnit)
        {
            if (unit.Equals(finishUnit))
                return true;
            newlyUsedUnits.Add(ToPosition(unit));

            foreach (var command in Commands)
            {
                var nextUnit = unit.MakeStep(command);
                if (!previouslyUsedUnits.Contains(nextUnit) && !newlyUsedUnits.Contains(ToPosition(nextUnit)))
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

        private static void Dfs(Board board, GameUnit unit, HashSet<Position> newlyUsedUnits, HashSet<GameUnit> previouslyUsedUnits, HashSet<GameUnit> lockedUnits)
        {
            newlyUsedUnits.Add(ToPosition(unit));

            foreach (var command in Commands)
            {
                var nextUnit = unit.MakeStep(command);
                if (!previouslyUsedUnits.Contains(nextUnit) && !newlyUsedUnits.Contains(ToPosition(nextUnit)))
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

        private static Position ToPosition(GameUnit unit)
        {
            return new Position(unit.UnitPosition.PivotLocation.Col, unit.UnitPosition.PivotLocation.Row, unit.UnitPosition.RotationCount);
        }

        private struct Position
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public int Rotation { get; set; }

            public Position(int col, int row, int rotation) : this()
            {
                Row = row;
                Col = col;
                Rotation = rotation;
            }

            public override int GetHashCode()
            {
                return Row ^ (Col * 113) ^ (Rotation * 157);
            }
        }
    }
}