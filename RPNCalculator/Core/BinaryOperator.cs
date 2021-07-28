using System;

namespace RPNCalculator.Core
{
    public class BinaryOperator : IRpnElement
    {
        private readonly Func<IRpn, IRpn, IRpn> _operatorFactoryFunction;

        public BinaryOperator(Func<IRpn, IRpn, IRpn> operatorFactoryFunction)
        {
            _operatorFactoryFunction = operatorFactoryFunction;
        }

        public IRpn ToExpression(StackExpressions stack) =>
            _operatorFactoryFunction(stack.ToExpressions(), stack.ToExpressions());

        public bool IsTerminal() => false;
    }
}