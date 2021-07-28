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