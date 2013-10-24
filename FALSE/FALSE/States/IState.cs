using System;

namespace FALSE
{
    public interface IState
    {
        Tuple<IState, Token> Process(char c);
    }
}