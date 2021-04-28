using System;
using Tokenizer;

namespace AST
{
    public sealed class UnaryOperatorExpression : Expression
    {
        public Expression Expression { get; set; }

        public UnaryOperatorExpression(Token token) : base(token)
        {
        }

        public override string Source() => $"{Literal}{Expression.Source()}";
    }
}
