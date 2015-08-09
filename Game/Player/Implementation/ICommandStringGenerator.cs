using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public interface ICommandStringGenerator
    {
        string Generate(Board _board, GameUnit _unit, GameUnit _finishUnit);
    }
}