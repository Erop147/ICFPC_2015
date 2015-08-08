using System.Text;
using ICFPC2015.GameLogic.Logic;
using ICFPC2015.Player.Implementation;
using System.Linq;

namespace ICFPC2015.Player.Players
{
    public class DummyBottomLeftPlayer : IPlayer
    {
        private readonly ICommandStringGenerator commandStringGenerator;

        public DummyBottomLeftPlayer(ICommandStringGenerator commandStringGenerator)
        {
            this.commandStringGenerator = commandStringGenerator;
        }

        public PlayedGameInfo Play(Game game)
        {
            var stringBuilder = new StringBuilder();

            while (game.State != GameState.GameOver && game.State != GameState.Error)
            {
                var unitPositions = ReachableStatesGetter.Get(game.Board, game.Current);

                var finishPosition = unitPositions.Select(x => new {BottomLeft = new GameUnit(game.Current.Unit, x).BottomLeft(), Position = x})
                                                  .OrderByDescending(x => x.BottomLeft.Row)
                                                  .ThenBy(x => x.BottomLeft.Col)
                                                  .First()
                                                  .Position;

                var commandString = commandStringGenerator.Generate(game, finishPosition);
                stringBuilder.Append(commandString);

                foreach (var command in commandString)
                {
                    game = game.TryMakeStep(CommandConverter.Convert(command));
                }
            }

            return new PlayedGameInfo(stringBuilder.ToString(), game.Score);
        }
    }
}