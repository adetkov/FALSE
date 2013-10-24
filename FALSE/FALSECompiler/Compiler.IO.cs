using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace FALSECompiler
{
    public partial class Compiler
    {
        public void LoadNumber(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldc_I4, (int)code.Tag);
        }

        public void WriteString(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldstr, (string)code.Tag);
            g.Emit(OpCodes.Call, new Action<string>(Console.WriteLine).Method);
        }

        public void WriteChar(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldstr, (string)code.Tag);
            g.Emit(OpCodes.Call, new Action<string>(Console.WriteLine).Method);
        }

        public void WriteNumber(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Call, new Action<int>(Console.WriteLine).Method);
        }

        public void ReadChar(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Call, new Func<ConsoleKeyInfo>(Console.ReadKey).Method);
            g.Emit(OpCodes.Box, typeof(ConsoleKeyInfo));
            g.Emit(OpCodes.Call, typeof(ConsoleKeyInfo).GetMethod("get_KeyChar", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public));
            g.Emit(OpCodes.Conv_I4);
        }
    }
}
