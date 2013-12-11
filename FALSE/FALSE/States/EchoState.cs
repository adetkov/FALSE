using System;
using System.Collections.Generic;

namespace FALSE
{
    public class EchoState : IState
    {
        private static readonly HashSet<char> AllowedSymbols = new HashSet<char>(new[] {'"', '\\', 'n', 'r', 't'});

        public Tuple<IState, Token> Process(char c)
        {
            return AllowedSymbols.Contains(c)
                       ? new Tuple<IState, Token>(States.String, null)
                       : new Tuple<IState, Token>(States.String, new Token(OpCode.Error, String.Format("\\{0} is unknown symbol", c)));
        }
    }
}
