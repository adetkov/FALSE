using System;

namespace FALSE
{
	public class VariableState : IState
	{
        public Tuple<IState, Token> Process(char c)
		{
            switch (c)
            {
                case ':': return new Tuple<IState, Token>(States.Common, new Token(OpCode.SaveVar));
                case ';': return new Tuple<IState, Token>(States.Common, new Token(OpCode.LoadVar));
                case (char)0: return new Tuple<IState, Token>(States.Common, new Token(OpCode.Error, ": or ; expected"));
                default: return new Tuple<IState, Token>(States.Common, new Token(OpCode.Error, ": or ; expected"));
            }
		}
	}
}
