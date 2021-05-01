using System;
using System.Globalization;
using Tokenizer;

namespace AST
{
    public sealed class NumberLiteral : Expression
    {
        private int IntValue { get; }
        private uint UIntValue { get; }
        private float FloatValue { get; }
        private double DoubleValue { get; }

        public NumberLiteral(Token token) : base(token)
        {
            if (token.Literal.Contains("."))
            {
                if (token.Literal.Contains("E+") || token.Literal.Contains("e+")) DoubleValue = double.Parse(token.Literal);
                else FloatValue = float.Parse(token.Literal);
            }
            else if (token.Literal.Contains("E+") || token.Literal.Contains("e+"))
            {
                DoubleValue = double.Parse(token.Literal);
            }
            else
            {
                if (token.Literal.Contains("-")) IntValue = int.Parse(token.Literal);
                else UIntValue = uint.Parse(token.Literal);
            }
        }

        public IConvertible Value
        {
            get {
                if (IntValue != 0) return IntValue;
                if (UIntValue != 0) return UIntValue;
                if (FloatValue != 0) return FloatValue;
                if (DoubleValue != 0) return DoubleValue;
                return null;
            }
        }
    }
}
