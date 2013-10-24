using FALSE;

namespace FALIDE
{
    public enum TokenType
    {
        Char,
        Number,
        String,
        Comment,
        Error,
        LoadVariable,
        StoreVariable,
        ControlFlow,
        StackOperators,
        Operator,
        IO
    }

    public static class TokenTypeExtension
    {
        public static TokenType GetTokenType(this OpCode code)
        {
            switch (code)
            {
                case OpCode.LoadConst: return TokenType.Number;
                case OpCode.LoadChar: return TokenType.Char; 
                case OpCode.PrintStr: return TokenType.String;
                case OpCode.Comment: return TokenType.Comment;
                case OpCode.SaveVar: return TokenType.StoreVariable;
                case OpCode.LoadVar: return TokenType.LoadVariable;
                case OpCode.If:
                case OpCode.Loop:
                case OpCode.Call:
                    return TokenType.ControlFlow;
                case OpCode.Dup:
                case OpCode.Drop:
                case OpCode.Pick:
                case OpCode.Rot:
                case OpCode.Swap:
                    return TokenType.StackOperators;
                case OpCode.Error:
                    return TokenType.Error;
                default: return TokenType.Operator;
            }
        }
    }
}
