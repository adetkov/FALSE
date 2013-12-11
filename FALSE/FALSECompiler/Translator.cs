using System;
using System.Collections.Generic;
using System.Linq;
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
                    // IO
                    { OpCode.PrintStr, ILWriteLine },
                    { OpCode.LoadConst, ILLoadNumber },
                    { OpCode.LoadChar, ILLoadNumber },
                    { OpCode.PrintNum, t => ILWriteNumber() },
                    { OpCode.PrintChar, t => ILWriteChar() },
                    { OpCode.ReadChar, t => ILReadChar() },

                    // Arithmetics
                    { OpCode.Neg, t => ILNegate() },
                    { OpCode.Add, t => ILAdd() },
                    { OpCode.Sub, t => ILSub() },
                    { OpCode.Mul, t => ILMul() },
                    { OpCode.Div, t => ILDivide() },

                    // Logical
                    { OpCode.Not, t => ILNot() },
                    { OpCode.Eq, t => ILEqual() },
                    { OpCode.And, t => ILAnd() },
                    { OpCode.Or, t => ILOr() },
                    { OpCode.Gt, t => ILGreater() },

                    // Stack
                    { OpCode.Dup, t => ILDuplicate() },
                    { OpCode.Drop, t => ILDrop() },
                    { OpCode.Swap, t => ILSwap() },

                    //Variables
                    { OpCode.LoadVar, ILLoadVariable },
                    { OpCode.SaveVar, ILStoreVariable },

                    // Functions
                    { OpCode.FuncStart, ILPushFunction },
                    { OpCode.Call, t => ILCallFunction() },

                    // Control flow
                    { OpCode.If, t => ILIf((Token)t.Arg) },
                    { OpCode.Loop, t => ILWhile((Token[])t.Arg) },

                    // Comments
                    { OpCode.Comment, t => Enumerable.Empty<ILCode>()}
                };
        }

        public static IEnumerable<ILCode> Translate(IEnumerable<Token> tokens)
        {
            return tokens.SelectMany(TranslateToken);
        }

        private static IEnumerable<ILCode> TranslateToken(Token token)
        {
            return Map[token.Type](token);
        }
    }
}