namespace RPNCalculator.Core
{
    public interface IRpnElement
    {
        IRpn ToExpression(StackExpressions stack);
        bool IsTerminal();
    }
}