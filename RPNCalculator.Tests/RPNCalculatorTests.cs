using System;
using System.Collections;
using System.Linq.Expressions;
using RPNCalculator.Core;
using Xunit;

namespace RPNCalculator.Tests
{
    public class RpnCalculatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("1 2 +", 3)]
        [InlineData("5 3 -", 2)]
        [InlineData("4 3 *", 12)]
        [InlineData("8 2 /", 4)]
        [InlineData("8 2 + 3 +", 13)]
        [InlineData("4 2 + 3 -", 3)]
        [InlineData("5 8 * 7 +", 47)]
        [InlineData("5 8 * 7 + 3 *", 141)]
        [InlineData("3 5 8 * 7 + *", 141)]
        [InlineData("9 SQRT", 3)]
        [InlineData("9 4 5 + +", 18)]
        [InlineData("9 SQRT 4 5 + +", 12)]
        [InlineData("4 5 9 SQRT + +", 12)]
        [InlineData("4 9 SQRT +", 7)]
        public void Test1(string expressionStr, double expectedValue)
        {
            double result = Rpn.Evaluate(expressionStr);
            Assert.Equal(expectedValue, result);
        }
    }
}
