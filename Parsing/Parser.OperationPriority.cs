using System.Collections.Generic;
using Tokenizer;

namespace Parsing
{
    public sealed partial class Parser
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
        /// Get parse priority table value
        /// </summary>
        /// <param name="type">Token type</param>
        /// <returns>OperationPriority value</returns>
        private OperationPriority Priority(Token.Type type) => Priorities.ContainsKey(type) ? Priorities[type] : OperationPriority.Minimal;
    }
}