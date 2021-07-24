using System.Collections.Generic;
using System.Linq;

namespace RPNCalculator.Core
{
    public static class Rpn
    {
        public static double Evaluate(string expressionStr)
        {
            if (string.IsNullOrEmpty(expressionStr)) return 0;
            var stack = new Stack<IRpnElement>(expressionStr
                .Split(" ")
                .Select(RpnElement.Of)
                .ToList());

            IRpn expression = new StackExpressions(stack).ToExpressions();
            return expression.Evaluate();
        }
    }
}