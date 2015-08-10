using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ICFPC2015.GameLogic.Implementation;
using ICFPC2015.GameLogic.Logic;
using ICFPC2015.GameLogic.Logic.Output;
using ICFPC2015.Player.Implementation;
using ICFPC2015.Player.Players;

namespace ICFPC2015.ConsoleRunner
{
    public class Program
    {
        private static int timeLimit;
        private static int memoryLimit;
        private static int coresCount;
        private static readonly List<string> filenames = new List<string>();
        private static readonly List<string> powerWords = new List<string>();
        private static IPlayer[] players;

        private static IEnumerable<IPlayer> GetOrderedPlayers()
        {
            yield return new DummyGreedyPlayer(new GreedyPowerWordCommandStringGenerator(), new PaverPositionFinder());
            yield return new DummyGreedyPlayer(new DpPowerWordCommandStringGenerator(), new PaverPositionFinder());
            yield return new DummyGreedyPlayer(new GreedyPowerWordCommandStringGenerator(), new BottomPositionFinder());
            yield return new DummyGreedyPlayer(new DpPowerWordCommandStringGenerator(), new BottomPositionFinder());
            yield return new DummyGreedyPlayer(new GreedyPowerWordCommandStringGenerator(), new BottomLeftPositionFinder());
            yield return new DummyGreedyPlayer(new DpPowerWordCommandStringGenerator(), new BottomLeftPositionFinder());
        }

        static void Main(string[] args)
        {
            //TODO прибить перед паблишем
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            ParseArguments(args);

            TimeLimiter.Start(TimeSpan.FromSeconds(timeLimit));
            MagicWordsStore.AddWords(powerWords);
            players = timeLimit == 0 && memoryLimit == 0
                ? GetOrderedPlayers().ToArray()
                : (coresCount == 0 ? GetOrderedPlayers().ToArray() : GetOrderedPlayers().Take(coresCount).ToArray());

            var outputs = new List<Output>();
            foreach (var filename in filenames)
            {
                if (TimeLimiter.NeedStop())
                    break;

                var games = new GameBuilder().Build(filename);
                foreach (var game in games)
                {
                    if (TimeLimiter.NeedStop())
                        break;

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

                    Console.WriteLine("Score = {0}", best);
                    if (!new GameHistoryValidator().Validate(game, commands).IsValid)
                    {
                        Console.WriteLine("Not Valid");
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

            //TODO прибить перед паблишем
            stopwatch.Stop();
            Console.WriteLine("Done in {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static void ParseArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i ++)
            {
                if (args[i] == "-f")
                {
                    filenames.Add(args[i + 1]);
                    i++;
                }
                else if (args[i] == "-t")
                {
                    timeLimit = int.Parse(args[i + 1]);
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
                else if (args[i] == "-c")
                {
                    coresCount = int.Parse(args[i + 1]);
                    i++;
                }

                if (coresCount == 0)
                {
                    coresCount = Environment.ProcessorCount;
                }
            }
        }

        private static Task<PlayedGameInfo> Play(Game game, IPlayer player)
        {
            return Task.Run(() => player.Play(game));
        }
    }
}
