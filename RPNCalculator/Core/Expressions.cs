using System;
using System.Collections.Generic;

namespace RPNCalculator.Core
{
    public class StackExpressions
    {
        private readonly Stack<IRpnElement> _stack;

        public StackExpressions(Stack<IRpnElement> stack)
        {
            _stack = stack;
        }

        public IRpn ToExpressions() => _stack
            .Pop()
            .ToExpression(this);

        public bool EndOfStack() => 
            _stack.Count == 0 || _stack.Peek().IsTerminal();
    }

    public enum RpnElementEnum
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Sqrt,
        Max,
    }

    public static class RpnElement
    {
        public static IRpnElement Of(string value) =>
            value switch
            {
                "+" => new RpnOperator(RpnElementEnum.Add),
                "-" => new RpnOperator(RpnElementEnum.Subtract),
                "*" => new RpnOperator(RpnElementEnum.Multiply),
                "/" => new RpnOperator(RpnElementEnum.Divide),
                "SQRT" => new RpnOperator(RpnElementEnum.Sqrt),
                "MAX" => new RpnOperator(RpnElementEnum.Max),
                _ => new RpnDigit(IntRpn.Of(value)),
            };
    }

    public class RpnDigit : IRpnElement
    {
        private readonly IntRpn _digit;

        public RpnDigit(IntRpn digit)
        {
            _digit = digit;
        }

        public IRpn ToExpression(StackExpressions stack) => _digit;
        public bool IsTerminal() => false;
    }

    public class RpnOperator : IRpnElement
    {
        private readonly RpnElementEnum _type;

        public RpnOperator(RpnElementEnum type)
        {
            _type = type;
        }

        public IRpn ToExpression(StackExpressions stack)
        {
            return _type switch
            {
                RpnElementEnum.Add => new Sum(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Subtract => Subtraction.Invert(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Multiply => new Multiply(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Divide => Divide.Invert(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Sqrt => new Sqrt(stack.ToExpressions()),
                RpnElementEnum.Max => new Max(stack.ToExpressions(), stack),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public bool IsTerminal() => _type == RpnElementEnum.Max;
    }

    public interface IRpnElement
    {
        IRpn ToExpression(StackExpressions stack);
        bool IsTerminal();
    }

    class Max : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Max(IRpn operand1, StackExpressions stack)
        {
            _operand1 = operand1;
            _operand2 = stack.EndOfStack()
                ? (IRpn) new IntRpn(0) 
                : new Max(stack.ToExpressions(), stack);
        }

        public double Evaluate()
        {
            return Math.Max(_operand1.Evaluate(), _operand2.Evaluate());
        }
    }
}