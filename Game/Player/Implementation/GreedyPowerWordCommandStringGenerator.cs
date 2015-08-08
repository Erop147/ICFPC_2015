using System.Linq;
using System.Text;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public class GreedyPowerWordCommandStringGenerator : ICommandStringGenerator
    {
        public string Generate(Game game, UnitPosition finishPosition)
        {
            var stringBuilder = new StringBuilder();
            var powerWords = new string[] {};
            while (game.State != GameState.GameOver && game.State != GameState.Error)
            {
                foreach (var powerWord in powerWords.OrderByDescending(x => x.Length))
                {
                    var currentGame = game;
                    var fail = false;
                    foreach (var command in powerWord)
                    {
                        if (currentGame.State == GameState.GameOver)
                        {
                            fail = true;
                        }
                        currentGame = currentGame.TryMakeStep(CommandConverter.Convert(command));
                        if (currentGame.State == GameState.Error)
                        {
                            fail = true;
                        }
                    }
                    if (!fail)
                    {
                        stringBuilder.Append(powerWord);
                        game = currentGame;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}