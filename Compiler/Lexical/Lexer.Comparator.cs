namespace Compiler
{
    public sealed partial class Lexer
    {
        /// <summary>
        /// Charater type comparator
        /// </summary>
        private struct Comparator
        {
            /// <summary>
            /// Character under scope
            /// </summary>
            private char Char { get; }

            /// <summary>
            /// Charater type comparator contructor
            /// </summary>
            /// <param name="c">Char to needs detect type</param>
            public Comparator(char c) { Char = c; }

            /// <summary>
            /// Check is character digit
            /// </summary>
            public bool Digit { get => char.IsDigit(Char); }

            /// <summary>
            /// Check is character letter
            /// </summary>
            public bool Letter { get => char.IsLetter(Char) || Char == '_'; }

            /// <summary>
            /// Check is character symbolic operator
            /// </summary>
            public bool Operator { get => !Letter && !Digit && !char.IsWhiteSpace(Char) && !Block && Char != char.MinValue; }

            /// <summary>
            /// Check is string begin
            /// </summary>
            public bool String { get => Char == '"' || Char == '\''; }

            /// <summary>
            /// Detect is char block begin or end
            /// </summary>
            public bool Block { get => Char == '{' || Char == '(' || Char == '[' || Char == ']' || Char == ')' || Char == '}'; }

            public bool SignedDigit(char next) => Char == '-' && char.IsDigit(next);
        }
    }
}