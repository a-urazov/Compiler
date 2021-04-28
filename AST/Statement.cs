using Tokenizer;

namespace AST
{
    public class Statement : Node
    {
        protected Statement(Token token) : base(token)
        {
        }

        public override string Source() => Literal;
    }
}