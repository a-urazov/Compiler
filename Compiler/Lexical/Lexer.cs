using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Compiler
{
    /// <summary>
    /// Lexer
    /// </summary>
    public sealed partial class Lexer
    {
        /// <summary>
        /// Source code of program
        /// </summary>
        private string Source { get; }

        /// <summary>
        /// Current cursor position
        /// </summary>
        private int Position { get; set; }

        /// <summary>
        /// Current readed cursor position
        /// </summary>
        private int ReadPosition { get; set; }

        /// <summary>
        /// Current character under cursor
        /// </summary>
        private char Char { get; set; }

        /// <summary>
        /// Skip white spaces
        /// </summary>
        /// <param name="contidion">Additional condition for character skiping</param>
        private void Skip(bool contidion = false)
        {
            if (contidion) while (char.IsWhiteSpace(Char) && contidion) Next();
            while (char.IsWhiteSpace(Char)) Next();
        }

        /// <summary>
        /// Move cursor to next character in source code
        /// </summary>
        private void Next()
        {
            Char = Peek;
            Position = ReadPosition;
            ++ReadPosition;
        }

        /// <summary>
        /// Move cursor to step size in source code
        /// </summary>
        /// <param name="step">How many steps needs to be skiped</param>
        private void Next(int step)
        {
            for (var i = 0; i < step; ++i) Next();
        }

        /// <summary>
        /// Peek next char without replace current char
        /// </summary>
        /// <returns></returns>
        private char Peek => ReadPosition >= Source.Length ? char.MinValue : Source[ReadPosition];

        /// <summary>
        /// Peek for passed steps count
        /// </summary>
        /// <param name="step">Steps count</param>
        /// <returns></returns>
        private char PeekOn(int step = 1) => ReadPosition >= Source.Length ? char.MinValue : Source[ReadPosition + step - 1];

        /// <summary>
        /// Current character type comparator
        /// </summary>
        /// <param name="c">Current character</param>
        /// <returns>Instance of comparator</returns>
        private static Comparator Is(char c) => new(c);

        /// <summary>
        /// Read token literals
        /// </summary>
        /// <param name="type">Type of token literal</param>
        /// <returns>Token literal</returns>
        private string Read(Reader type = Reader.Identifier)
        {
            var position = Position;
            switch (type)
            {
                case Reader.String:
                    do Next(); // Fill string for condition downside
                    while (!Is(Char).String && Char != char.MinValue);
                    break;
                case Reader.Number:
                    if (Char == '-') Next(); // Skip sign
                    if (Char == '0')
                    {
                        switch (Peek)
                        {
                            case 'b':
                            case 'x':
                                Next(2);
                                while (Is(Char).Digit) Next(); // Fill Secand part of number
                                return Source[position..Position];
                        }
                    }
                    while (Is(Char).Digit) Next(); // Read number
                    if (Char == '.')
                    {
                        Next(); // Skip dot for float
                        while (Is(Char).Digit) Next(); // Fill Secand part of number
                    }
                    if (Char == 'E' || Char == 'e')
                    {
                        Next(2); // e+|E+ skipping
                        while (Is(Char).Digit) Next(); // Fill Secand part of number
                    }
                    break;
                case Reader.Identifier:
                    while (Is(Char).Letter) Next(); // Fill identifier
                    break;
                case Reader.Literal:
                    var character = Char;
                    Next();
                    var next = Char != char.MinValue ? Char.ToString() : "";
                    return (character + (Is(Char).Block ? "" : next)).Trim();
            }

            return Source[position..Position];
        }

        /// <summary>
        /// Make standard Token creator functon
        /// </summary>
        /// <param name="literal">Token literal from source</param>
        /// <returns>Token</returns>
        private static Func<Token> FnOperator(string literal) => () => new Token(Operators.Tokens[literal], literal);

        /// <summary>
        /// Make standard Token creator functon
        /// </summary>
        /// <param name="literal">Token literal from source</param>
        /// <returns>Token</returns>
        private static Func<Token> FnBlock(string literal) => () => new Token(Blocks.Tokens[literal], literal);

        /// <summary>
        /// Make custom token creator fucntion
        /// </summary>
        /// <param name="fn">Custom function</param>
        /// <returns>Token</returns>
        private static Func<Token> F(Func<Token> fn) => fn;

        /// <summary>
        /// Lexer constructor
        /// </summary>
        /// <param name="source">Source code of program</param>
        public Lexer(string source)
        {
            Source = source;

            /// Fill operators token handlers
            foreach (var (token, literal) in Operators.Literals)
            {
                switch (token)
                {
                    case Token.Type.Minus:
                        Tokens.Add(literal, F(() =>
                        {
                            if (Char == '-' && Is(Peek).Digit) return new Token(Token.Type.Number, Read(Reader.Number));
                            return new Token(Token.Type.Minus, literal);
                        }));
                        continue;
                    default:
                        Tokens.Add(literal, FnOperator(literal));
                        continue;
                }
            }

            /// Fill blocks token handlers
            foreach (var (token, literal) in Blocks.Literals)
            {
                switch (token)
                {
                    case Token.Type.Quote:
                    case Token.Type.DoubleQuote:
                        Tokens.Add(literal, F(() =>
                        {
                            // Skip '"|'' after string is start
                            // Handly set quotes around the string as literal string and read full string literal
                            Next();
                            var token = new Token(Token.Type.String, $"{literal}{Read(Reader.String)}{literal}");
                            Next(); // Skip '"|'' after string is end
                            return token; // return string literal with quotes
                        }));
                        continue;
                    default:
                        Tokens.Add(literal, FnBlock(literal));
                        continue;
                }
            }

            Next(); // Set first character for initialize algorithm
        }

        /// <summary>
        /// Reserved symbolic operators table
        /// </summary>
        private Dictionary<string, Func<Token>> Tokens { get; } = new();

        /// <summary>
        /// Make token
        /// </summary>
        /// <param name="literal">Literal of token</param>
        /// <param name="skip">How many characters needs to skip</param>
        /// <returns>Token</returns>
        private Token Make(string literal, int skip = 0)
        {
            Next(skip);
            return Tokens.ContainsKey(literal) ? Tokens[literal]() : new Token();
        }

        /// <summary>
        /// Switch cursor to next token
        /// </summary>
        /// <returns>Token</returns>
        public Token NextToken
        {
            get
            {
                // Skip all whitespaces after current character
                Skip();

                // If end of source code we simply return
                if (Char == char.MinValue) return new Token(Token.Type.EndOfFile, char.MinValue);

                StringBuilder literal = new();
                string key;

                // If is letter than is exactly identifier
                if (Is(Char).Letter)
                {
                    literal.Append(Read(Reader.Identifier));
                    key = literal.ToString();
                    var type = Keyword.Tokens.ContainsKey(key)
                        ? Keyword.Tokens[key]
                        : Token.Type.Identifier;
                    return new Token(type, type != Token.Type.Identifier ? Keyword.Literals[type] : key);
                }
                // Char is digit then is exactly a number
                if (Is(Char).Digit) return new Token(Token.Type.Number, Read(Reader.Number));

                // Otherwise is one of the binary or unary operators
                var character = Char.ToString(); // Save value of start character for operator

                // Is string make string
                if (Is(Char).String) return Make(character);

                // If block make token and skip it for cursor
                if (Is(Char).Block) return Make(character, Skip(1));

                // Fill operator behaind it will not end as token
                while (Is(Char).Operator)
                {
                    var next = Peek;
                    if (Is(Char).SignedDigit(next)) break; // hack for signed digit
                    literal.Append(Read(Reader.Literal));
                    if (!Is(next).Operator) break;
                    Next();
                }

                // Complete to write operator token literal
                key = literal.ToString();

                // Return operator or illegal token
                return Make(key != "" ? key : character);
            }
        }

        /// <summary>
        /// Shugar like skip helper pure function
        /// </summary>
        /// <param name="v">Skip steps</param>
        /// <returns>Skip steps</returns>
        private static int Skip(int v) => v;
    }
}