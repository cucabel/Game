namespace ToyRobot
{
    public interface ICardinal
    {
        public ICardinal turnLeft();
        public ICardinal turnRight();
        public Direction getDirection();
    }
}
