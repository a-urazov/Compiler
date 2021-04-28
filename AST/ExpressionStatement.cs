using System;
using Tokenizer;

namespace AST
{
    /// <summary>
    /// Expression statement - <expression>;
    /// </summary>
    public sealed class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }

        public ExpressionStatement(Token token) : base(token)
        {
        }

        public override string Source() => $"{Expression.Source()};";
    }
}
