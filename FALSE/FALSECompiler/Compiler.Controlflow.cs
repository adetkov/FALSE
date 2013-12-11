using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private static void Label(ILGenerator g, ILCode code)
        {
            g.MarkLabel(((ILLabel)code.Tag).GetLabel(g));
            g.Emit(OpCodes.Nop);
        }

        private static void JumpIfFalse(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Brfalse, ((ILLabel)code.Tag).GetLabel(g));
        }

        private static void Jump(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Br, ((ILLabel)code.Tag).GetLabel(g));
        }
    }
}
