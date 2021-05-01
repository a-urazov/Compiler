using System.Collections.Generic;
using Tokenizer;
using System.Linq;

namespace AST
{
    public class FunctionStatement : Statement
    {
        private static string Open => Blocks.Literals[Token.Type.LeftBrace];
        private static string Close => Blocks.Literals[Token.Type.RightBrace];

        public IList<Identifier> Parameters { get; } = new List<Identifier>();
        public IList<Statement> Statements { get; } = new List<Statement>();
        public Identifier Name { get; set; }
        public Expression Return { get; set; }
        public FunctionStatement(Token token) : base(token)
        {
        }

        public override string Source => Statements.Count == 0 && Return != null
            ? $"{Literal} ({string.Join(", ", Parameters.Select(x => x.Source))}) => {Return.Source};"
            : $"{Literal} ({string.Join(", ", Parameters.Select(x => x.Source))}) {Open} {string.Join(" ", Statements.Select(x => x.Source))} {Close}";
    }
}