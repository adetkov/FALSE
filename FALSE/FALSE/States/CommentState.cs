using System;
namespace FALSE
{
    public class CommentState : IState
    {
        private readonly IState _parentState;

        public CommentState(IState parentState)
        {
            _parentState = parentState;
        }

        public Tuple<IState, Token> Process(char c)
        {
            switch (c)
            {
                case (char)0: return new Tuple<IState, Token>(this, new Token(OpCode.Error, ") expected"));
                case '(': return new Tuple<IState, Token>(new CommentState(this), null);
                case ')': return new Tuple<IState, Token>(_parentState, _parentState is CommentState ? null : new Token(OpCode.Comment));
                default : return new Tuple<IState, Token>(this, null);
            }
        }
    }
}