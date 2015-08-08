using System.Linq;

namespace ICFPC2015.GameLogic.Logic.Input
{
    public class InputUnit
    {
        public InputCell[] members { get; set; }
        public InputCell pivot { get; set; }

        public Unit ToUnit()
        {
            return Unit.Create(pivot.ToPoint(), members.Select(x => x.ToPoint()).ToArray());
        }
    }
}