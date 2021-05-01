using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Tokenizer;

namespace AST
{
    public sealed class ClassMember : INode
    {
        public Statement Statement { get; }

        public Qualifier.Type Access { get; set; }
        public Qualifier.Type Static { get; set; }
        public Qualifier.Type Async { get; set; }

        public ClassMember(Statement statement)
        {
            Statement = statement;
        }

        public string Source
        {
            get
            {
                StringBuilder result = new();

                if (Keyword.Literals.ContainsKey(Qualifier.Tokens[Access])) result.Append(Keyword.Literals[Qualifier.Tokens[Access]]).Append(' ');
                if (Keyword.Literals.ContainsKey(Qualifier.Tokens[Static])) result.Append(Keyword.Literals[Qualifier.Tokens[Static]]).Append(' ');
                if (Keyword.Literals.ContainsKey(Qualifier.Tokens[Async])) result.Append(Keyword.Literals[Qualifier.Tokens[Async]]).Append(' ');

                return result.ToString();
            }
        }
    }
}