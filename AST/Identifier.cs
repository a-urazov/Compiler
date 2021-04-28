using Tokenizer;

namespace AST
{
    /// <summary>
    /// Identifier of any statemnt in source
    /// </summary>
    public sealed class Identifier : Expression
    {
        /// <summary>
        /// Value of identifier
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token">Token</param>
        public Identifier(Token token) : base(token)
        {
            Value = token.Literal;
        }
    }
}