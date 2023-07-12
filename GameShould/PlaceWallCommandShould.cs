using CoordinateNS;
using IPlayNS;
using PlaceWallCommandNS;
using Telerik.JustMock;

namespace PlaceWallCommandShouldNS
{
    public class PlaceWallCommandShould
    {
        private IPlay mockedBoard;
        private Coordinate coordinate;
        private PlaceWallCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            coordinate = new Coordinate(2, 3);
            command = new PlaceWallCommand(mockedBoard, coordinate);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            Mock.Arrange(() => mockedBoard.Items).Returns(coordinates);

            command.execute();

            Mock.Assert(() => mockedBoard.Items, Occurs.Once());

        }
    }
}
