namespace ToyRobot
{
    public class South : ICardinal
    {
        public ICardinal turnLeft()
        {
            return new East();
        }
        public ICardinal turnRight()
        {
            return new West();
        }
        public Direction getDirection()
        {
            return Direction.SOUTH;
        }
    }
}
