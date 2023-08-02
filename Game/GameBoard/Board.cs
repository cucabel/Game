namespace ToyRobot
{
    public class Board : IPlay
    {
        private List<Coordinate> items = new List<Coordinate>();
        private Robot robot;
        public Board() { }
        public void placeRobot(Coordinate coordinate, ICardinal cardinal)
        {
            if (robot != null)
            {
                deleteExistingRobotCoordinate();
            }
            robot = Robot.getInstance(coordinate, cardinal);

            addNewRobotCoordinate(coordinate);
        }
        public void placeWall(Coordinate coordinate)
        {
            items.Add(coordinate);
        }
        public string getRobotLocation()
        {
            return robot.ToString();
        }
        public Coordinate moveOneSpaceForward(Coordinate coordinate, ICardinal cardinal)
        {
            int x = coordinate.X;
            int y = coordinate.Y;

            if (cardinal.getDirection().Equals(Direction.NORTH)) y = y < (int)Parameters.MaxHeight ? y + (int)Parameters.UnitSpace : (int)Parameters.MinHeight;
            if (cardinal.getDirection().Equals(Direction.EAST)) x = x < (int)Parameters.MaxWidth ? x + (int)Parameters.UnitSpace : (int)Parameters.MinWidth;
            if (cardinal.getDirection().Equals(Direction.SOUTH)) y = y > (int)Parameters.MinHeight ? y - (int)Parameters.UnitSpace : (int)Parameters.MaxHeight;
            if (cardinal.getDirection().Equals(Direction.WEST)) x = x > (int)Parameters.MinWidth ? x - (int)Parameters.UnitSpace : (int)Parameters.MaxWidth;

            Coordinate newCoordinate = new Coordinate(x, y);
            return newCoordinate;
        }
        public void turnRobotLeft()
        {
            robot.turnLeft();
        }
        public void turnRobotRight()
        {
            robot.turnRight();
        }
        private void deleteExistingRobotCoordinate()
        {
            Coordinate existingCoordinate = robot.Coordinate;
            items.RemoveAll(c => c == existingCoordinate);
        }
        private void addNewRobotCoordinate(Coordinate newCoordinate)
        {
            items.Add(newCoordinate);
        }
        public List<Coordinate> Items { get => items; }
        public Robot Robot { get => robot; }
    }
}
