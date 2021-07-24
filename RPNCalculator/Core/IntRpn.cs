namespace RPNCalculator.Core
{
    public class IntRpn : IRpn
    {
        private readonly int _value;

        public IntRpn(int value)
        {
            _value = value;
        }

        public static IntRpn Of(string value) => new IntRpn(int.Parse(value));

        public double Evaluate() => _value;
    }
}