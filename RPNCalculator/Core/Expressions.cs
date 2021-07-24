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
}