using System;
using System.Collections.Generic;

namespace RPNCalculator.Core
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
                "SQRT" => new Sqrt(ToExpressions(stack)),
                _ => IntRpn.Of(element)
            };
        }
    }

    public class Sqrt : IRpn
    {
        private readonly IRpn _expr;

        public Sqrt(IRpn expr)
        {
            _expr = expr;
        }

        public double Evaluate() => Math.Sqrt(_expr.Evaluate());
    }
}