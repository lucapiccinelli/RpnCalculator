using System;
using System.Collections;
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


            var stack = new Stack<string>(expressionStr
                .Split(" ")
                .ToList());

            IRpn expression = ToExpressions(stack);
            return expression.Evaluate();
        }

        private static IRpn ToExpressions(Stack<string> stack)
        {
            string element = stack.Pop();
            return element switch
            {
                "+" => new Sum(ToExpressions(stack), ToExpressions(stack)),
                "-" => Subtraction.Invert(ToExpressions(stack), ToExpressions(stack)),
                "*" => new Multiply(ToExpressions(stack), ToExpressions(stack)),
                "/" => Divide.Invert(ToExpressions(stack), ToExpressions(stack)),
                _ => IntRpn.Of(element)
            };
        }

        private static IRpn GetBynaryOperator(string op, List<IntRpn> operands)
        {
            IRpn expression = op switch
            {
                "+" => new Sum(operands[0], operands[1]),
                "-" => new Subtraction(operands[0], operands[1]),
                "*" => new Multiply(operands[0], operands[1]),
                "/" => new Divide(operands[0], operands[1]),
                _ => operands[0]
            };
            return expression;
        }

        private static IOperand Operand(string value)
        {
            char c = value.First();
            if (char.IsDigit(c)) return IntRpn.Of(value);
            return new OperatorFactory(c);
        }
    }

    internal class OperatorFactory : IOperand
    {
        private readonly char _c;

        public OperatorFactory(in char c)
        {
            _c = c;
        }
    }

    interface IOperand
    {
        
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

        public static IRpn Invert(IRpn operand1, IRpn operand2) => new Divide(operand2, operand1);
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

        public static IRpn Invert(IRpn operand1, IRpn operand2) => new Subtraction(operand2, operand1);
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

    public class IntRpn : IRpn, IOperand
    {
        private readonly int _value;

        public IntRpn(int value)
        {
            _value = value;
        }

        public static IntRpn Of(string value) => new IntRpn(int.Parse(value));

        public double Evaluate() => _value;
    }
}
