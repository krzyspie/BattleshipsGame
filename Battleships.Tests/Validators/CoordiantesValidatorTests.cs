using Battleships.Validators;

namespace Battleships.Tests.Validators
{
    public class CoordinatesValidatorTests
    {
        private readonly ICoordinatesValidator validator;

        public CoordinatesValidatorTests()
        {
            validator = new CoordinatesValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_EmptyOrNullCoordinates_ReturnsFalseWithError(string input)
        {
            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.False(result);
            //Assert.Equal("Coordinates not provided.", result.Error);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("AB12")]
        public void Validate_InvalidCoordinates_ReturnsFalseWithError(string input)
        {
            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.False(result);
            //Assert.Equal("Wrong coordinates.", result.Error);
        }

        [Theory]
        [InlineData("1A")]
        [InlineData("123")]
        public void Validate_InvalidFirstCoordinate_ReturnsFalseWithError(string input)
        {
            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("K1")]
        [InlineData("z2")]
        public void Validate_FirstCoordinateNotBetweenAandJ_ReturnsFalseWithError(string input)
        {
            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("A0")]
        [InlineData("A11")]
        public void Validate_InvalidSecondCoordinate_ReturnsFalseWithError(string input)
        {
            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ValidCoordinates_ReturnsTrueWithoutError()
        {
            // Arrange
            string input = "A1";

            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.True(result);
        }
    }
}
