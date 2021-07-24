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
        [InlineData("8 2 + 3 +", 13)]
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
                .Select(IntRpn.Of)
                .ToList();

            IRpn expression = op switch
            {
                '+' => new Sum(operands[0], operands[1]),
                '-' => new Subtraction(operands[0], operands[1]),
                '*' => new Multiply(operands[0], operands[1]),
                '/' => new Divide(operands[0], operands[1]),
                _ => operands.First()
            };

            return expression.Evaluate();
        }
    }

    public class Divide : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Divide(IRpn operand1, IRpn operand2)
        {
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public double Evaluate() => _operand1.Evaluate() / _operand2.Evaluate();
    }

    public class Multiply : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Multiply(IRpn operand1, IRpn operand2)
        {
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public double Evaluate() => _operand1.Evaluate() * _operand2.Evaluate();
    }

    public class Subtraction : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Subtraction(IRpn operand1, IRpn operand2)
        {
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public double Evaluate() => _operand1.Evaluate() - _operand2.Evaluate();
    }

    public class Sum : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Sum(IRpn operand1, IRpn operand2)
        {
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public double Evaluate() => _operand1.Evaluate() + _operand2.Evaluate();
    }

    public interface IRpn
    {
        double Evaluate();
    }

    public class IntRpn : IRpn
    {
        private readonly int _value;

        private IntRpn(int value)
        {
            _value = value;
        }

        public static IntRpn Of(string value) => new IntRpn(int.Parse(value));

        public double Evaluate() => _value;
    }
}
