namespace ToyRobot
{
    public sealed class Robot
    {
        private static readonly object padlock = new object();
        private static Robot instance;
        private Coordinate coordinate;
        private ICardinal cardinal;

        private Robot(Coordinate coordinate, ICardinal cardinal)
        {
            this.coordinate = coordinate;
            this.cardinal = cardinal;
        }

        public static Robot getInstance(Coordinate coordinate, ICardinal cardinal)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Robot(coordinate, cardinal);
                }
                instance.coordinate = coordinate;
                instance.cardinal = cardinal;
                return instance;
            }
        }
        public void turnLeft()
        {
            cardinal = cardinal.turnLeft();
        }
        public void turnRight() 
        {
            cardinal = cardinal.turnRight(); 
        }
        public override string ToString()
        {
            return coordinate.ToString() + "," + cardinal.getDirection();
        }
        public static Robot Instance { get => instance; }
        public Coordinate Coordinate { get => coordinate; }
        public ICardinal Cardinal { get => cardinal; }
    }
}
