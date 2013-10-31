using System.Diagnostics;
using FALSE;

namespace FALSECompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = @"[1][2 3-]";
            var tokens = Lexer.Tokenize(program);
            var c = new Compiler();
            c.Compile("Debug1", 1024, new ProgramContext(tokens));
            Process.Start("Debug1.exe");
        }
    }
}
