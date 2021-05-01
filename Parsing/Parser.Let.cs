using System;
using System.Collections.Generic;
using AST;
using Tokenizer;

namespace Parsing
{
    public sealed partial class Parser
    {
        private Func<Statement> ParseLetStatement
        {
            get => () =>
            {
                var statement = new LetStatement(CurrentToken);

                if (PeekedToken == Token.Type.Identifier)
                {
                    Next();

                    statement.Name = new Identifier(CurrentToken);

                    if (PeekedToken == Token.Type.Assign) Next(2);
                    else return null;

                    statement.Value = ParseExpression();

                    if (typeof(FucntionLiteral).IsInstanceOfType(statement.Value)) (statement.Value as FucntionLiteral).Name = statement.Name;
                }
                else if (PeekedToken == Token.Type.LeftParen)
                {
                    Next(2);
                    statement.TypeOf = Statement.Type.Cortege;
                    while (CurrentToken != Token.Type.RightParen)
                    {
                        if (CurrentToken == Token.Type.Comma)
                        {
                            Next();
                            continue;
                        }
                        if (ParseExpression() is Identifier name) statement.Names.Add(name);
                        Next();
                    }

                    if (PeekedToken == Token.Type.Assign) Next();
                    else return null;

                    if (PeekedToken == Token.Type.LeftParen)
                    {
                        var names = new List<Identifier>();
                        foreach (var name in statement.Names) names.Add(name);
                        Next(2);
                        while (CurrentToken != Token.Type.RightParen)
                        {
                            if (CurrentToken == Token.Type.Comma)
                            {
                                Next();
                                continue;
                            }
                            var expression = ParseExpression();
                            if (expression != null)
                            {
                                var name = names[0];
                                names.Remove(name);
                                statement.Values.Add(name, expression);
                            }
                            Next();
                        }
                    }
                }
                return statement;
            };
        }
    }
}