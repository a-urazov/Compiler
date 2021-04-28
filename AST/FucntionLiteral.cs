using System.Linq;
using System;
using System.Collections.Generic;
using Tokenizer;

namespace AST
{
    /// <summary>
    /// fn (<expression>...) { <statement>... }
    /// fn () => <expression>;
    /// </summary>
    public sealed class FucntionLiteral : Expression
    {
        private const string Begin = "{";
        private const string End = "}";

        public IList<Identifier> Parameters { get; private set; } = new List<Identifier>();
        public IList<Statement> Statements { get; private set; } = new List<Statement>();
        public Identifier Name { get; set; }
        public Expression Return { get; set; }
        public FucntionLiteral(Token token) : base(token)
        {
        }

        public override string Source() => Statements.Count == 0 && Return != null
                ? $"{Literal} ({string.Join(", ", Parameters.Select(x => x.Source()))}) => {Return.Source()}"
                : $"{Literal} ({string.Join(", ", Parameters.Select(x => x.Source()))}) {Begin} {string.Join(" ", Statements.Select(x => x.Source()))} {End}";
    }
}