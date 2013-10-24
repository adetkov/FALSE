using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
