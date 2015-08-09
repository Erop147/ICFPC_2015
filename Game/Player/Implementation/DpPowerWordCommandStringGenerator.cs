using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public class DpPowerWordCommandStringGenerator : ICommandStringGenerator
    {
        private Dictionary<State, DpInfo> dp;
        private Dictionary<Tuple<GameUnit, string>, GameUnit> moves;
        private GameUnit target;
        private string[] words;
        private Board board;
        private static readonly string[] HorizontalWords = new[] { CommandConverter.CovertToAnyChar(Command.MoveEast).ToString(), CommandConverter.CovertToAnyChar(Command.MoveWest).ToString(), CommandConverter.CovertToAnyChar(Command.TurnClockWise).ToString(), CommandConverter.CovertToAnyChar(Command.TurnCounterClockWise).ToString() };

        private static readonly IEnumerable<string> SpecialWords = Enum.GetValues(typeof(Command))
                                                                       .Cast<Command>()
                                                                       .Where(x => x != Command.MoveEast && x != Command.MoveWest && x != Command.Empty && x != Command.TurnClockWise && x != Command.TurnCounterClockWise)
                                                                       .Select(x => CommandConverter.CovertToAnyChar(x).ToString());

        public string Generate(Board _board, GameUnit _unit, GameUnit _finishUnit)
        {
            board = _board;
            target = _finishUnit;
            moves = new Dictionary<Tuple<GameUnit, string>, GameUnit>();
            words = MagicWordsStore.Words.Concat(SpecialWords).OrderByDescending(x => x.Length).ToArray();
            dp = new Dictionary<State, DpInfo>();

            var state = new State(_unit, Command.Empty, false, true);
            CalcDp(state);
            var stringBuilder = new StringBuilder();

            while (!(state.Unit.Equals(target) && state.IsLocked))
            {
                var dpInfo = dp[state];
                stringBuilder.Append(dpInfo.Word);
                state = dpInfo.NextState.Value;
            }

            return stringBuilder.ToString();
        }

        public DpInfo CalcDp(State state)
        {
            if (dp.ContainsKey(state))
            {
                return dp[state];
            }
            if (state.Unit.Equals(target) && state.IsLocked)
            {
                return dp[state] = new DpInfo(0, null, string.Empty, 0);
            }
            if (state.IsLocked)
            {
                return dp[state] = new DpInfo(-1, null, string.Empty, 0);
            }
            if (state.Unit.Equals(target))
            {
                foreach (var command in Enum.GetValues(typeof(Command)).Cast<Command>())
                {
                    GameUnit nextUnit;
                    var word = CommandConverter.CovertToAnyChar(command).ToString();
                    if (!CanMove(state.Unit, word, out nextUnit))
                    {
                        return dp[state] = new DpInfo(0, new State(state.Unit, command, true, true), word, 0);
                    }
                }
                return dp[state] = new DpInfo(-1, null, string.Empty, 0);
            }

            if (!state.CanGoHorisontal)
            {
                var best = new DpInfo(-1, null, string.Empty, 0);
                dp[state] = best;

                foreach (var word in words)
                {
                    GameUnit nextUnit;
                    if (AvailablePair(state.LastCommand, CommandConverter.Convert(word.First())) && CanMove(state.Unit, word, out nextUnit))
                    {
                        var nextState = new State(nextUnit, CommandConverter.Convert(word.Last()), false, true);
                        var dpInfo = CalcDp(nextState);
                        if (dpInfo.Value < 0)
                        {
                            continue;
                        }

                        var newValue = dpInfo.Value + (word.Length == 1 ? 0 : word.Length);
                        if (newValue > best.Value || (newValue == best.Value && dpInfo.ExtraMoves < best.ExtraMoves))
                        {
                            best = new DpInfo(newValue, nextState, word, dpInfo.ExtraMoves);
                        }
                    }
                }

                return dp[state] = best;
            }

            var result = CalcDp(new State(state.Unit, state.LastCommand, state.IsLocked, false));
            dp[state] = result;

            foreach (var horizontalWord in HorizontalWords)
            {
                //ответ для некоторых элементов компоненты сильной связанности может быть меньше, чем надо. Может еще допилю. Вдруг не критично
                GameUnit nextUnit;
                if (AvailablePair(state.LastCommand, CommandConverter.Convert(horizontalWord.First())) && CanMove(state.Unit, horizontalWord, out nextUnit))
                {
                    var nextState = new State(nextUnit, CommandConverter.Convert(horizontalWord.Last()), false, true);
                    var dpInfo = CalcDp(nextState);
                    if (dpInfo.Value < 0)
                    {
                        continue;
                    }

                    var newValue = dpInfo.Value;
                    if (newValue > result.Value || (newValue == result.Value && dpInfo.ExtraMoves + 1 < result.ExtraMoves))
                    {
                        result = new DpInfo(newValue, nextState, horizontalWord, dpInfo.ExtraMoves + 1);
                    }
                }
            }

            return dp[state] = result;
        }

        private bool AvailablePair(Command lastCommand, Command newCommand)
        {
            if (lastCommand == Command.MoveEast && newCommand == Command.MoveWest) return false;
            if (lastCommand == Command.MoveWest && newCommand == Command.MoveEast) return false;
            if (lastCommand == Command.TurnClockWise && newCommand == Command.TurnCounterClockWise) return false;
            if (lastCommand == Command.TurnCounterClockWise && newCommand == Command.TurnClockWise) return false;
            return true;
        }

        private bool CanMove(GameUnit unit, string word, out GameUnit nextUnit)
        {
            var tuple = new Tuple<GameUnit, string>(unit, word);
            if (moves.TryGetValue(tuple, out nextUnit))
            {
                return nextUnit != null;
            }

            var gameUnits = new HashSet<GameUnit>();
            gameUnits.Add(unit);
            nextUnit = unit;
            foreach (var command in word)
            {
                nextUnit = nextUnit.MakeStep(CommandConverter.Convert(command));
                if (gameUnits.Contains(nextUnit) || !board.IsValid(nextUnit))
                {
                    moves[tuple] = null;
                    return false;
                }
                gameUnits.Add(nextUnit);
            }

            moves[tuple] = nextUnit;
            return true;
        }
    }
}