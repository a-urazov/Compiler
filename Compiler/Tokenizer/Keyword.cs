using System.Collections.Generic;

namespace Tokenizer
{
    /// <summary>
    /// Keywords of tokens
    /// </summary>
    public static class Keyword
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
        /// Tokens as string literal keywords
        /// </summary>
        public static IDictionary<Token.Type, string> Literals { get; } = new Dictionary<Token.Type, string>()
        {
            { Token.Type.Let, "let" },
            { Token.Type.Const, "const" },
            { Token.Type.Static, "static" },
            { Token.Type.Field, "field" },
            { Token.Type.Virtual, "virtual" },
            { Token.Type.Override, "override" },
            { Token.Type.Property, "property" },
            { Token.Type.Set, "set" },
            { Token.Type.Get, "get" },
            { Token.Type.Class, "class" },
            { Token.Type.Interface, "interface" },
            { Token.Type.Struct, "struct" },
            { Token.Type.Type, "type" },
            { Token.Type.Enum, "enum" },
            { Token.Type.For, "for" },
            { Token.Type.Case, "case" },
            { Token.Type.In, "in" },
            { Token.Type.If, "if" },
            { Token.Type.Else, "else" },
            { Token.Type.Is, "is" },
            { Token.Type.Of, "of" },
            { Token.Type.Continue, "continue" },
            { Token.Type.Break, "break" },
            { Token.Type.Return, "return" },
            { Token.Type.Private, "private" },
            { Token.Type.Public, "public" },
            { Token.Type.Protected, "protected" },
            { Token.Type.Sealed, "sealed" },
            { Token.Type.Internal, "internal" },
            { Token.Type.ReadOnly, "readonly" },
            { Token.Type.Mutable, "mutable" },
            { Token.Type.Service, "service" },
            { Token.Type.True, "true" },
            { Token.Type.False, "false" },
            { Token.Type.Try, "try" },
            { Token.Type.Catch, "catch" },
            { Token.Type.Finally, "finally" },
            { Token.Type.New, "new" },
            { Token.Type.Throw, "throw" },
            { Token.Type.Thread, "thread" },
            { Token.Type.Async, "async" },
            { Token.Type.Await, "await" },
            { Token.Type.Sync, "sync" },
            { Token.Type.Function, "fn" },
        };

        /// <summary>
        /// Keywords as token types
        /// </summary>
        public static IDictionary<string, Token.Type> Tokens { get; } = Literals.Reverse();
    }
}
