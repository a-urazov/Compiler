using System.Collections.Generic;
using System.Linq;
using Tokenizer;

namespace AST
{
    public sealed class ClassStatement : Statement
    {
        private static string Open => Blocks.Literals[Token.Type.LeftBrace];
        private static string Close => Blocks.Literals[Token.Type.RightBrace];

        public Identifier Name { get; set; }

        public IList<Identifier> Inheritances { get; } = new List<Identifier>();

        public IList<ClassMember> Fields { get; } = new List<ClassMember>();

        public IList<ClassMember> Methods { get; } = new List<ClassMember>();

        public ClassStatement(Token token) : base(token)
        {
        }

        public override string Source => Inheritances.Count != 0
            ? $"{Literal} {Name} : {string.Join(", ", Inheritances.Select(x => x.Source))} {Open} {string.Join(" ", Fields.Select(x => x.Source + x.Statement.Source))} {string.Join(" ", Methods.Select(x => x.Source + x.Statement.Source))} {Close}"
            : $"{Literal} {Name} {Open} {string.Join(" ", Fields.Select(x => x.Source + x.Statement.Source))} {string.Join(" ", Methods.Select(x => x.Source + x.Statement.Source))} {Close}";
    }
}