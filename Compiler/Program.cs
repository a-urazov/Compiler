using System;
using Parsing;

namespace Compiler
{
    /// <summary>
    /// Entry point of comp
    /// </summary>
    internal sealed class Program
    {
        private static void Main(string[] _) => Console.WriteLine(new Parser(new Lexer("let A = class : B, C { public static let a = fn (a, b) => a + b + 1; };")).Parse.Source);
    }
}
