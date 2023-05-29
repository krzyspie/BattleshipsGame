using Battleships.Helpers;

namespace Battleships.Tests.Helpers
{
    public class CoordinateExtractorTests
    {
        private readonly CoordinateExtractor extractor;

        public CoordinateExtractorTests()
        {
            extractor = new CoordinateExtractor();
        }

        [Theory]
        [InlineData("A1", 1, 1)]
        [InlineData("B5", 2, 5)]
        [InlineData("J10", 10, 10)]
        public void GetCoordinates_ValidCoordinates_ReturnsExpectedValues(string input, int expectedColumn, int expectedRow)
        {
            // Act
            var (column, row) = extractor.GetCoordinates(input);

            // Assert
            Assert.Equal(expectedColumn, column);
            Assert.Equal(expectedRow, row);
        }
    }
}