namespace ToyRobot
{
    public class Board : IPlay
    {
        private const int MAX_HEIGHT = 5;
        private const int MAX_WIDTH = 5;
        private const int MIN_HEIGHT = 1;
        private const int MIN_WIDTH = 1;
        private const int UNIT_SPACE = 1;
        private List<Coordinate> items = new List<Coordinate>();
        public Board() {}
        public void placeRobot(Coordinate coordinate, ICardinal cardinal)
        {
            if (Robot.Instance != null)
            {
                deleteExistingRobotCoordinate();
            }
            Robot.getInstance(coordinate, cardinal);

            addNewRobotCoordinate(coordinate);
        }
        public void placeWall(Coordinate coordinate)
        {
            Items.Add(coordinate);
        }
        public void getRobotLocation()
        {
            Console.WriteLine(Robot.Instance.Coordinate.X + "," + Robot.Instance.Coordinate.Y + "," + Robot.Instance.Cardinal.getDirection());
        }
        public Coordinate moveOneSpaceForward(Coordinate coordinate, ICardinal cardinal)
        {
            int x = coordinate.X;
            int y = coordinate.Y;

            if (cardinal.getDirection() == Direction.NORTH) y = (y < MAX_HEIGHT) ? y + UNIT_SPACE : MIN_HEIGHT;
            if (cardinal.getDirection() == Direction.EAST)  x = (x < MAX_WIDTH) ? x + UNIT_SPACE : MIN_WIDTH;
            if (cardinal.getDirection() == Direction.SOUTH) y = (y > MIN_HEIGHT) ? y - UNIT_SPACE : MAX_HEIGHT;
            if (cardinal.getDirection() == Direction.WEST)  x = (x > MIN_WIDTH) ? x - UNIT_SPACE : MAX_WIDTH;

            Coordinate newCoordinate = new Coordinate(x, y);
            return newCoordinate;
        }
        public void moveRobot(Coordinate newCoordinate)
        {
            deleteExistingRobotCoordinate();

            Robot.Instance.Coordinate = newCoordinate;

            addNewRobotCoordinate(newCoordinate);
        }
        public void turnRobotLeft() 
        { 
            Robot.Instance.turnLeft();
        }
        public void turnRobotRight()
        {
            Robot.Instance.turnRight();
        }
        public void deleteExistingRobotCoordinate()
        {
            Coordinate existingCoordinate = Robot.Instance.Coordinate;
            items.RemoveAll(c => c == existingCoordinate);
        }
        public void addNewRobotCoordinate(Coordinate newCoordinate)
        {
            items.Add(newCoordinate);
        }
        public static int MAX_HEIGHT1 => MAX_HEIGHT;
        public static int MAX_WIDTH1 => MAX_WIDTH;
        public static int MIN_HEIGHT1 => MIN_HEIGHT;
        public static int MIN_WIDTH1 => MIN_WIDTH;
        public static int UNIT_SPACE1 => UNIT_SPACE;
        public List<Coordinate> Items { get => items; set => items = value; }
    }
}
