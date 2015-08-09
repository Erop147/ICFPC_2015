using System.Text;
using ICFPC2015.GameLogic.Implementation;
using ICFPC2015.GameLogic.Logic;
using ICFPC2015.Player.Implementation;

namespace ICFPC2015.Player.Players
{
    public class DummyGreedyPlayer : IPlayer
    {
        private readonly ICommandStringGenerator commandStringGenerator;
        private readonly IBestPositionFinder bestPositionFinder;

        public DummyGreedyPlayer(ICommandStringGenerator commandStringGenerator, IBestPositionFinder bestPositionFinder)
        {
            this.commandStringGenerator = commandStringGenerator;
            this.bestPositionFinder = bestPositionFinder;
        }

        public PlayedGameInfo Play(Game game)
        {
            var stringBuilder = new StringBuilder();

            while (game.State != GameState.GameOver && game.State != GameState.Error)
            {
                if (TimeLimiter.NeedStop())
                    break;

                var gameUnits = ReachableStatesGetter.Get(game.Board, game.Current, true);

                var finishUnit = bestPositionFinder.FindBest(gameUnits, game);

                var commandString = commandStringGenerator.Generate(game.Board, game.Current, finishUnit);
                stringBuilder.Append(commandString);

                for (var i = 0; i < commandString.Length; i++)
                {
                    var command = commandString[i];
                    game = game.TryMakeStep(command);
                }
            }

            return new PlayedGameInfo(stringBuilder.ToString(), game.Score, bestPositionFinder.Name);
        }
    }
}