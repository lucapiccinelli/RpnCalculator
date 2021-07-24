using System;

namespace RPNCalculator.Core
{
    public class Max : IRpn
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

        public double Evaluate() => 
            Math.Max(_operand1.Evaluate(), _operand2.Evaluate());
    }
}