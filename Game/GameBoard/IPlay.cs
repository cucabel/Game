namespace ToyRobot
{
    public interface IPlay : IPlayRobot, IPlayWall
    {
        public Coordinate moveOneSpaceForward(Coordinate coordinate, ICardinal cardinal);
        public List<Coordinate> Items { get; }
        public Robot Robot { get; }
    }
}
