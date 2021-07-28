using System;

namespace RPNCalculator.Core
{
    public class UnaryOperator : IRpnElement
    {
        private readonly Func<IRpn, IRpn> _operatorFactoryFunction;

        public UnaryOperator(Func<IRpn, IRpn> operatorFactoryFunction)
        {
            _operatorFactoryFunction = operatorFactoryFunction;
        }

        public IRpn ToExpression(StackExpressions stack) =>
            _operatorFactoryFunction(stack.ToExpressions());

        public bool IsTerminal() => false;
    }
}