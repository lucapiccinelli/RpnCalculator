namespace RPNCalculator.Core
{
    public class RpnDigit : IRpnElement
    {
        private readonly IntRpn _digit;

        public RpnDigit(IntRpn digit)
        {
            _digit = digit;
        }

        public IRpn ToExpression(StackExpressions stack) => _digit;
        public bool IsTerminal() => false;
    }
}