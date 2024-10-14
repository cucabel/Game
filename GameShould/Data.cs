
using ToyRobot;

namespace ToyRobotTests
{
    public static class Data
    {
        public static int validX() { return (int)Parameters.MinWidth; }
        public static int validY() { return (int)Parameters.MinHeight; }
        public static int invalidY() { return (int)Parameters.MaxHeight + (int)Parameters.UnitSpace; }
        public static string validDirection() { return "NORTH"; }
        public static string invalidDirection() { return "CENTER"; }
        public static string validPlaceRobotStringCommand()
        {
            return StringCommand.PlaceRobot + " " + validX().ToString() + "," + validY().ToString() + "," + validDirection();
        }
        public static string validPlaceWallStringCommand()
        {
            return StringCommand.PlaceWall + " " + validX().ToString() + "," + validY().ToString();
        }
        public static Coordinate bottomLeftCoordinate() { return new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight); }
        public static Coordinate bottomRightCoordinate() { return new Coordinate((int)Parameters.MaxWidth, (int)Parameters.MinHeight); }
        public static Coordinate topLeftCoordinate() { return new Coordinate((int)Parameters.MinWidth, (int)Parameters.MaxHeight); }
        public static Coordinate topRightCoordinate() { return new Coordinate((int)Parameters.MaxWidth, (int)Parameters.MaxHeight); }
        public static ICardinal northCardinal() { return new North(); }
        public static ICardinal eastCardinal() { return new East(); }
        public static ICardinal southCardinal() { return new South(); }
        public static ICardinal westCardinal() { return new West(); }
    }
}
