using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Tokenizer;

namespace AST
{
    public sealed partial class ClassMember : INode
    {
        public Statement Statement { get; }

        public IDictionary<Qualifier, Token.Type> Qualifiers { get; } = new Dictionary<Qualifier, Token.Type>()
        {
            { Qualifier.Public, Token.Type.Public },
            { Qualifier.Private, Token.Type.Private },
            { Qualifier.Protected, Token.Type.Protected },
            { Qualifier.Static, Token.Type.Static },
            { Qualifier.Const, Token.Type.Const },
            { Qualifier.Async, Token.Type.Async },
            { Qualifier.None, Token.Type.Illegal },
        };

        public Qualifier Access { get; set; }
        public Qualifier Static { get; set; }
        public Qualifier Async { get; set; }
        public Qualifier Const { get; set; }

        public ClassMember(Statement statement)
        {
            Statement = statement;
        }

        public string Source
        {
            get
            {
                StringBuilder result = new();

                if (Keyword.Literals.ContainsKey(Qualifiers[Access])) result.Append(Keyword.Literals[Qualifiers[Access]]).Append(' ');
                if (Keyword.Literals.ContainsKey(Qualifiers[Static])) result.Append(Keyword.Literals[Qualifiers[Static]]).Append(' ');
                if (Keyword.Literals.ContainsKey(Qualifiers[Const])) result.Append(Keyword.Literals[Qualifiers[Const]]).Append(' ');
                if (Keyword.Literals.ContainsKey(Qualifiers[Async])) result.Append(Keyword.Literals[Qualifiers[Async]]).Append(' ');

                return result.ToString();
            }
        }
    }
}