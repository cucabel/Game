namespace ToyRobot
{
    public class East : ICardinal
    {
        public ICardinal turnLeft()
        {
            return new North();
        }
        public ICardinal turnRight()
        {
            return new South();
        }
        public Direction getDirection()
        {
            return Direction.EAST;
        }
    }
}
