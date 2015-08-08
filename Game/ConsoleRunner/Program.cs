using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ICFPC2015.GameLogic.Logic;
using ICFPC2015.GameLogic.Logic.Output;
using ICFPC2015.Player.Implementation;
using ICFPC2015.Player.Players;

namespace ICFPC2015.ConsoleRunner
{
    public class Program
    {
        private static TimeSpan timeLimit;
        private static int memoryLimit;
        private static readonly List<string> filenames = new List<string>();
        private static readonly List<string> powerWords = new List<string>();
        private static IPlayer[] players;

        static void Main(string[] args)
        {
            players = new[]
            {
                new DummyGreedyPlayer(new GreedyPowerWordCommandStringGenerator(), new BottomLeftPositionFinder()),
                new DummyGreedyPlayer(new GreedyPowerWordCommandStringGenerator(), new PaverPositionFinder()),
            };

            for (int i = 0; i < args.Length; i ++)
            {
                if (args[i] == "-f")
                {
                    filenames.Add(args[i + 1]);
                    i++;
                }
                else if (args[i] == "-t")
                {
                    timeLimit = TimeSpan.FromSeconds(int.Parse(args[i + 1]));
                    i++;
                }
                else if (args[i] == "-m")
                {
                    memoryLimit = int.Parse(args[i + 1]);
                    i++;
                }
                else if (args[i] == "-p")
                {
                    powerWords.Add(args[i + 1]);
                    i++;
                }
            }

            var outputs = new List<Output>();
            foreach (var filename in filenames)
            {
                var games = new GameBuilder().Build(filename);
                foreach (var game in games)
                {
                    var best = -1;
                    var playerName = string.Empty;
                    var commands = string.Empty;

                    var playGameTasks = players.Select(player => Play(game, player)).ToArray();
                    for (int i = 0; i < playGameTasks.Length; i++)
                    {
                        var playResult = playGameTasks[i].Result;
                        if (playResult.Score > best)
                        {
                            best = playResult.Score;
                            playerName = playResult.PlayerName;
                            commands = playResult.Commands;
                        }
                    }

                    outputs.Add(new Output
                    {
                        tag = playerName,
                        seed = game.Seed,
                        problemId = game.ProblemId,
                        solution = commands
                    });
                }
            }

            var result = new OutputWriter().GenWriteString(outputs.ToArray());
            Console.WriteLine(result);
        }

        private static Task<PlayedGameInfo> Play(Game game, IPlayer player)
        {
            return Task.Run(() => player.Play(game));
        }
    }
}
