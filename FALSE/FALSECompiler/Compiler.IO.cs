using System;
using System.Reflection;
using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        public static void LoadNumber(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldc_I4, (int)code.Tag);
        }

        public static void WriteString(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldstr, (string)code.Tag);
            g.Emit(OpCodes.Call, new Action<string>(Console.Write).Method);
        }

        public static void WriteChar(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldstr, (string)code.Tag);
            g.Emit(OpCodes.Call, new Action<string>(Console.Write).Method);
        }

        public static void WriteNumber(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Call, new Action<int>(Console.Write).Method);
        }

        public static void ReadChar(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Call, new Func<ConsoleKeyInfo>(Console.ReadKey).Method);
            g.Emit(OpCodes.Box, typeof(ConsoleKeyInfo));
            g.Emit(OpCodes.Call, typeof(ConsoleKeyInfo).GetMethod("get_KeyChar", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public));
        }
    }
}
