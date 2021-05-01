using System;
using AST;
using Tokenizer;

namespace Parsing
{
    public sealed partial class Parser
    {
        private Func<Expression> ParseFucntionLiteral
        {
            get => () =>
            {
                var expression = new FucntionLiteral(CurrentToken);

                if (PeekedToken == Token.Type.LeftParen) Next(2);
                else return null;

                while (CurrentToken != Token.Type.RightParen)
                {
                    if (CurrentToken == Token.Type.Comma) Next();
                    if (CurrentToken == Token.Type.Identifier && ParseExpression() is Identifier parameter)
                    {
                        expression.Parameters.Add(parameter);
                    }
                    Next();
                }

                if (PeekedToken == Token.Type.Arrow)
                {
                    Next(2);
                    expression.Return = ParseExpression();
                    return expression;
                }

                if (PeekedToken == Token.Type.LeftBrace) Next(2);

                while (CurrentToken != Token.Type.RightBrace)
                {
                    var statement = ParseStatement;
                    if (statement != null) expression.Statements.Add(statement);
                    Next();
                }

                return expression;
            };
        }

        private Func<Statement> ParseFucntionStatement
        {
            get => () =>
            {
                var statement = new FunctionStatement(CurrentToken);

                if (PeekedToken == Token.Type.Identifier)
                {
                    Next();
                    statement.Name = ParseExpression() as Identifier;
                }

                if (PeekedToken == Token.Type.LeftParen) Next(2);
                else return null;

                while (CurrentToken != Token.Type.RightParen)
                {
                    if (CurrentToken == Token.Type.Comma) Next();
                    if (CurrentToken == Token.Type.Identifier && ParseExpression() is Identifier parameter)
                    {
                        statement.Parameters.Add(parameter);
                    }
                    Next();
                }

                if (PeekedToken == Token.Type.Arrow)
                {
                    Next(2);
                    statement.Return = ParseExpression();
                    return statement;
                }

                if (PeekedToken == Token.Type.LeftBrace) Next(2);

                while (CurrentToken != Token.Type.RightBrace)
                {
                    var stmt = ParseStatement;
                    if (stmt != null) statement.Statements.Add(stmt);
                    Next();
                }

                return statement;
            };
        }
    }
}