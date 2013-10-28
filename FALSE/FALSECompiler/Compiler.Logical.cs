using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private static void Not(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Not);
        }

        private static void Equal(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ceq);
        }

        private static void And(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.And);
        }

        private static void Or(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Or);
        }

        private static void Greater(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Cgt);
        }
    }
}
