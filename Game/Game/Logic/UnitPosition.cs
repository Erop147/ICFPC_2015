namespace ICFPC2015.GameLogic.Logic
{
    public struct UnitPosition
    {
        public Point PivotLocation { get; private set; }
        public int RotationCount { get; private set; }

        public UnitPosition(Point pivotLocation, int rotationCount)
            : this()
        {
            PivotLocation = pivotLocation;
            RotationCount = (rotationCount % 6 + 6) % 6;
        }

        public UnitPosition MakeStep(Command command)
        {
            if (command == Command.TurnClockWise)
            {
                return Rotate(1);
            }
            if (command == Command.TurnCounterClockWise)
            {
                return Rotate(-1);
            }

            return new UnitPosition(PivotLocation.MakeStep(command), RotationCount);
        }

        private UnitPosition Rotate(int count)
        {
            return new UnitPosition(PivotLocation, RotationCount + count);
        }
    }
}