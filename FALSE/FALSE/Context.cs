﻿using System.Collections.Generic;
using System.Linq;

namespace FALSE
{
    public class Context
    {
        public Context(IEnumerable<Token> program)
        {
            var res = new List<List<Token>>(1) {new List<Token>()};
            int max = 0, ptr = 0;

            ParseFunctions(program.ToArray(), ref ptr, 0, ref max, res);
            MainFunc = res[0].ToArray();

            if (res.Count > 1)
                Funcs = res.Skip(1).Select(x => x.ToArray()).ToArray();
        }

        public Token[][] Funcs { get; private set; }

        public Token[] MainFunc { get; private set; }

        private static void ParseFunctions(IList<Token> program, ref int ptr, int curFunc, ref int max,
                                           IList<List<Token>> res)
        {
            for (; ptr < program.Count; ptr++)
            {
                switch (program[ptr].Type)
                {
                    case OpCode.FuncStart:
                        res.Add(new List<Token>());
                        max++;
                        ParseFunctions(program, ref ptr, max, ref max, res);
                        res[curFunc].Add(new Token(OpCode.FuncStart) {Arg = max});
                        break;
                    case OpCode.FuncEnd:
                        return;
                    default:
                        res[curFunc].Add(program[ptr]);
                        break;
                }
            }
        }
    }
}