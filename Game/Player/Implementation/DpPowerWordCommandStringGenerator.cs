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
        private GameUnit target;
        private string[] words;
        private Board board;
        private static readonly Command[] HorizontalCommands = new[] {Command.MoveEast, Command.MoveWest};
        private static readonly Command[] TurningCommands = new[] {Command.TurnClockWise, Command.TurnCounterClockWise};

        private static readonly IEnumerable<string> SpecialWords = Enum.GetValues(typeof(Command))
                                                                       .Cast<Command>()
                                                                       .Where(x => x != Command.MoveEast && x != Command.MoveWest && x != Command.Empty && x != Command.TurnClockWise && x != Command.TurnCounterClockWise)
                                                                       .Select(x => CommandConverter.CovertToAnyChar(x).ToString());

        public string Generate(Board _board, GameUnit _unit, GameUnit _finishUnit)
        {
            board = _board;
            target = _finishUnit;
            words = MagicWordsStore.Words.Concat(SpecialWords).OrderByDescending(x => x.Length).ToArray();
            dp = new Dictionary<State, DpInfo>();

            var state = new State { IsLocked = false, LastCommand = Command.Empty, Unit = _unit };
            CalcDp(state);
            var stringBuilder = new StringBuilder();

            while (!(state.Unit.Equals(target) && state.IsLocked))
            {
                stringBuilder.Append(dp[state].NextCommand);
                state = dp[state].NextState;
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
                return dp[state] = new DpInfo { Value = 0 };
            }
            if (state.IsLocked)
            {
                return dp[state] = new DpInfo { Value = -1 };
            }
            var best = new DpInfo { Value = -1 };

            foreach (var horizontalCommand in HorizontalCommands)
            {
                UpdateHorizontal(state, best, horizontalCommand);
            }

            return dp[state] = best;
        }

        private void UpdateHorizontal(State state, DpInfo best, Command horizontalCommand)
        {
            var shiftCount = 0;
            while (true)
            {
                foreach (var turningCommand in TurningCommands)
                {
                    UpdateTurning(state, best, horizontalCommand, shiftCount, turningCommand);
                }

                if (!CanGo(state.LastCommand, horizontalCommand))
                    break;

                state = MakeStep(state, horizontalCommand);
                shiftCount++;
                if (state.IsLocked)
                {
                    var cur = CalcDp(state);
                    if (cur.Value > best.Value)
                    {
                        best.NextState = state;
                        best.Value = cur.Value;
                        best.NextCommand = new NextCommandInfo
                        {
                            ShiftCommand = horizontalCommand,
                            ShiftCount = shiftCount
                        };
                    }
                    break;
                }
            }
        }

        private void UpdateTurning(State curState, DpInfo best, Command horizontalCommand, int shiftCount, Command turningCommand)
        {
            var turningUsedGameUnits = new HashSet<GameUnit>();
            for (int turningCount = 0; turningCount < 6; turningCount++)
            {
                turningUsedGameUnits.Add(curState.Unit);
                foreach (var word in words)
                {
                    UpdateWord(word, curState, best, horizontalCommand, shiftCount, turningCommand, turningCount);
                }

                if (!CanGo(curState.LastCommand, turningCommand))
                    break;

                curState = MakeStep(curState, turningCommand);

                if (!curState.IsLocked && turningUsedGameUnits.Contains(curState.Unit))
                {
                    break;
                }

                if (curState.IsLocked)
                {
                    var cur = CalcDp(curState);
                    if (cur.Value > best.Value)
                    {
                        best.NextState = curState;
                        best.Value = cur.Value;
                        best.NextCommand = new NextCommandInfo
                        {
                            ShiftCommand = horizontalCommand,
                            ShiftCount = shiftCount,
                            TurningCommand = turningCommand,
                            TurningCount = turningCount,
                        };
                    }
                    break;
                }
            }
        }

        private void UpdateWord(string word, State curState, DpInfo best, Command horizontalCommand, int shiftCount, Command turningCommand, int turningCount)
        {
            var newlyUsedGameUnits = new HashSet<GameUnit>();
            if (CanGo(curState.LastCommand, word[0]))
            {
                var newState = curState;
                DpInfo cur;
                bool fail = false;
                for (int i = 0; i < word.Length; i++)
                {
                    newlyUsedGameUnits.Add(newState.Unit);
                    newState = MakeStep(newState, word[i]);
                    if (!newState.IsLocked && newlyUsedGameUnits.Contains(newState.Unit))
                    {
                        fail = true;
                        break;
                    }
                    if (newState.IsLocked)
                    {
                        fail = true;
                        cur = CalcDp(newState);
                        if (cur.Value > best.Value)
                        {
                            best.NextState = newState;
                            best.Value = cur.Value;
                            best.NextCommand = new NextCommandInfo
                            {
                                ShiftCommand = horizontalCommand,
                                ShiftCount = shiftCount,
                                TurningCommand = turningCommand,
                                TurningCount = turningCount,
                                Word = word,
                                WordPrefix = i + 1
                            };
                        }
                        break;
                    }
                }
                if (!fail)
                {
                    cur = CalcDp(newState);
                    if (cur.Value > best.Value)
                    {
                        best.NextState = newState;
                        best.Value = cur.Value;
                        best.NextCommand = new NextCommandInfo
                        {
                            ShiftCommand = horizontalCommand,
                            ShiftCount = shiftCount,
                            TurningCommand = turningCommand,
                            TurningCount = turningCount,
                            Word = word,
                            WordPrefix = word.Length
                        };
                    }
                }
            }
        }

        private bool CanGo(Command lastCommand, char ch)
        {
            return CanGo(lastCommand, CommandConverter.Convert(ch));
        }

        private bool CanGo(Command lastCommand, Command newCommand)
        {
            if (lastCommand == Command.MoveEast && newCommand == Command.MoveWest)
                return false;
            if (lastCommand == Command.MoveWest && newCommand == Command.MoveEast)
                return false;
            return true;
        }

        private State MakeStep(State state, char ch)
        {
            return MakeStep(state, CommandConverter.Convert(ch));
        }

        private State MakeStep(State state, Command command)
        {
            if (state.IsLocked)
            {
                throw new Exception("State is locked");
            }
            var newUnit = state.Unit.MakeStep(command);
            if (!board.IsValid(newUnit))
            {
                return new State {IsLocked = true, LastCommand = command, Unit = state.Unit};
            }
            return new State {IsLocked = false, LastCommand = command, Unit = newUnit};
        }

        public struct State
        {
            public GameUnit Unit { get; set; }
            public Command LastCommand { get; set; }
            public bool IsLocked { get; set; }

            public override int GetHashCode()
            {
                unchecked
                {

                    var hashCode = (Unit != null ? Unit.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int) LastCommand;
                    hashCode = (hashCode * 397) ^ IsLocked.GetHashCode();
                    return hashCode;
                }
            }

            public bool Equals(State other)
            {
                return Unit.Equals(other.Unit) && LastCommand == other.LastCommand && IsLocked == other.IsLocked;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is State && Equals((State) obj);
            }
        }

        public class DpInfo
        {
            public int Value { get; set; }
            public State NextState { get; set; }
            public NextCommandInfo NextCommand { get; set; }
        }

        public class NextCommandInfo
        {
            public Command? ShiftCommand { get; set; }
            public Command? TurningCommand { get; set; }
            public int ShiftCount { get; set; }
            public int TurningCount { get; set; }
            public string Word { get; set; }
            public int WordPrefix { get; set; }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}",
                    ShiftCommand.HasValue
                        ? new string(Enumerable.Repeat(CommandConverter.CovertToAnyChar(ShiftCommand.Value), ShiftCount).ToArray())
                        : string.Empty,
                    TurningCommand.HasValue
                        ? new string(Enumerable.Repeat(CommandConverter.CovertToAnyChar(TurningCommand.Value), TurningCount).ToArray())
                        : string.Empty,
                    Word == null
                        ? string.Empty
                        : Word.Substring(0, WordPrefix));
            }
        }
    }
}