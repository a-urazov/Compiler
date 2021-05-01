using System.Linq;
using System.Collections.Generic;
using Tokenizer;

namespace AST
{
    /// <summary>
    /// Let Statement
    /// let <identifier> = <expression>; //single
    /// let (<identifier>...i) = (<expression>...i)
    /// </summary>
    public sealed class LetStatement : Statement
    {
        /// <summary>
        /// Name of let identifier
        /// </summary>
        public Identifier Name { get; set; }

        /// <summary>
        /// Value expression of let statement
        /// </summary>
        public Expression Value { get; set; }

        public IList<Identifier> Names { get; set; } = new List<Identifier>();

        public Dictionary<Identifier, Expression> Values { get; set; } = new();

        public Type TypeOf { get; set; }

        /// <summary>
        /// Constructor - let <identifier> = <expression>;
        /// </summary>
        /// <param name="token">Token</param>
        public LetStatement(Token token) : base(token)
        {
        }

        private string Open => TypeOf == Type.Array ? Keyword.Literals[Token.Type.LeftBracket] : (TypeOf == Type.Cortege ? Keyword.Literals[Token.Type.LeftParen] : "");
        private string Close => TypeOf == Type.Array ? Keyword.Literals[Token.Type.RightBracket] : (TypeOf == Type.Cortege ? Keyword.Literals[Token.Type.RightParen] : "");

        /// <summary>
        /// Original constructed source of token by tokens
        /// </summary>
        /// <returns>Source view</returns>
        public override string Source => IsMany()
            ? $"{Literal} {Open}{string.Join(", ", Names.Select(x => x.Source))}{Close} = {Open}{string.Join(", ", Values.Select(x => x.Value.Source))}{Close};"
            : $"{Literal} {Name.Value} = {Value.Source};";

        private bool IsMany() => 
            (Value == null && Name == null && Names.Count != 0 && Values.Count == Names.Count) ||
            (Value != null && Name == null && Names.Count != 0 && Values.Count == 0);
    }
}
