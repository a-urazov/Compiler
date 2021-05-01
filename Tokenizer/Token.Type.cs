namespace Tokenizer
{
    public sealed partial class Token
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
    }
}