namespace ToyRobot
{
    public interface ICoordinateValidation
    {
        public bool validateCoordinate(int row, int col);
        public bool isOccupiedCoordinate(Coordinate coordinate, IPlay board);
    }
}