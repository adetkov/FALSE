using System;
using System.Collections.Generic;
using System.Linq;

namespace FALSE
{
    public class Context
    {
        public Context(IEnumerable<Token> program)
        {
            var res = new List<List<Token>>(1) { new List<Token>() };
            int max = 0, ptr = 0;

            ParseFunctions(program.ToArray(), ref ptr, 0, ref max, res);
            MainFunc = res[0].ToArray();

            Funcs = res.Count > 1 ? res.Skip(1).Select(x => x.ToArray()).ToArray() : new Token[0][];
        }

        public Token[][] Funcs { get; private set; }

        public Token[] MainFunc { get; private set; }

        private static void ParseFunctions(IList<Token> program, ref int ptr, int curFunc, ref int max,
                                           IList<List<Token>> res)
        {
            for (; ptr < program.Count; ptr++)
            {
                var lastIndex = res[curFunc].Count - 1;
                switch (program[ptr].Type)
                {
                    case OpCode.FuncStart:
                        res.Add(new List<Token>());
                        max++;
                        ptr++;
                        ParseFunctions(program, ref ptr, max, ref max, res);
                        res[curFunc].Add(new Token(OpCode.FuncStart) { Arg = max - 1 });
                        break;

                    case OpCode.FuncEnd:
                        return;

                    case OpCode.If:
                        program[ptr].Arg = res[curFunc][lastIndex];
                        res[curFunc].RemoveAt(lastIndex);
                        res[curFunc].Add(program[ptr]);
                        break;

                    case OpCode.Loop:
                        program[ptr].Arg = new[] { res[curFunc][lastIndex - 1], res[curFunc][lastIndex] };
                        res[curFunc].RemoveAt(lastIndex);
                        res[curFunc].RemoveAt(lastIndex - 1);
                        res[curFunc].Add(program[ptr]);
                        break;

                    default:
                        res[curFunc].Add(program[ptr]);
                        break;
                }
            }
        }
    }
}