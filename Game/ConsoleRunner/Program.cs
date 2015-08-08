using System;
using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using ICFPC2015.GameLogic.Logic.Output;

namespace ICFPC2015.ConsoleRunner
{
    public class Program
    {
        private static TimeSpan timeLimit;
        private static int memoryLimit;
        private static List<string> filenames;
        private static List<string> powerWords;
        private static IPlayer[] players;

        static void Main(string[] args)
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
                    timeLimit = new TimeSpan(0, 0, int.Parse(args[i + 1]));
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
                    var answer = string.Empty;
                    for (int i = 0; i < players.Length; i ++)
                    {
                        var playResult = players[i].Play(game);
                        if (playResult.Score > best)
                        {
                            best = playResult.Score;
                            answer = playResult.Answer;
                        }
                    }

                    outputs.Add(new Output
                    {
                        tag = "push push",
                        seed = game.Seed,
                        problemId = game.ProblemId,
                        solution = answer
                    });
                }
            }

            var result = new OutputWriter().GenWriteString(outputs.ToArray());
            Console.WriteLine(result);
        }

        public interface IPlayer
        {
            PlayResult Play(Game game);
        }
    }

    public class PlayResult
    {
        public string Answer { get; set; }
        public int Score { get; set; }
    }
}
