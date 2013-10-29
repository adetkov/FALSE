namespace FALSECompiler
{
    public class ILCode
    {
        public enum ILType
        {
            PopStack,
            PushStack,
            
            LoadNumber,
            WriteLine,
            WriteNumber,
            WriteChar,
            ReadChar,
            
            Negate,
            Add,
            Sub,
            Mul,
            Div,

            Not,
            Equal,
            And,
            Or,
            Greater,

            Duplicate,
            Drop,

            LoadVariable,
            StoreVariable,
        }
        
        public ILCode(ILType type, object tag = null)
        {
            Tag = tag;
            Type = type;
        }

        public ILType Type { get; private set; }

        public object Tag { get; private set; }
    }
}
