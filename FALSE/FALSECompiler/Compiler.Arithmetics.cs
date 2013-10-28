using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private static void Add(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Add);
        }

        private static void Negate(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Neg);
        }

        private static void Substract(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Sub);
        }

        private static void Multiplicate(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Mul);
        }

        private static void Divide(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Div);
        }
    }
}
