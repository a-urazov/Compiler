using System.Collections.Generic;
using Tokenizer;

namespace AST
{
    public sealed class ClassStatement : Statement
    {
        public Identifier Name { get; set; }

        public IList<Identifier> Inheritances { get; set; } = new List<Identifier>();

        [SecurityMemberAttribute]
        [StaticMemberAttribute]
        [ConstMemberAttribute]
        public IList<Statement> Fields { get; set; } = new List<Statement>();

        [SecurityMemberAttribute]
        [StaticMemberAttribute]
        [ConstMemberAttribute]
        [AsyncMemberAttribute]
        public IList<Statement> Methods { get; set; } = new List<Statement>();

        public ClassStatement(Token token) : base(token)
        {
        }
    }
}