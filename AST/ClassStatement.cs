using System.Collections.Generic;
using Tokenizer;

namespace AST
{
    public sealed class ClassStatement : Statement
    {
        public IList<Statement> Fields { get; set; } = new List<Statement>();
        public IList<Statement> Methods { get; set; } = new List<Statement>();

        public ClassStatement(Token token) : base(token)
        {
        }
    }
}