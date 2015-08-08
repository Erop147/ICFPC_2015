namespace ICFPC2015.GameLogic.Logic
{
    public struct UnitPosition
    {
        public Point PivotLocation { get; private set; }
        public int RotationCount { get; private set; }

        public UnitPosition(Point pivotLocation, int rotationCount) : this()
        {
            PivotLocation = pivotLocation;
            RotationCount = rotationCount;
        }

        public UnitPosition Clone()
        {
            return new UnitPosition(PivotLocation, RotationCount);
        }

        public UnitPosition Move(Point point)
        {
            return new UnitPosition(PivotLocation + point, RotationCount);
        }

        public UnitPosition Rotate(int count)
        {
            return new UnitPosition(PivotLocation, RotationCount + count);
        }
    }
}