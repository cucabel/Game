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

        public void placeRobot(Coordinate newCoordinate, Facing facing)
        {
            Robot robot = Robot.Instance;

            if (robot != null)
            {
                Coordinate coordinate = robot.Coordinate;
                items.RemoveAll(c => c == coordinate);
            }
            robot = Robot.getInstance(newCoordinate, facing);
            items.Add(newCoordinate);
        }
        public List<Coordinate> Items { get => items; set => items = value; }
    }
}
