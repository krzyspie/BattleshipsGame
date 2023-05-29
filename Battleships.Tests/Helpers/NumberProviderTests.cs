using Battleships.Helpers;

namespace Battleships.Tests.Helpers
{
    public class NumberProviderTests
    {
        [Fact]
        public void GetNumber_ReturnsNumberInRange()
        {
            // Arrange
            INumberProvider numberProvider = new NumberProvider();
            int minValue = 1;
            int maxValue = 10;

            // Act
            int result = numberProvider.GetNumber(minValue, maxValue);

            // Assert
            Assert.InRange(result, minValue, maxValue);
        }
    }
}
