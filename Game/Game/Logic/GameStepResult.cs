namespace ICFPC2015.GameLogic.Logic
{
    public struct GameStepResult
    {
        public Game Game { get; private set; }
        public StepResult Result { get; private set; }

        public GameStepResult(Game game, StepResult result) 
            : this()
        {
            Game = game;
            Result = result;
        }
    }
}