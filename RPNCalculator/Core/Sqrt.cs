using System;

namespace RPNCalculator.Core
{
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