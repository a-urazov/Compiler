using System;
using System.Collections.Generic;
using AST;
using Compiler;
using Tokenizer;

namespace Parsing
{
    /// <summary>
    /// Parser
    /// </summary>
    public sealed partial class Parser
    {
        /// <summary>
        /// Lexer
        /// </summary>
        private Lexer Lexer { get; }

        /// <summary>
        /// Current token
        /// </summary>
        private Token CurrentToken { get; set; }

        /// <summary>
        /// Peeked token
        /// </summary>
        private Token PeekedToken { get; set; }

        /// <summary>
        /// Statement parsers
        /// </summary>
        private Dictionary<Token.Type, Func<Statement>> StatementParser { get; } = new();

        /// <summary>
        /// Prefix parsers
        /// </summary>
        private Dictionary<Token.Type, Func<Expression>> LiteralParser { get; } = new();

        /// <summary>
        /// Infix parsers
        /// </summary>
        private Dictionary<Token.Type, Func<Expression, Expression>> ExpressionParser { get; } = new();

        /// <summary>
        /// Create parser
        /// </summary>
        /// <param name="lexer">Lexer</param>
        public Parser(Lexer lexer)
        {
            Lexer = lexer;

            Next(2); // Set Current & Peeked Tokens

            // Fill statement parsers
            StatementParser.Add(Token.Type.Let, ParseLetStatement);
            StatementParser.Add(Token.Type.Function, ParseFucntionStatement);
            StatementParser.Add(Token.Type.Return, ParseReturnStatement);

            // Fill prefix parsers
            LiteralParser.Add(Token.Type.Identifier, () => new Identifier(CurrentToken));
            LiteralParser.Add(Token.Type.Number, () => new NumberLiteral(CurrentToken));
            LiteralParser.Add(Token.Type.Function, ParseFucntionLiteral);
            LiteralParser.Add(Token.Type.Class, ParseClassLiteral);

            // Fill infix parsers
            ExpressionParser.Add(Token.Type.Assign, ParseBinaryOperatorExpression(Token.Type.Assign));
            ExpressionParser.Add(Token.Type.Plus, ParseBinaryOperatorExpression(Token.Type.Plus));
            ExpressionParser.Add(Token.Type.Minus, ParseBinaryOperatorExpression(Token.Type.Minus));
            ExpressionParser.Add(Token.Type.Astersk, ParseBinaryOperatorExpression(Token.Type.Astersk));
            ExpressionParser.Add(Token.Type.Slash, ParseBinaryOperatorExpression(Token.Type.Slash));
            ExpressionParser.Add(Token.Type.Inc, ParseUnaryOperator(Token.Type.Inc));
            ExpressionParser.Add(Token.Type.Dec, ParseUnaryOperator(Token.Type.Dec));
        }

        /// <summary>
        /// Parse unary operator
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Func<Expression, Expression> ParseUnaryOperator(Token.Type type) =>
            (Expression left) =>
            {
                if (PeekedToken == type) Next();
                else return null;

                var expression = new UnaryOperatorExpression(CurrentToken)
                {
                    Expression = left
                };

                Next();

                return expression;
            };

        /// <summary>
        /// Parse expression
        /// </summary>
        /// <param name="priority">Operation priority</param>
        /// <returns>Expression</returns>
        private Expression ParseExpression(OperationPriority priority = OperationPriority.Minimal)
        {
            if (!LiteralParser.ContainsKey(CurrentToken.T)) return null;

            var expression = LiteralParser[CurrentToken.T]();

            if (CurrentToken != Token.Type.Semicolon && priority < Priority(PeekedToken.T)) return (!ExpressionParser.ContainsKey(PeekedToken.T)) ? expression : ExpressionParser[PeekedToken.T](expression);

            return expression;
        }

        /// <summary>
        /// Move to a next token
        /// </summary>
        private void Next()
        {
            CurrentToken = PeekedToken;
            PeekedToken = Lexer.NextToken;
        }

        /// <summary>
        /// Move to step token
        /// </summary>
        /// <param name="step">Steps</param>
        private void Next(int step = 1)
        {
            for (var i = 0; i < step; ++i) Next();
        }

        /// <summary>
        /// Parse source code
        /// </summary>
        /// <returns>AST</returns>
        public AsymptoticTree Parse
        {
            get
            {
                var program = new AsymptoticTree();

                while (!CurrentToken.End())
                {
                    try
                    {
                        var statement = ParseStatement ?? ParseExpressionStatement;
                        if (statement != null) program.Add(statement);
                        Next();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message, e.Source);
                        return new AsymptoticTree();
                    }
                }

                return program;
            }
        }

        private Statement ParseExpressionStatement
        {
            get
            {
                if (CurrentToken == Token.Type.Semicolon) return null;

                var statement = new ExpressionStatement(CurrentToken)
                {
                    Expression = ParseExpression()
                };

                if (PeekedToken == Token.Type.Semicolon) Next();

                return statement;
            }
        }

        /// <summary>
        /// Parse Statement
        /// </summary>
        /// <returns>Statement</returns>
        private Statement ParseStatement => StatementParser.ContainsKey(CurrentToken.T) ? StatementParser[CurrentToken.T]() : null;
    }
}