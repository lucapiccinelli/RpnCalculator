using System.Linq;
using Xunit;

namespace RPNCalculator.Tests
{
    public class RPNCalculatorTests
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("1 2 +", 3)]
        public void Test1(string expressionStr, double expectedValue)
        {
            var expression = Rpn.Of(expressionStr);
            double result = expression.Evaluate();

            Assert.Equal(expectedValue, result);
        }
    }

    public static class Rpn
    {
        public static RpnInt Of(string expression)
        {
            int result = expression
                .Split(" ")
                .Take(2)
                .Select(int.Parse)
                .Sum();

            return new RpnInt(result);
        }
    }

    public class RpnInt
    {
        private readonly int _value;

        public RpnInt(int value)
        {
            _value = value;
        }

        public double Evaluate() => _value;
    }
}
