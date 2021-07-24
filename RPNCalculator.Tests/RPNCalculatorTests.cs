using System.Collections.Generic;
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
            double result = Rpn.Evaluate(expressionStr);
            Assert.Equal(expectedValue, result);
        }
    }

    public static class Rpn
    {
        public static double Evaluate(string expressionStr)
        {
            return expressionStr
                .Split(" ")
                .Take(2)
                .Select(int.Parse)
                .Sum();
        }
    }
}
