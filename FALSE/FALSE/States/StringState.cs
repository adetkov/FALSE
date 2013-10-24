using System;

namespace FALSE
{
    public class StringState : IState
    {
        public Tuple<IState, Token> Process(char c)
        {
            switch (c)
            {
                case '"': return new Tuple<IState, Token>(States.Common, new Token(OpCode.PrintStr));
                case '\\' : return new Tuple<IState, Token>(new EchoState(), null);
                case (char) 0: return new Tuple<IState, Token>(States.Common, new Token(OpCode.Error, "\" expected"));
                default: return new Tuple<IState, Token>(this, null);
            }
        }
    }
}