using System;
using System.Collections.Generic;
using System.Linq;
using ICFPC2015.GameLogic.Logic.Input;

namespace ICFPC2015.GameLogic.Logic
{
    public class GameBuilder
    {
        public Game[] Build(string inputFilename)
        {
            var games = new List<Game>();

            var input = new InputReader().Read(inputFilename);

            var board = Board.CreateEmpty(input.height, input.width);
            foreach (var busyCell in input.filled)
            {
                board.Fill(busyCell.ToPoint());
            }

            var units = input.units.Select(x => x.ToUnit()).ToArray();
            foreach (var seed in input.sourceSeeds)
            {
                var generator = new RandomGenerator(seed);
                var unitSequence = generator.Generate().Select(x => units[x % units.Length]).Take(input.sourceLength).ToArray();

                var game = new Game(board, null, unitSequence, 0, 0, 0, input.id, seed);
                var spawnResult = game.TrySpawnNew();
                if (spawnResult.Result != StepResult.NewIsSpawned)
                {
                    throw new Exception("Can't spawn first unit");
                }
                game = spawnResult.Game;

                games.Add(game);
            }

            return games.ToArray();
        }
    }
}