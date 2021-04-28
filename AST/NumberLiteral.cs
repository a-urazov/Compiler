using System.Globalization;
using System;
using Tokenizer;

namespace AST
{
    public sealed class NumberLiteral : Expression
    {
        private int IntValue { get; set; }
        private uint UIntValue { get; set; }
        private float FloatValue { get; set; }
        private double DoubleValue { get; set; }
        private Int64 HexValue { get; set; }
        private Int64 BinValue { get; set; }

        public NumberLiteral(Token token) : base(token)
        {
            if (token.Literal.Contains("0x")) 
            {
                HexValue = Convert.ToInt64(token.Literal, 16);
                return;
            }
            if (token.Literal.Contains("0b")) 
            {
                BinValue = Convert.ToInt64(token.Literal, 2);
                return;
            }
            if (token.Literal.Contains("."))
                if (token.Literal.Contains("E+") || token.Literal.Contains("e+")) DoubleValue = double.Parse(token.Literal);
                else FloatValue = float.Parse(token.Literal);
            else if (token.Literal.Contains("E+") || token.Literal.Contains("e+")) DoubleValue = double.Parse(token.Literal);
            else
            {
                if (token.Literal.Contains("-")) IntValue = int.Parse(token.Literal);
                else UIntValue = uint.Parse(token.Literal);
            }
        }

        public IConvertible Value()
        {
            if (IntValue != 0) return IntValue;
            if (UIntValue != 0) return UIntValue;
            if (FloatValue != 0) return FloatValue;
            if (DoubleValue != 0) return DoubleValue;
            return null;
        }
    }
}
