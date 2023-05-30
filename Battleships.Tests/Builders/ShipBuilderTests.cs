using Battleships.Builders;
using Battleships.Enums;
using Battleships.Models;
using Moq;

namespace Battleships.Tests.Builders
{
    public class ShipBuilderTests
    {
        private readonly List<ShipCoordinate> firstShipCoordinates = new List<ShipCoordinate>()
        {
            new ShipCoordinate(1,1),
            new ShipCoordinate(1,2),
            new ShipCoordinate(1,3),
            new ShipCoordinate(1,4),
            new ShipCoordinate(1,5),
        };

        private readonly List<ShipCoordinate> secondShipCoordinates = new List<ShipCoordinate>()
        {
            new ShipCoordinate(5,1),
            new ShipCoordinate(5,2),
            new ShipCoordinate(5,3),
            new ShipCoordinate(5,4),
            new ShipCoordinate(5,5),
        };

        private readonly List<ShipCoordinate> thirdShipCoordinates = new List<ShipCoordinate>()
        {
            new ShipCoordinate(4,5),
            new ShipCoordinate(5,5),
            new ShipCoordinate(6,5),
            new ShipCoordinate(7,5),
        };

        private readonly List<ShipCoordinate> fourthShipCoordinates = new List<ShipCoordinate>()
        {
            new ShipCoordinate(4,8),
            new ShipCoordinate(5,8),
            new ShipCoordinate(6,8),
            new ShipCoordinate(7,8),
        };
        private readonly Mock<IShipCoordinatesBuilder> mockShipCoordinateBuilder = new Mock<IShipCoordinatesBuilder>();

        [Fact]
        public void GenerateShips_NoColision_ReturnsValidListOfShips()
        {
            // Arrange
            mockShipCoordinateBuilder.SetupSequence(x => x.GenerateShipCoordinates(It.IsAny<int>()))
                .Returns(firstShipCoordinates)
                .Returns(secondShipCoordinates)
                .Returns(fourthShipCoordinates);

            IShipsBuilder shipsBuilder = new ShipsBuilder()
            {
                ShipCoordinateBuilder = mockShipCoordinateBuilder.Object
            };

            // Act
            List<Ship> ships = shipsBuilder.GenerateShips();

            // Assert
            VerifyResult(ships, 3);
        }

        [Fact]
        public void GenerateShips_Colision_ReturnsValidListOfShips()
        {
            // Arrange
            mockShipCoordinateBuilder.SetupSequence(x => x.GenerateShipCoordinates(It.IsAny<int>()))
                .Returns(firstShipCoordinates)
                .Returns(secondShipCoordinates)
                .Returns(thirdShipCoordinates)
                .Returns(fourthShipCoordinates);

            IShipsBuilder shipsBuilder = new ShipsBuilder()
            {
                ShipCoordinateBuilder = mockShipCoordinateBuilder.Object
            };

            // Act
            List<Ship> ships = shipsBuilder.GenerateShips();

            // Assert
            VerifyResult(ships, 4);
        }

        private void VerifyResult(List<Ship> ships, int times)
        {
            Assert.NotNull(ships);
            Assert.NotEmpty(ships);
            Assert.Equal(3, ships.Count);
            Assert.Equal(firstShipCoordinates, ships[0].Coordinates);
            Assert.Equal(ShipType.Battleship, ships[0].Type);
            Assert.Equal(secondShipCoordinates, ships[1].Coordinates);
            Assert.Equal(ShipType.Destroyer, ships[1].Type);
            Assert.Equal(fourthShipCoordinates, ships[2].Coordinates);
            Assert.Equal(ShipType.Destroyer, ships[2].Type);

            mockShipCoordinateBuilder.Verify(x => x.GenerateShipCoordinates(It.IsAny<int>()), Times.Exactly(times));
        }
    }
}
