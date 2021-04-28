using System;
using Tokenizer;

namespace AST
{
    public class Expression : Node
    {
        public Expression(Token token) : base(token)
        {
        }

        public override string Source() => Literal;
    }
}
