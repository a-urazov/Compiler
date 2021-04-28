using Tokenizer;

namespace AST
{
    public abstract class Node : INode
    {
        private Token Token { get; }

        protected Node(Token token)
        {
            Token = token;
        }

        protected string Literal => Token.Literal;
        protected Token.Type Type => Token.T;

        public abstract string Source();
    }

    public interface INode
    {
        string Source();
    }
}