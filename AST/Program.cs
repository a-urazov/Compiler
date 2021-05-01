using System;
using System.Collections.Generic;
using System.Linq;

namespace AST
{
    /// <summary>
    /// Program as list of statements
    /// </summary>
    public sealed class Program : INode
    {
        /// <summary>
        /// List of program statements
        /// </summary>
        private IList<Statement> Statements { get; } = new List<Statement>();

        /// <summary>
        /// Add new statement to program
        /// </summary>
        /// <param name="statement">Program Statement</param>
        public void Add(Statement statement)
        {
            Statements.Add(statement);
        }

        /// <summary>
        /// Make source code from AST
        /// </summary>
        /// <returns></returns>
        public string Source => Statements.Select(x => x.Source).Aggregate((current, next) => current + next);
    }
}
