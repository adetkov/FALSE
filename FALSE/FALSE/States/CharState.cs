using System;
using ExLibris;

namespace FALSE
{
    public class CharState : IState
    {
        public Tuple<IState, Token> Process(char c)
        {
            return new Tuple<IState, Token>
            (
                States.Common,
                c.In('\r', '\n', '\t', (char)0)
                    ? new Token(OpCode.Error, "Symbol expected")
                    : new Token(OpCode.LoadChar)
            );
        }
    }
}