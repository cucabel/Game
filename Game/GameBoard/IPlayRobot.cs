namespace ToyRobot
{
    public interface IPlayRobot
    {
        public void placeRobot(Coordinate newCoordinate, ICardinal cardinal);
        public string getRobotLocation();
        public void turnRobotLeft();
        public void turnRobotRight();
    }
}
