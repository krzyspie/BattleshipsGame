using Battleships.Builders;
using Battleships.Helpers;
using Battleships.Models;
using Moq;

namespace Battleships.Tests.Builders
{
    public class ShipCoordinatesBuilderTests
    {
        private readonly int length = 4;
        private readonly int startCoordinateRow = 4;
        private readonly int startCoordinateColumn = 5;
        private readonly Mock<INumberProvider> mockNumberProvider = new Mock<INumberProvider>();
        private readonly IShipCoordinatesBuilder shipCoordinatesBuilder;

        public ShipCoordinatesBuilderTests()
        {
            shipCoordinatesBuilder = new ShipCoordinatesBuilder
            {
                NumberProvider = mockNumberProvider.Object
            };
        }

        [Fact]
        public void GenerateShipCoordinates_HorizontalDirection_ReturnsValidListOfCoordinates()
        {
            // Arrange
            mockNumberProvider.Setup(x => x.GetNumber(0, 1)).Returns(0);
            mockNumberProvider.Setup(x => x.GetNumber(1, 10)).Returns(startCoordinateRow);
            mockNumberProvider.Setup(x => x.GetNumber(1, 10 - length)).Returns(startCoordinateColumn);

            // Act
            List<ShipCoordinate> coordinates = shipCoordinatesBuilder.GenerateShipCoordinates(length);

            // Assert
            Assert.NotNull(coordinates);
            Assert.Equal(length, coordinates.Count);

            for (int i = 0; i < length; i++)
            {
                Assert.Equal(startCoordinateColumn + i, coordinates[i].Column);
                Assert.Equal(startCoordinateRow, coordinates[i].Row);
            }
        }

        [Fact]
        public void GenerateShipCoordinates_VerticalDirection_ReturnsValidListOfCoordinates()
        {
            // Arrange
            mockNumberProvider.Setup(x => x.GetNumber(0, 1)).Returns(1);
            mockNumberProvider.Setup(x => x.GetNumber(1, 10)).Returns(startCoordinateColumn);
            mockNumberProvider.Setup(x => x.GetNumber(1, 10 - length)).Returns(startCoordinateRow);

            // Act
            List<ShipCoordinate> coordinates = shipCoordinatesBuilder.GenerateShipCoordinates(length);

            // Assert
            Assert.NotNull(coordinates);
            Assert.Equal(length, coordinates.Count);

            for (int i = 0; i < length; i++)
            {
                Assert.Equal(startCoordinateColumn, coordinates[i].Column);
                Assert.Equal(startCoordinateRow + i, coordinates[i].Row);
            }
        }
    }
}
