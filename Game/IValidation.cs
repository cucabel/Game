namespace ToyRobot
{
    public interface IValidation : ICoordinateValidation, IRobotValidation
    {
        public Boolean validateLocation(int row, int col, string facing);
    }
}
