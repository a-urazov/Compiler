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

                var Access = ClassMember.Qualifier.Private;
                var Static = ClassMember.Qualifier.None;
                var Const = ClassMember.Qualifier.None;
                var Async = ClassMember.Qualifier.None;

                while (CurrentToken != Token.Type.RightBrace)
                {
                    switch (CurrentToken.T)
                    {
                        case Token.Type.EndOfFile:
                            return expression;
                        case Token.Type.Public:
                            Access = ClassMember.Qualifier.Public;
                            break;
                        case Token.Type.Private:
                            Access = ClassMember.Qualifier.Private;
                            break;
                        case Token.Type.Protected:
                            Access = ClassMember.Qualifier.Protected;
                            break;
                        case Token.Type.Static:
                            Static = ClassMember.Qualifier.Static;
                            break;
                        case Token.Type.Const:
                            Const = ClassMember.Qualifier.Const;
                            break;
                        case Token.Type.Async:
                            Async = ClassMember.Qualifier.Async;
                            break;
                        case Token.Type.Let:
                            var let = ParseStatement;
                            if (let != null)
                            {
                                expression.Fields.Add(new ClassMember(let)
                                {
                                    Access = Access,
                                    Static = Static,
                                    Const = Const,
                                    Async = Async,
                                });
                            }

                            Access = ClassMember.Qualifier.Private;
                            Static = ClassMember.Qualifier.None;
                            Const = ClassMember.Qualifier.None;
                            Async = ClassMember.Qualifier.None;
                            Next(4);
                            continue;
                        case Token.Type.Function:
                            var fn = ParseStatement;
                            if (fn != null)
                            {
                                expression.Methods.Add(new ClassMember(fn)
                                {
                                    Access = Access,
                                    Static = Static,
                                    Const = Const,
                                    Async = Async,
                                });
                            }

                            Access = ClassMember.Qualifier.Private;
                            Static = ClassMember.Qualifier.None;
                            Const = ClassMember.Qualifier.None;
                            Async = ClassMember.Qualifier.None;
                            continue;
                    }
                    Next();
                }
                return expression;
            };
        }
    }
}