using FluentAssertions;
using KBS.Cities.Shared.Extensions;
using NUnit.Framework;

namespace KBS.Cities.Shared.UnitTests.Extensions
{
    public class ToQueryStringTests
    {
        [Test]
        public void ShouldReturnQueryString_WithOneParameter()
        {
            // Arrange
            const string expectedQueryString = "?Str=str";
            var obj = new { Str = "str" };

            // Act
            var result = obj.ToQueryString();

            // Assert
            result.Should().Be(expectedQueryString);
        }

        [Test]
        public void ShouldReturnQueryString_WithTwoParameters()
        {
            // Arrange
            const string expectedQueryString = "?Str1=str1&Str2=str2";
            var obj = new { Str1 = "str1", Str2 = "str2" };

            // Act
            var result = obj.ToQueryString();

            // Assert
            result.Should().Be(expectedQueryString);
        }
    }
}
