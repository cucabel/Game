using CoordinateNS;
using FacingNS;

namespace IPlayNS
{
    public interface IPlay
    {
        public void placeRobot(Coordinate newCoordinate, Facing facing);
        public List<Coordinate> Items { get; set; }
    }
}
