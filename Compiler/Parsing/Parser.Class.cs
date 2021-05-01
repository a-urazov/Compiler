using System;
using AST;
using Tokenizer;

namespace Parsing
{
    public sealed partial class Parser
    {
        private Func<Expression> ParseClassLiteral
        {
            get => () =>
            {
                var expression = new ClassLiteral(CurrentToken);
                if (PeekedToken == Token.Type.Colon)
                {
                    Next(2);
                    while (CurrentToken != Token.Type.LeftBrace)
                    {
                        if (CurrentToken == Token.Type.Comma)
                        {
                            Next();
                            continue;
                        }
                        if (ParseExpression() is Identifier identifier) expression.Inheritances.Add(identifier);
                        Next();
                    }
                }

                if (CurrentToken == Token.Type.LeftBrace) Next();
                else return null;

                var qualifier = (
                    Access: Qualifier.Type.Private,
                    Static: Qualifier.Type.None,
                    Async: Qualifier.Type.None
                );

                while (CurrentToken != Token.Type.RightBrace)
                {
                    switch (CurrentToken.T)
                    {
                        case Token.Type.EndOfFile:
                            return expression;
                        case Token.Type.Public:
                            qualifier.Access = Qualifier.Type.Public;
                            break;
                        case Token.Type.Private:
                            qualifier.Access = Qualifier.Type.Private;
                            break;
                        case Token.Type.Protected:
                            qualifier.Access = Qualifier.Type.Protected;
                            break;
                        case Token.Type.Static:
                            qualifier.Static = Qualifier.Type.Static;
                            break;
                        case Token.Type.Async:
                            qualifier.Async = Qualifier.Type.Async;
                            break;
                        case Token.Type.Let:
                            var let = ParseStatement;
                            if (let != null)
                            {
                                expression.Fields.Add(new ClassMember(let)
                                {
                                    Access = qualifier.Access,
                                    Static = qualifier.Static,
                                    Async = qualifier.Async,
                                });
                            }
                            qualifier = Qualifier.Reset;
                            Next(4);
                            continue;
                        case Token.Type.Function:
                            var fn = ParseStatement;
                            if (fn != null)
                            {
                                expression.Methods.Add(new ClassMember(fn)
                                {
                                    Access = qualifier.Access,
                                    Static = qualifier.Static,
                                    Async = qualifier.Async,
                                });
                            }
                            qualifier = Qualifier.Reset;
                            continue;
                    }
                    Next();
                }
                return expression;
            };
        }
    }
}