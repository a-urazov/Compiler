using System;
using System.Collections.Generic;

namespace Tokenizer
{
    /// <summary>
    /// Operators hash map
    /// </summary>
    public static class Operators
    {
        /// <summary>
        /// Function for reverse key and value arrange of map
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="source">Map</param>
        /// <returns>Reversed map of keys and values range</returns>
        private static Dictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var dictionary = new Dictionary<TValue, TKey>();
            foreach (var entry in source) if (!dictionary.ContainsKey(entry.Value)) dictionary.Add(entry.Value, entry.Key);
            return dictionary;
        }

        /// <summary>
        /// Tokens as string literal keywords
        /// </summary>
        public static Dictionary<Token.Type, string> Literals { get; } = new()
        {
            { Token.Type.Assign, "=" },
            { Token.Type.SpaceShip, "<=>"},
            { Token.Type.Plus, "+" },
            { Token.Type.Inc, "++" },
            { Token.Type.Dec, "--" },
            { Token.Type.PlusAssign, "+=" },
            { Token.Type.MinusAssign, "-=" },
            { Token.Type.AsteriskAssign, "*=" },
            { Token.Type.SlashAssign, "/=" },
            { Token.Type.PercentAssign, "%=" },
            { Token.Type.Percent, "%" },
            { Token.Type.Minus, "-" },
            { Token.Type.Astersk, "*" },
            { Token.Type.Slash, "/" },
            { Token.Type.Semicolon, ";" },
            { Token.Type.Dot, "." },
            { Token.Type.Diapason, ".." },
            { Token.Type.Etc, "..." },
            { Token.Type.Comma, ","},
            { Token.Type.Arrow, "=>" },
        };

        /// <summary>
        /// Keywords as token types
        /// </summary>
        public static Dictionary<string, Token.Type> Tokens { get; } = Literals.Reverse();
    }
}
