using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace RPNCalculator.Tests
{
    public class RPNCalculatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("1 2 +", 3)]
        [InlineData("5 3 -", 2)]
        [InlineData("4 3 *", 12)]
        [InlineData("8 2 /", 4)]
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
            if (string.IsNullOrEmpty(expressionStr)) return 0;

            char op = expressionStr.LastOrDefault();

            var operands = expressionStr
                .Split(" ")
                .Take(2)
                .Select(int.Parse);

            return op switch
            {
                '+' => operands.Aggregate((acc, n) => acc + n),
                '-' => operands.Aggregate((acc, n) => acc - n),
                '*' => operands.Aggregate((acc, n) => acc * n),
                '/' => operands.Aggregate((acc, n) => acc / n),
                _ => operands.FirstOrDefault()
            };
        }
    }
}
