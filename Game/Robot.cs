namespace ToyRobot
{
    public sealed class Robot
    {
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
            if (instance == null)
            {
                instance = new Robot(coordinate, cardinal);
            }

            instance.coordinate = coordinate;
            instance.cardinal = cardinal;
            return instance;
        }
        public void turnLeft()
        {
            cardinal = cardinal.turnLeft();
        }
        public void turnRight() 
        {
            cardinal = cardinal.turnRight(); 
        }
        public static Robot Instance { get => instance; set => instance = value; }
        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
        public ICardinal Cardinal { get => cardinal; set => cardinal = value; }
    }
}
