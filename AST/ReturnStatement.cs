using Tokenizer;

namespace AST
{
    public class ReturnStatement : Statement
    {
        public Expression Value { get; set; }

        public ReturnStatement(Token token) : base(token)
        {
        }

        public override string Source => $"{Literal} {Value.Source};";
    }
}