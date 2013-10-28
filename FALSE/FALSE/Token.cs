namespace FALSE
{
    public enum OpCode
    {
        LoadConst,
        LoadChar,
        Comment,
        PrintStr,
        PrintNum,
        PrintChar,
        ReadChar,
        Add,
        Sub,
        Mul,
        Div,
        Neg,
        Eq,
        Not,
        And,
        Or,
        Gt,
        Dup,
        Drop,
        Swap, // TODO: Implement
        Rot, // TODO: Implement
        Pick, // TODO: Implement
        LoadVar,
        SaveVar,
        FuncStart,
        FuncEnd,
        Call,
        If,
        Loop,
        Error,
    }

    public class Token
    {
        public Token(OpCode code)
        {
            Type = code;
        }

        public Token(OpCode code, string arg)
            : this(code)
        {
            StringArg = arg;
        }

        public int Position { get; set; }

        public string StringArg
        {
            get; set;
        }

        public OpCode Type
        {
            get;
            private set;
        }

        public int Arg { get; set; }
    }
}
