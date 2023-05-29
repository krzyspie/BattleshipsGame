using Battleships.Builders;
using Battleships.Enums;
using Battleships.Helpers;
using Battleships.Models;
using Battleships.Validators;
using Battleships.Views;
using Moq;

namespace Battleships.Tests.Controllers
{
    public class GameControllerTests
    {
        private readonly Mock<IView> mockGameView = new Mock<IView>();
        private readonly Mock<IShipsBuilder> mockShipsBuilder = new Mock<IShipsBuilder>();
        private readonly Mock<ICoordinatesValidator> mockValidator = new Mock<ICoordinatesValidator>();
        private readonly Mock<ICoordinateExtractor> mockCoordinateExtractor = new Mock<ICoordinateExtractor>();
        //private readonly Mock<IShipCoordinatesBuilder> mockShipCoordinatesBuilder = new Mock<IShipCoordinatesBuilder>();
        private readonly List<Ship> ships = new List<Ship>
        {
            new Ship(ShipType.Battleship) { Coordinates = new List<ShipCoordinate> { new ShipCoordinate(1, 2), new ShipCoordinate(5, 2) } }
        };

        [Fact]
        public void RunGame_ShouldExecuteSuccessfully()
        {
            // Arrange
            mockShipsBuilder.Setup(x => x.GenerateShips()).Returns(ships);
            mockGameView.SetupSequence(x => x.ReadUserInput()).Returns("B44").Returns("B4").Returns("B4").Returns("A2").Returns("E2");
            mockValidator.Setup(x => x.Validate("B44")).Returns(false);
            mockValidator.Setup(x => x.Validate("B4")).Returns(true);
            mockValidator.Setup(x => x.Validate("A2")).Returns(true);
            mockValidator.Setup(x => x.Validate("E2")).Returns(true);

            mockCoordinateExtractor.Setup(x => x.GetCoordinates("B4")).Returns((2, 4));
            mockCoordinateExtractor.Setup(x => x.GetCoordinates("A2")).Returns((1, 2));
            mockCoordinateExtractor.Setup(x => x.GetCoordinates("E2")).Returns((5, 2));

            var game = new GameController(mockGameView.Object);
            game.CoordinateExtractor = mockCoordinateExtractor.Object;
            game.ShipsBuilder = mockShipsBuilder.Object;
            game.Validator = mockValidator.Object;

            // Act
            game.RunGame();

            // Assert
            mockValidator.Verify(x => x.Validate(It.IsAny<string>()), Times.Exactly(5));
            mockCoordinateExtractor.Verify(x => x.GetCoordinates(It.IsAny<string>()), Times.Exactly(4));
            mockGameView.Verify(x => x.DisplayShipMiss(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            mockGameView.Verify(x => x.DisplayShipHit(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            mockGameView.Verify(x => x.DisplayShipDestroyed(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            mockGameView.Verify(x => x.DisplayAlreadyShot(), Times.Once);
            mockGameView.Verify(x => x.DisplayEndGameMessage(), Times.Once);
        }
    }
}
