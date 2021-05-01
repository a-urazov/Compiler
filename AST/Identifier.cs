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
        public string Value { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="token">Token</param>
        public Identifier(Token token) : base(token)
        {
            Value = Source;
        }

        public override string Source => Value;
    }
}