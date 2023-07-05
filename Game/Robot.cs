using CoordinateNS;
using FacingNS;

namespace RobotNS
{
    public sealed class Robot
    {
        private static Robot instance;
        private Coordinate coordinate;
        private Facing facing;

        private Robot(Coordinate coordinate, Facing facing)
        {
            this.coordinate = coordinate;
            this.facing = facing;
        }

        public static Robot getInstance(Coordinate coordinate, Facing facing)
        {
            if (instance == null)
            {
                instance = new Robot(coordinate, facing);
            }

            instance.coordinate = coordinate;
            instance.facing = facing;
            return instance;
        }
        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
        public Facing Facing { get => facing; set => facing = value; }
    }
}
