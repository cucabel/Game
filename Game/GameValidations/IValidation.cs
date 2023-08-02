namespace ToyRobot
{
    public interface IValidation : ICoordinateValidation, IRobotValidation
    {
        public bool validateLocation(int row, int col, string facing);
    }
}
