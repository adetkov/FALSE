using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FALSE;

namespace FALSECompiler
{
    public static partial class Translator
    {
        private readonly static Dictionary<OpCode, Func<Token, IEnumerable<ILCode>>> Map; 

        static Translator()
        {
            Map = new Dictionary<OpCode, Func<Token, IEnumerable<ILCode>>>
                {
                    { OpCode.PrintStr, ILWriteLine },
                    { OpCode.LoadConst, ILLoadNumber },
                    { OpCode.PrintNum, ILWriteNumber },
                    { OpCode.ReadChar, ILReadChar },
                };
        }

        public static IEnumerable<ILCode> Translate(IEnumerable<Token> tokens)
        {
            return tokens.SelectMany(token => Map[token.Type](token));
        }
    }
}
