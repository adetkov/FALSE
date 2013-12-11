using System;
using System.Collections.Generic;

namespace FALSE
{
    using FullState = Tuple<IState, Token>;

    public class CommonState : IState
    {
        private static readonly Dictionary<char, OpCode> ArglessCodes = new Dictionary<char, OpCode>();
        private static readonly HashSet<char> WrongSymbols = new HashSet<char>(new[] {' ', '\n', '\r', '\t'}); 


        static CommonState()
        {
            ArglessCodes.Add('.', OpCode.PrintNum);
            ArglessCodes.Add(',', OpCode.PrintChar);
            ArglessCodes.Add('^', OpCode.ReadChar);

            ArglessCodes.Add('+', OpCode.Add);
            ArglessCodes.Add('-', OpCode.Sub);
            ArglessCodes.Add('*', OpCode.Mul);
            ArglessCodes.Add('/', OpCode.Div);
            ArglessCodes.Add('_', OpCode.Neg);

            ArglessCodes.Add('=', OpCode.Eq);
            ArglessCodes.Add('>', OpCode.Gt);
            ArglessCodes.Add('&', OpCode.And);
            ArglessCodes.Add('|', OpCode.Or);
            ArglessCodes.Add('~', OpCode.Not);

            ArglessCodes.Add('$', OpCode.Dup);
            ArglessCodes.Add('%', OpCode.Drop);
            ArglessCodes.Add('\\', OpCode.Swap);
            ArglessCodes.Add('@', OpCode.Rot);
            ArglessCodes.Add('ø', OpCode.Pick);

            ArglessCodes.Add('!', OpCode.Call);
            ArglessCodes.Add('?', OpCode.If);
            ArglessCodes.Add('#', OpCode.Loop);
        }

        public FullState Process(char c)
        {
            if (Char.IsDigit(c))
                return new FullState(new NumState(), null);

            if (Char.IsLower(c))
                return new FullState(new VariableState(), null);

            if (ArglessCodes.ContainsKey(c))
                return new FullState(this, new Token(ArglessCodes[c]));

            switch (c)
            {
                case (char)0: return new FullState(this, null);
                case '\'': return new FullState(States.Char, null);
                case '(': return new FullState(new CommentState(this), null);
                case '"': return new FullState(States.String, null);
                case '[': return new FullState(this, new Token(OpCode.FuncStart));
                case ']': return new FullState(this, new Token(OpCode.FuncEnd));
            }

            if (!WrongSymbols.Contains(c))
            {
                return new FullState(this, new Token(OpCode.Error, c==';' || c == ':'
                                         ? "Variable expected"
                                         : "Unknown symbol"));
            }
            return new FullState(this, null);
        }
    }
}