namespace RPNCalculator.Core
{
    public static class RpnElement
    {
        public static IRpnElement Of(string value) =>
            value switch
            {
                "+" => new RpnOperator(RpnElementEnum.Add),
                "-" => new RpnOperator(RpnElementEnum.Subtract),
                "*" => new RpnOperator(RpnElementEnum.Multiply),
                "/" => new RpnOperator(RpnElementEnum.Divide),
                "SQRT" => new RpnOperator(RpnElementEnum.Sqrt),
                "MAX" => new RpnOperator(RpnElementEnum.Max),
                _ => new RpnDigit(IntRpn.Of(value)),
            };
    }
}