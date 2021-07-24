namespace RPNCalculator.Core
{
    public class Sum : IRpn
    {
        private readonly IRpn _operand1;
        private readonly IRpn _operand2;

        public Sum(IRpn operand1, IRpn operand2)
        {
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public double Evaluate() => _operand1.Evaluate() + _operand2.Evaluate();
    }
}