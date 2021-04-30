using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Tokenizer;

namespace AST
{
    public sealed class ClassLiteral : Expression
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

        public ClassLiteral(Token token) : base(token)
        {
        }
    }
}