using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using FALSE;

namespace FALSECompiler
{
	class Program
	{
		static void Main(string[] args)
		{
		    var program = @"100 200+50-_.^";
		    var tokens = Lexer.Tokenize(program);
		    var c = new Compiler();
            c.Compile("Debug1", 1024, new ProgramContext(tokens));
		    Process.Start("Debug1.exe");
		}
	}
}
