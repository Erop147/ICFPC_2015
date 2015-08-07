namespace ICFPC2015.Game.Logic
{
    public class GameStepResult
    {
        public Game Game { get; private set; }
        public StepResult Result { get; private set; }

        public GameStepResult(Game game, StepResult result)
        {
            Game = game;
            Result = result;
        }
    }
}