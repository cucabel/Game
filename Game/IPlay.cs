using CoordinateNS;
using FacingNS;

namespace IPlayNS
{
    public interface IPlay
    {
        public void placeRobot(Coordinate newCoordinate, Facing facing);
        public void getRobotLocation();
        public Coordinate moveOneSpaceForward(Coordinate coordinate, Facing direction);
        public void moveRobot(Coordinate coordinate);
        public List<Coordinate> Items { get; set; }
    }
}
