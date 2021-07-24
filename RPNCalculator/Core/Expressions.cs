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
                "MAX" => new Max(ToExpressions(stack), stack),
                _ => IntRpn.Of(element)
            };
        }
    }

    class Max : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Max(IRpn operand1, Stack<string> stack)
        {
            _operand1 = operand1;
            _operand2 = stack.Count == 0 || stack.Peek() == "MAX"
                ? (IRpn) new IntRpn(0) 
                : new Max(Expressions.ToExpressions(stack), stack);
        }

        public double Evaluate()
        {
            return Math.Max(_operand1.Evaluate(), _operand2.Evaluate());
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