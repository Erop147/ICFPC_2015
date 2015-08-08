using System.Linq;
using System.Text;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public class GreedyPowerWordCommandStringGenerator : ICommandStringGenerator
    {
        public string Generate(Board board, GameUnit unit, UnitPosition finishPosition)
        {
            var stringBuilder = new StringBuilder();
            var powerWords = MagicWordsStore.Words;
            while (!unit.UnitPosition.Equals(finishPosition))
            {
                foreach (var powerWord in powerWords.OrderByDescending(x => x.Length))
                {
                    var currentUnit = unit;
                    foreach (var command in powerWord)
                    {
                        currentUnit = currentUnit.MakeStep(CommandConverter.Convert(command));
                        if (!board.IsValid(currentUnit))
                        {
                            break;
                        }
                    }
                    if (board.IsValid(currentUnit) && ReachableStatesGetter.Get(board, currentUnit).Contains(finishPosition))
                    {
                        unit = currentUnit;
                        stringBuilder.Append(powerWord);
                        break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}