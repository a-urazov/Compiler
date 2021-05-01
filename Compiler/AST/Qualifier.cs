using System.Collections.Generic;
using Tokenizer;

namespace AST
{
    public static partial class Qualifier
    {
        private static IDictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var dictionary = new Dictionary<TValue, TKey>();
            foreach (var entry in source) if (!dictionary.ContainsKey(entry.Value)) dictionary.Add(entry.Value, entry.Key);
            return dictionary;
        }

        public static IDictionary<Type, Token.Type> Tokens { get; } = new Dictionary<Type, Token.Type>()
        {
                { Type.Public, Token.Type.Public },
                { Type.Private, Token.Type.Private },
                { Type.Protected, Token.Type.Protected },
                { Type.Static, Token.Type.Static },
                { Type.Async, Token.Type.Async },
                { Type.None, Token.Type.Illegal },
            };

        public static IDictionary<Token.Type, Type> Qualifiers { get; } = Tokens.Reverse();

        public static (Type Access, Type Static, Type Async) Reset => (Access: Type.Private, Static: Type.None, Async: Type.None);
    }
}