namespace RPNCalculator.Core
{
    public static class RpnElement
    {
        public static IRpnElement Of(string value) =>
            value switch
            {
                "+" => new BinaryOperator((op1, op2) => new Sum(op1, op2)),
                "-" => new BinaryOperator(Subtraction.Invert),
                "*" => new BinaryOperator((op1, op2) => new Multiply(op1, op2)),
                "/" => new BinaryOperator(Divide.Invert),
                "SQRT" => new UnaryOperator(op => new Sqrt(op)),
                "MAX" => new RecursiveOperator((op, stack) => new Max(op, stack)),
                _ => new RpnDigit(IntRpn.Of(value)),
            };
    }
}