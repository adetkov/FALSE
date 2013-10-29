using System.Collections.Generic;
using System.Linq;
using FALSE;

namespace FALSECompiler
{
    public class ProgramContext
    {
        public ProgramContext(IEnumerable<Token> tokens)
        {
            var context = new Context(tokens);
            Main = new Function(Translator.Translate(context.MainFunc).ToArray());
            Funcs = context.Funcs
                .Select(f => new Function(Translator.Translate(f).ToArray()))
                .ToArray();
        }

        public bool IsStackUsed { get { return true; } }

        public uint Variables
        {
            get { return Main.Variables | Funcs.Aggregate(0U, (a, f) => a | f.Variables); }
        }

        public Function Main { get; private set; }

        public Function[] Funcs { get; private set; }
    }
}
