using System;
using System.Collections.Generic;

namespace Tokenizer
{
    /// <summary>
    /// Blocks symbols
    /// </summary>
    public static class Blocks
    {
        /// <summary>
        /// Function for reverse key and value arrange of map
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="source">Map</param>
        /// <returns>Reversed map of keys and values range</returns>
        private static IDictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var dictionary = new Dictionary<TValue, TKey>();
            foreach (var entry in source) if (!dictionary.ContainsKey(entry.Value)) dictionary.Add(entry.Value, entry.Key);
            return dictionary;
        }

        /// <summary>
        /// Tokens as string literal blocks
        /// </summary>
        public static IDictionary<Token.Type, string> Literals { get; } = new Dictionary<Token.Type, string>()
        {
            { Token.Type.LeftBrace, "{" },
            { Token.Type.RightBrace, "}" },
            { Token.Type.LeftParen, "(" },
            { Token.Type.RightParen, ")" },
            { Token.Type.LeftBracket, "[" },
            { Token.Type.RightBracket, "]" },
            { Token.Type.DoubleQuote, "\"" },
            { Token.Type.Quote, "'" },
        };

        /// <summary>
        /// Tokens for blocks
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, Token.Type> Tokens { get; } = Literals.Reverse();
    }
}
