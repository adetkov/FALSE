using System;
using System.Collections.Generic;

namespace FALSE
{
	public class VM
	{
	    private readonly IConsole _console;
	    private readonly Dictionary<OpCode, Action<Context, Token, Stack>> _funcTable = new Dictionary<OpCode, Action<Context, Token, Stack>>();

        private readonly Variable[] _vars = new Variable[26];

        public VM(IConsole console)
        {
            _console = console;
            _funcTable.Add(OpCode.Comment, (cx, c, s) => { });

            _funcTable.Add(OpCode.LoadConst, (cx, c, s) => s.Push(c.Arg));
            _funcTable.Add(OpCode.LoadChar, (cx, c, s) => s.Push(c.Arg, Stack.Type.Char));

            _funcTable.Add(OpCode.PrintNum, (cx, c, s) => console.Write(s.Pop()));
            _funcTable.Add(OpCode.PrintStr, (cx, c, s) => console.Write(c.StringArg));
            _funcTable.Add(OpCode.PrintChar, (cx, c, s) => console.Write((char)s.Pop()));
            _funcTable.Add(OpCode.ReadChar, (cx, c, s) => s.Push(console.Read()));

            _funcTable.Add(OpCode.Add, (cx, c, s) => s.Push(s.Pop() + s.Pop()));
            _funcTable.Add(OpCode.Sub, (cx, c, s) => s.Push(s.Pop() - s.Pop()));
            _funcTable.Add(OpCode.Mul, (cx, c, s) => s.Push(s.Pop() * s.Pop()));
            _funcTable.Add(OpCode.Div, (cx, c, s) => s.Push(s.Pop() / s.Pop()));
            _funcTable.Add(OpCode.Neg, (cx, c, s) => s.Push(-s.Pop()));

            _funcTable.Add(OpCode.Eq, (cx, c, s) => s.Push(s.Pop() == s.Pop() ? -1 : 0));
            _funcTable.Add(OpCode.Gt, (cx, c, s) => s.Push(s.Pop() > s.Pop() ? -1 : 0));
            _funcTable.Add(OpCode.And, (cx, c, s) => s.Push(s.Pop() & s.Pop()));
            _funcTable.Add(OpCode.Or, (cx, c, s) => s.Push(s.Pop() | s.Pop()));
            _funcTable.Add(OpCode.Inv, (cx, c, s) => s.Push(s.Pop() ^ (-1)));

            _funcTable.Add(OpCode.Dup, (cx, c, s) => s.Dup());
            _funcTable.Add(OpCode.Drop, (cx, c, s) => s.Pop());
            _funcTable.Add(OpCode.Swap, (cx, c, s) => s.Swap());
            _funcTable.Add(OpCode.Rot, (cx, c, s) => s.Rot());
            _funcTable.Add(OpCode.Pick, (cx, c, s) => s.Dup(s.Pop()));

            _funcTable.Add(OpCode.SaveVar, (cx, c, s) => _vars[c.Arg].Value = s.PopRegister());
            _funcTable.Add(OpCode.LoadVar, (cx, c, s) => s.PushRegister(_vars[c.Arg].Value));

            _funcTable.Add(OpCode.Call, (cx, c, s) => Run(cx, cx.Funcs[c.Arg], s));
            _funcTable.Add(OpCode.If, (cx, c, s) =>
            {
                int func = s.Pop();
                if (s.Pop() != 0)
                    Run(cx, cx.Funcs[func], s);
            });

            _funcTable.Add(OpCode.Loop, (cx, c, s) =>
            {
                int body = s.Pop();
                int cond = s.Pop();

                while (true)
                {
                    Run(cx, cx.Funcs[cond], s);
                    if (s.Pop() == 0) break;
                    Run(cx, cx.Funcs[body], s);
                }
            });
        }

        public void Run(Context context, Token[] program, Stack stack)
        {
            foreach (var code in program)
                _funcTable[code.Type](context, code, stack);
        }

        public void Run(Context context)
        {
            Run(context, context.MainFunc, new Stack());
        }

        private struct Variable
        {
            private Register _value;

            public bool IsModified { get; private set; }

            public Register Value
            {
                get { return _value; }
                set
                {
                    _value = value;
                    IsModified = true;
                }
            }
        }
    }
}
