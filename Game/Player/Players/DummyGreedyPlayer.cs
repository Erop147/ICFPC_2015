using System.Text;
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
                var unitPositions = ReachableStatesGetter.Get(game.Board, game.Current);

                var finishPosition = bestPositionFinder.FindBest(unitPositions, game);

                var commandString = commandStringGenerator.Generate(game.Board, game.Current, finishPosition);
                stringBuilder.Append(commandString);

                foreach (var command in commandString)
                {
                    game = game.TryMakeStep(command);
                }
            }

            return new PlayedGameInfo(stringBuilder.ToString(), game.Score);
        }
    }
}