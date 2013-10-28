﻿using System;
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
                    // IO
                    { OpCode.PrintStr, ILWriteLine },
                    { OpCode.LoadConst, ILLoadNumber },
                    { OpCode.PrintNum, ILWriteNumber },
                    { OpCode.PrintChar, ILWriteChar },
                    { OpCode.ReadChar, ILReadChar },

                    // Arithmetics

                    { OpCode.Neg, ILNegate },
                    { OpCode.Add, ILAdd },
                    { OpCode.Sub, ILSub },
                    { OpCode.Mul, ILMul },
                    { OpCode.Div, ILDivide },

                    { OpCode.Not, ILNot },
                    { OpCode.Eq, ILEqual },
                    { OpCode.And, ILAnd },
                    { OpCode.Or, ILOr },
                    { OpCode.Gt, ILGreater },

                    { OpCode.Dup, ILDuplicate },
                    { OpCode.Drop, ILDrop }
                };
        }

        public static IEnumerable<ILCode> Translate(IEnumerable<Token> tokens)
        {
            return tokens.SelectMany(token => Map[token.Type](token));
        }
    }
}
