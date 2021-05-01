using System;
using AST;

namespace Parsing
{
    public sealed partial class Parser
    {
        private Func<Statement> ParseReturnStatement
        {
            get => () =>
            {
                var statement = new ReturnStatement(CurrentToken);
                Next();
                statement.Value = ParseExpression();
                return statement;
            };
        }
    }
}