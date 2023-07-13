using CoordinateNS;
using FacingNS;

namespace IPlayNS
{
    public interface IPlay
    {
        public void placeRobot(Coordinate newCoordinate, Facing facing);
        public void getRobotLocation();
        public List<Coordinate> Items { get; set; }
    }
}
