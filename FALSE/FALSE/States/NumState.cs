using System;

namespace FALSE
{
    public class NumState : IState
    {
        public Tuple<IState, Token> Process(char c)
        {
            return Char.IsDigit(c)
                       ? new Tuple<IState, Token>(this, null)
                       : new Tuple<IState, Token>(States.Common, new Token(OpCode.LoadConst));
        }

    }
}