namespace ToyRobot
{
    public interface ICoordinateValidation
    {
        public Boolean validateCoordinate(int row, int col);
        public Boolean isOccupiedCoordinate(Coordinate coordinate, IPlay board);
    }
}