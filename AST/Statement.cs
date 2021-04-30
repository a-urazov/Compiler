using Tokenizer;

namespace AST
{
    public class Statement : Node
    {
        /// <summary>
        /// Type of statement
        /// </summary>
        public new enum Type
        {
            Array, Cortege, Single
        }

        protected Statement(Token token) : base(token)
        {
        }

        /// <summary>
        /// Return source
        /// </summary>
        /// <returns>Source</returns>
        public override string Source() => Literal;
    }
}