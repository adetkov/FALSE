using System;
using System.Collections.Generic;

namespace FALSE
{
    public class CharState : IState
    {
        private static readonly HashSet<char> WrongSymbols = new HashSet<char>(new[] {'\r', '\n', '\t', (char) 0}); 

        public Tuple<IState, Token> Process(char c)
        {
            return new Tuple<IState, Token>
            (
                States.Common,
                WrongSymbols.Contains(c)
                    ? new Token(OpCode.Error, "Symbol expected")
                    : new Token(OpCode.LoadChar)
            );
        }
    }
}