using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private static void Drop(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Pop);
        }

        private static void Duplicate(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Dup);
        }
    }
}
