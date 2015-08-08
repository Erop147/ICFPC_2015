using System;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.ConsoleRunner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new GameBuilder().Build(@"Problems\problem_0.json")[0];

            var w = "ei!";
            var commands = new[] {Command.MoveEast, Command.MoveSouthWest, Command.MoveWest};
            for (int i = 0;; i ++)
            {
                Console.Write(w[i % 3]);
                var command = commands[i % 3];
                var result = game.TryMakeStep(command);
                game = result.Game;
                if (result.Result == StepResult.GameOver)
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.WriteLine(game.Score);
        }
    }
}
