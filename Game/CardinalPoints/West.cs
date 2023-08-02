namespace ToyRobot
{
    public class West : ICardinal
    {
        public ICardinal turnLeft()
        {
            return new South();
        }
        public ICardinal turnRight()
        {
            return new North();
        }
        public Direction getDirection()
        {
            return Direction.WEST;
        }
    }
}
