using System;
using System.Collections.Generic;
using ExLibris;

namespace FALSE
{
    using FullState = Tuple<IState, Token>;

    public class CommonState : IState
    {
        private static readonly Dictionary<char, OpCode> _arglessCodes = new Dictionary<char, OpCode>();

        static CommonState()
        {
            _arglessCodes.Add('.', OpCode.PrintNum);
            _arglessCodes.Add(',', OpCode.PrintChar);
            _arglessCodes.Add('^', OpCode.ReadChar);

            _arglessCodes.Add('+', OpCode.Add);
            _arglessCodes.Add('-', OpCode.Sub);
            _arglessCodes.Add('*', OpCode.Mul);
            _arglessCodes.Add('/', OpCode.Div);
            _arglessCodes.Add('_', OpCode.Neg);

            _arglessCodes.Add('=', OpCode.Eq);
            _arglessCodes.Add('>', OpCode.Gt);
            _arglessCodes.Add('&', OpCode.And);
            _arglessCodes.Add('|', OpCode.Or);
            _arglessCodes.Add('~', OpCode.Not);

            _arglessCodes.Add('$', OpCode.Dup);
            _arglessCodes.Add('%', OpCode.Drop);
            _arglessCodes.Add('\\', OpCode.Swap);
            _arglessCodes.Add('@', OpCode.Rot);
            _arglessCodes.Add('ø', OpCode.Pick);

            _arglessCodes.Add('!', OpCode.Call);
            _arglessCodes.Add('?', OpCode.If);
            _arglessCodes.Add('#', OpCode.Loop);
        }

        public FullState Process(char c)
        {
            return c.match<FullState, char>(cx => cx
                .when<char>(Char.IsDigit).then(y => new FullState(new NumState(), null))
                .when<char>(Char.IsLower).then(y => new FullState(new VariableState(), null))
                .when<char>(x => x == 0).then(y => new FullState(this, null))
                .when<char>(x => x == '\'').then(y => new FullState(States.Char, null))
                .when<char>(x => x == '(').then(y => new FullState(new CommentState(this), null))
                .when<char>(x => x == '"').then(y => new FullState(States.String, null))
                .when<char>(_arglessCodes.ContainsKey).then(y => new FullState(this, new Token(_arglessCodes[c])))
                .when<char>(x => x == '[').then(y => new FullState(this, new Token(OpCode.FuncStart)))
                .when<char>(x => x == ']').then(y => new FullState(this, new Token(OpCode.FuncEnd)))
                .when<char>(x => !x.In(' ', '\n', '\r', '\t')).then(y => new FullState(this, new Token(OpCode.Error, c.In(';', ':') ? "Variable expected" : "Unknown symbol")))
                .anyway().then(y => new FullState(this, null))
            );
        }
    }
}