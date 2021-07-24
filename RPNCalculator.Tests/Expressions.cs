using System.Collections.Generic;

namespace RPNCalculator.Tests
{
    internal static class Expressions
    {
        public static IRpn ToExpressions(Stack<string> stack)
        {
            string element = stack.Pop();
            return element switch
            {
                "+" => new Sum(ToExpressions(stack), ToExpressions(stack)),
                "-" => Subtraction.Invert(ToExpressions(stack), ToExpressions(stack)),
                "*" => new Multiply(ToExpressions(stack), ToExpressions(stack)),
                "/" => Divide.Invert(ToExpressions(stack), ToExpressions(stack)),
                _ => IntRpn.Of(element)
            };
        }
    }
}