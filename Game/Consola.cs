namespace ToyRobot
{
    public class Consola : IConsola
    {
        public Consola() {}

        public void print(string message)
        {
            Console.WriteLine(message);
        }
        public string readInput()
        {
            return Console.ReadLine();
        }
    }
}
