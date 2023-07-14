using CoordinateNS;
using FacingNS;
using IPlayNS;
using RobotNS;

namespace BoardNS
{
    public class Board : IPlay
    {
        private const int MAX_HEIGHT = 5;
        private const int MAX_WIDTH = 5;
        private const int MIN_HEIGHT = 1;
        private const int MIN_WIDTH = 1;

        private List<Coordinate> items = new List<Coordinate>();

        public Board() {}

        public static int MAX_HEIGHT1 => MAX_HEIGHT;
        public static int MAX_WIDTH1 => MAX_WIDTH;
        public static int MIN_HEIGHT1 => MIN_HEIGHT;
        public static int MIN_WIDTH1 => MIN_WIDTH;

        public void placeRobot(Coordinate coordinate, Facing facing)
        {
            Robot robot = Robot.Instance;

            if (robot != null)
            {
                Coordinate existingCoordinate = robot.Coordinate;
                items.RemoveAll(c => c == existingCoordinate);
            }
            robot = Robot.getInstance(coordinate, facing);
            items.Add(coordinate);
        }
        public void getRobotLocation()
        {
            Console.WriteLine(Robot.Instance.Coordinate.X + ", " + Robot.Instance.Coordinate.Y + ", " + Robot.Instance.Facing);
        }
        public Coordinate moveOneSpaceForward(Coordinate coordinate, Facing direction)
        {
            int x = coordinate.X;
            int y = coordinate.Y;

            if (direction == Facing.NORTH) y = (y + 1) % MAX_HEIGHT;
            if (direction == Facing.EAST)  x = (x + 1) % MAX_WIDTH;
            if (direction == Facing.SOUTH) y = (y > 1) ? y - 1 : MAX_HEIGHT;
            if (direction == Facing.WEST)  x = (x > 1) ? x - 1 : MAX_WIDTH;

            Coordinate newCoordinate = new Coordinate(x, y);
            return newCoordinate;
        }
        public void moveRobot(Coordinate newCoordinate)
        {
            Robot.Instance.Coordinate = newCoordinate;
        }
        public List<Coordinate> Items { get => items; set => items = value; }
    }
}
