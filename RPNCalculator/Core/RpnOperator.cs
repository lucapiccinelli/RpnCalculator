using System;

namespace RPNCalculator.Core
{
    public class RpnOperator : IRpnElement
    {
        private readonly RpnElementEnum _type;

        public RpnOperator(RpnElementEnum type)
        {
            _type = type;
        }

        public IRpn ToExpression(StackExpressions stack) =>
            _type switch
            {
                RpnElementEnum.Add => new Sum(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Subtract => Subtraction.Invert(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Multiply => new Multiply(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Divide => Divide.Invert(stack.ToExpressions(), stack.ToExpressions()),
                RpnElementEnum.Sqrt => new Sqrt(stack.ToExpressions()),
                RpnElementEnum.Max => new Max(stack.ToExpressions(), stack),
                _ => throw new ArgumentOutOfRangeException()
            };

        public bool IsTerminal() => _type == RpnElementEnum.Max;
    }
}