using System.Linq;
using System.Collections.Generic;

namespace ICFPC2015.GameLogic.Logic
{
    public static class CommandConverter
    {
        private static readonly Dictionary<Command, string> CommandMappings = new Dictionary<Command, string>()
            {
                {Command.MoveWest, "p'!.03"},
                {Command.MoveEast, "bcefy2"},
                {Command.MoveSouthWest, "aghij4"},
                {Command.MoveSouthEast, "lmno 5"},
                {Command.TurnClockWise, "dqrvz1"},
                {Command.TurnCounterClockWise, "kstuwx"},
                {Command.Empty, "\t\n\r"},
            };
        private static IDictionary<char, Command> commandDictionary;
        private static IDictionary<char, Command> CommandDictionary
        {
            get
            {
                return commandDictionary ??
                       (commandDictionary =
                        CommandMappings.SelectMany(
                            mapping => mapping.Value.Select(x => new {Key = x, Value = mapping.Key}))
                                       .ToDictionary(x => x.Key, x => x.Value));
            }
        }

        public static Command Convert(char command)
        {
            return CommandDictionary[command];
        }

        public static char CovertToAnyChar(Command command)
        {
            return CommandMappings[command].First();
        }
    }
}