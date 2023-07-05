using CoordinateNS;
using RobotNS;

namespace BoardNS
{

    public class Board
    {
        private const int MAX_HEIGHT = 5;
        private const int MAX_WIDTH = 5;
        private const int MIN_HEIGHT = 1;
        private const int MIN_WIDTH = 1;

        private Robot robot;
        private List<Coordinate> items = new List<Coordinate>();

        public Board() {}

        public static int MAX_HEIGHT1 => MAX_HEIGHT;

        public static int MAX_WIDTH1 => MAX_WIDTH;

        public static int MIN_HEIGHT1 => MIN_HEIGHT;

        public static int MIN_WIDTH1 => MIN_WIDTH;

        public Robot Robot { get => robot; set => robot = value; }
        public List<Coordinate> Items { get => items; set => items = value; }
    }
}
