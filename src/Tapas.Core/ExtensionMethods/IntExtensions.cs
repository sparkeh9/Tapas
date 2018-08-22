namespace Tapas.Core.ExtensionMethods
{
    public static class IntExtensions
    {
        public static int MinimumValue( this int operand, int minimum = 0 )
        {
            return operand > minimum
                ? operand
                : minimum;
        }
    }
}