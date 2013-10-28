using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExLibris;

namespace FALSE
{
    public class EchoState : IState
    {
        public Tuple<IState, Token> Process(char c)
        {
            return c.In('"', '\\', 'n', 't')
                       ? new Tuple<IState, Token>(States.String, null)
                       : new Tuple<IState, Token>(States.String, new Token(OpCode.Error, String.Format("\\{0} is unknown symbol", c)));
        }
    }
}
