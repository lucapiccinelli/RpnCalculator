using System.Collections.Generic;
using System.Linq;

namespace RPNCalculator.Core
{
    public static class Rpn
    {
        public static double Evaluate(string expressionStr)
        {
            if (string.IsNullOrEmpty(expressionStr)) return 0;
            var stack = new Stack<string>(expressionStr
                .Split(" ")
                .ToList());

            IRpn expression = Expressions.ToExpressions(stack);
            return expression.Evaluate();
        }
    }
}