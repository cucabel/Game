namespace ToyRobot
{
    public class Coordinate
    {
        private readonly int x;
        private readonly int y;
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X => x;
        public int Y => y;

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate &&
                   x == coordinate.x &&
                   y == coordinate.y;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
        public override string ToString()
        {
            return x + "," + y;
        }
    }
}
