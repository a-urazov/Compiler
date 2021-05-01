using System;

namespace Tokenizer
{
    /// <summary>
    /// Token
    /// </summary>
    public sealed partial class Token
    {
        /// <summary>
        /// Type of token
        /// </summary>
        public Type T { get; }

        /// <summary>
        /// Token string literal
        /// </summary>
        public string Literal { get; }

        /// <summary>
        /// Default contructor for illegal token
        /// </summary>
        public Token()
        {
            T = Type.Illegal;
            Literal = "";
        }

        /// <summary>
        /// Create token with single character
        /// </summary>
        /// <param name="type">Token type</param>
        /// <param name="character">Single char</param>
        public Token(Type type, char character)
        {
            T = type;
            Literal = character.ToString();
        }

        /// <summary>
        /// Create token for string literal
        /// </summary>
        /// <param name="type">Token type</param>
        /// <param name="literal">String literal</param>
        public Token(Type type, string literal)
        {
            T = type;
            Literal = literal;
        }

        /// <summary>
        /// Check is a last token
        /// </summary>
        /// <returns></returns>
        public bool End() => T == Type.EndOfFile || T == Type.Illegal;

        /// <summary>
        /// Stringable view of token
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{T}:{Literal}]";

        public override bool Equals(object obj) => obj is Token token &&
                   T == token.T &&
                   Literal == token.Literal;

        public override int GetHashCode() => HashCode.Combine(T, Literal);

        /// <summary>
        /// Overriden ==operator as helper for conduct types of tokens comparison
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="type">Type</param>
        /// <returns>Result comparison of types</returns>
        public static bool operator ==(Token token, Type type) => token.T == type;

        /// <summary>
        /// Overriden ==operator as helper for conduct types of tokens comparison
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="type">Type</param>
        /// <returns>Result comparison of types</returns>
        public static bool operator !=(Token token, Type type) => token.T != type;

        /// <summary>
        /// Overriden ==operator as helper for conduct types of tokens comparison
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="token">Token</param>
        /// <returns>Result comparison of types</returns>
        public static bool operator ==(Type type, Token token) => type == token.T;

        /// <summary>
        /// Overriden ==operator as helper for conduct types of tokens comparison
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="token">Token</param>
        /// <returns>Result comparison of types</returns>
        public static bool operator !=(Type type, Token token) => type != token.T;
    }
}
