using System;
using AST;
using Tokenizer;

namespace Parsing
{
    public sealed partial class Parser
    {
        /// <summary>
        /// Parse binary operator expression
        /// </summary>
        /// <param name="type">Type of binary operation</param>
        /// <returns>Expression with binary operator</returns>
        private Func<Expression, Expression> ParseBinaryOperatorExpression(Token.Type type)
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
    }
}