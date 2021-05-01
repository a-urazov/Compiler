namespace Compiler
{
    public sealed partial class Lexer
    {
        /// <summary>
        /// Literal reader types
        /// </summary>
        private enum Reader { String, Number, Identifier, Literal }
    }
}