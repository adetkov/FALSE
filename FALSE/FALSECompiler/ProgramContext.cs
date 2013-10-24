using System.Collections.Generic;
using System.Linq;
using FALSE;

namespace FALSECompiler
{
    public class ProgramContext
    {
        public ProgramContext(IEnumerable<Token> tokens)
        {
            Main = Translator.Translate(tokens).ToArray();
        }

        public bool IsStackUsed { get { return true; } }

        public ILCode[] Main { get; private set; }

        public ILCode[][] Funcs { get; private set; }
    }
}
