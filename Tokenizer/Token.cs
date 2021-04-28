using System;

namespace Tokenizer
{
    /// <summary>
    /// Token
    /// </summary>
    public sealed class Token
    {
        /// <summary>
        /// Token type
        /// </summary>
        public enum Type
        {
            Illegal,
            Arrow,
            EndOfFile,
            Identifier,
            UnsignedInt,
            SignedInt,
            Number,
            Float,
            Double,
            Real,
            Complex,
            String,
            Array,
            Object,
            Char,
            Byte,
            Assign,
            Plus,
            Minus,
            Bang,
            Astersk,
            Slash,
            Lt,
            Gt,
            LtEq,
            GtEq,
            Equal,
            NotEqual,
            Comma,
            Semicolon,
            Colon,
            LeftParen,
            RightParen,
            LeftBrace,
            RightBrace,
            LeftBracket,
            Quote,
            DoubleQuote,
            RightBracket,
            Let,
            Class,
            Interface,
            Struct,
            Function,
            For,
            In,
            Case,
            True,
            False,
            If,
            Else,
            Return,
            Sync,
            Async,
            Await,
            Const,
            Thread,
            Inc,
            Dec,
            PlusAssign,
            MinusAssign,
            AsteriskAssign,
            SlashAssign,
            Percent,
            PercentAssign,
            Continue,
            Break,
            Dot,
            Type,
            Diapason,
            Etc,
            BitWiseLeft,
            BitWiseRight,
            BitAnd,
            BitOr,
            And,
            Or,
            SpaceShip,
            Pipe,
            Try,
            Catch,
            Finally,
            Throw,
            Is,
            Of,
            Enum,
            Field,
            Get,
            Set,
            Public,
            Private,
            Protected,
            Static,
            Sealed,
            Internal,
            ReadOnly,
            Mutable,
            Service,
            Property,
            Override,
            Virtual,
            New,
        }

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

        public static bool operator ==(Token token, Token.Type type) => token.T == type;

        public static bool operator !=(Token token, Token.Type type) => token.T != type;
    }
}
