namespace ToyRobot
{
    public class North : ICardinal
    {
        public ICardinal turnLeft()
        {
            return new West();
        }
        public ICardinal turnRight()
        {
            return new East();
        }
        public Direction getDirection()
        {
            return Direction.NORTH;
        }
    }
}
