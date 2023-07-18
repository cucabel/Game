namespace ToyRobot
{
    public interface IPlayRobot
    {
        public void placeRobot(Coordinate newCoordinate, ICardinal cardinal);
        public void getRobotLocation();
        public void moveRobot(Coordinate coordinate);
        public void turnRobotLeft();
        public void turnRobotRight();
    }
}
