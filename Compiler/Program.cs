using System;
using Parsing;

namespace Compiler
{
    /// <summary>
    /// Entry point of comp
    /// </summary>
    internal sealed class Program
    {
        private static void Main(string[] _) => Console.WriteLine(new Parser(new Lexer("let A = class : B, C { let a = fn (a, b) => a + b + 1; }; fn func_a(a, b, c) { return a + b + c * 4; }")).Parse().Source());
    }
}
