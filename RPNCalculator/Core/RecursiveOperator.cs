using System;

namespace RPNCalculator.Core
{
    public class RecursiveOperator : IRpnElement
    {
        private readonly Func<IRpn, StackExpressions, IRpn> _operatorFactoryFunction;

        public RecursiveOperator(Func<IRpn, StackExpressions, IRpn> operatorFactoryFunction)
        {
            _operatorFactoryFunction = operatorFactoryFunction;
        }

        public IRpn ToExpression(StackExpressions stack) =>
            _operatorFactoryFunction(stack.ToExpressions(), stack);

        public bool IsTerminal() => true;
    }
}