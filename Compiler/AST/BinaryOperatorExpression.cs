using System;
using Tokenizer;

namespace AST
{
    public sealed class BinaryOperatorExpression : Expression
    {
        public Expression LeftExpression { get; set; }
        public Expression RightExpression { get; set; }

        public BinaryOperatorExpression(Token token) : base(token)
        {
        }

        public override string Source => $"{LeftExpression.Source} {Literal} {RightExpression.Source}";
    }
}
