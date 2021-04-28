using System.Linq;
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
    public sealed class Parser
    {
        /// <summary>
        /// Operation priorities
        /// </summary>
        private enum OperationPriority
        {
            Minimal,
            Assign,
            Add = 2,
            Substract = Add,
            Multiplication = 3,
            Division = Multiplication,
            PartlesDivision = Division,
            Power,
            Call,
            Index,
        }

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
        private Dictionary<Token.Type, Func<Expression>> PrefixParser { get; } = new();

        /// <summary>
        /// Infix parsers
        /// </summary>
        private Dictionary<Token.Type, Func<Expression, Expression>> InfixParser { get; } = new();

        /// <summary>
        /// Priorities
        /// </summary>
        private Dictionary<Token.Type, OperationPriority> Priorities { get; } = new()
        {
            { Token.Type.Plus, OperationPriority.Add },
            { Token.Type.Minus, OperationPriority.Substract },
            { Token.Type.Astersk, OperationPriority.Multiplication },
            { Token.Type.Slash, OperationPriority.Division },
            { Token.Type.Assign, OperationPriority.Assign },
        };

        /// <summary>
        /// Create parser
        /// </summary>
        /// <param name="lexer">Lexer</param>
        public Parser(Lexer lexer)
        {
            Lexer = lexer;

            Next(2); // Set Current & Peeked Tokens

            // Fill statement parsers
            StatementParser.Add(Token.Type.Let, () =>
            {
                // todo: let [a, b] = [1, 2];
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
                    while (CurrentToken != Token.Type.RightParen)
                    {
                        if (CurrentToken == Token.Type.Comma) 
                        {
                            Next();
                            continue;
                        }
                        if (CurrentToken == Token.Type.Identifier)
                        {
                            var name = new Identifier(CurrentToken);
                            statement.Names.Add(name);
                        }
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
                                var name = names.First();
                                names.Remove(name);
                                statement.Values.Add(name, expression);
                            }
                            Next();
                        }
                    }
                }
                return statement;
            });

            // Fill prefix parsers
            PrefixParser.Add(Token.Type.Identifier, () => new Identifier(CurrentToken));
            PrefixParser.Add(Token.Type.Number, () => new NumberLiteral(CurrentToken));
            PrefixParser.Add(Token.Type.Function, () =>
            {
                var expression = new FucntionLiteral(CurrentToken);

                if (PeekedToken == Token.Type.LeftParen) Next(2);
                else return null;

                while (CurrentToken != Token.Type.RightParen)
                {
                    if (CurrentToken == Token.Type.Comma) Next();
                    if (CurrentToken == Token.Type.Identifier)
                    {
                        var parameter = ParseExpression() as Identifier;
                        if (parameter != null) expression.Parameters.Add(parameter);
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
                    var statement = ParseStatement();
                    if (statement != null) expression.Statements.Add(statement);
                    Next();
                }

                return expression;
            });

            // Fill infix parsers
            InfixParser.Add(Token.Type.Assign, ParseBinaryOperator(Token.Type.Assign));
            InfixParser.Add(Token.Type.Plus, ParseBinaryOperator(Token.Type.Plus));
            InfixParser.Add(Token.Type.Minus, ParseBinaryOperator(Token.Type.Plus));
            InfixParser.Add(Token.Type.Astersk, ParseBinaryOperator(Token.Type.Astersk));
            InfixParser.Add(Token.Type.Slash, ParseBinaryOperator(Token.Type.Slash));
            InfixParser.Add(Token.Type.Inc, ParseUnaryOperator(Token.Type.Inc));
            InfixParser.Add(Token.Type.Dec, ParseUnaryOperator(Token.Type.Dec));
        }

        /// <summary>
        /// Parse binary operator expression
        /// </summary>
        /// <param name="type">Type of binary operation</param>
        /// <returns>Expression with binary operator</returns>
        private Func<Expression, Expression> ParseBinaryOperator(Token.Type type)
        {
            return (Expression left) =>
            {
                if (PeekedToken == type) Next();
                else return null;

                var expression = new BinaryOperatorExpression(CurrentToken)
                {
                    LeftExpression = left
                };

                Next();

                expression.RightExpression = ParseExpression();

                return expression;
            };
        }

        /// <summary>
        /// Parse binary operator expression
        /// </summary>
        /// <param name="type">Type of binary operation</param>
        /// <returns>Expression with binary operator</returns>
        private Func<Expression> ParseBinaryOperatorStatement(Token.Type type)
        {
            return () =>
            {
                if (PeekedToken == type) Next();
                else return null;

                var expression = new BinaryOperatorExpression(CurrentToken);

                Next();

                expression.RightExpression = ParseExpression();

                return expression;
            };
        }

        /// <summary>
        /// Parse unary operator
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Func<Expression, Expression> ParseUnaryOperator(Token.Type type)
        {
            return (Expression left) =>
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
        }

        /// <summary>
        /// Parse expression
        /// </summary>
        /// <param name="priority">Operation priority</param>
        /// <returns>Expression</returns>
        private Expression ParseExpression(OperationPriority priority = OperationPriority.Minimal)
        {
            if (!PrefixParser.ContainsKey(CurrentToken.T)) return null;

            var expression = PrefixParser[CurrentToken.T].Invoke();

            if (CurrentToken != Token.Type.Semicolon && priority < Priority(PeekedToken.T))
                if (!InfixParser.ContainsKey(PeekedToken.T)) return expression;
                else return InfixParser[PeekedToken.T].Invoke(expression);

            return expression;
        }

        /// <summary>
        /// Get parse priority table value
        /// </summary>
        /// <param name="type">Token type</param>
        /// <returns>OperationPriority value</returns>
        private OperationPriority Priority(Token.Type type) => Priorities.ContainsKey(type) ? Priorities[type] : OperationPriority.Minimal;

        /// <summary>
        /// Move to a next token
        /// </summary>
        private void Next()
        {
            CurrentToken = PeekedToken;
            PeekedToken = Lexer.NextToken();
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
        public Program Parse()
        {
            Program program = new();

            while (!CurrentToken.End())
            {
                var statement = ParseStatement() ?? ParseExpressionStatement();
                if (statement != null) program.Add(statement);
                Next();
            }

            return program;
        }

        private Statement ParseExpressionStatement()
        {
            if (CurrentToken == Token.Type.Semicolon) return null;

            var statement = new ExpressionStatement(CurrentToken);
            statement.Expression = ParseExpression();

            if (PeekedToken == Token.Type.Semicolon) Next();

            return statement;
        }

        /// <summary>
        /// Parse Statement
        /// </summary>
        /// <returns>Statement</returns>
        private Statement ParseStatement() => StatementParser.ContainsKey(CurrentToken.T) ? StatementParser[CurrentToken.T]() : null;
    }
}