using Xunit;
using MyLibrary;

namespace MyLibrary.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_WithTwoIntegers_ReturnsTheirSum()
        {
            var calc = new Calculator();
            Assert.Equal(5, calc.Add(2, 3));
        }

        [Fact]
        public void Add_WithZero_ReturnsFistNum()
        {
            var calc = new Calculator();
            Assert.Equal(42, calc.Add(42, 0));
        }
    }
}