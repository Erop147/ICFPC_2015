using System;
using System.Linq;
using System.Collections.Generic;

namespace ICFPC2015.GameLogic.Logic
{
    public static class CommandConverter
    {
        private static readonly List<Tuple<string, Command>> CommandMappings = new List<Tuple<string, Command>>()
            {
                new Tuple<string, Command>("p'!.03", Command.MoveWest),
                new Tuple<string, Command>("bcefy2", Command.MoveEast),
                new Tuple<string, Command>("aghij4", Command.MoveSouthWest),
                new Tuple<string, Command>("lmno 5", Command.MoveSouthEast),
                new Tuple<string, Command>("dqrvz1", Command.TurnClockWise),
                new Tuple<string, Command>("kstuwx", Command.TurnCounterClockWise),
            };
        private static IDictionary<char, Command> commandDictionary;
        private static IDictionary<char, Command> CommandDictionary
        {
            get
            {
                return commandDictionary ??
                       (commandDictionary =
                        CommandMappings.SelectMany(
                            mapping => mapping.Item1.Select(x => new {Key = x, Value = mapping.Item2}))
                                       .ToDictionary(x => x.Key, x => x.Value));
            }
        }

        public static Command Convert(char command)
        {
            return CommandDictionary[command];
        }
    }
}