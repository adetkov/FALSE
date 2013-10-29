using System.Reflection.Emit;

namespace FALSECompiler
{
    public partial class Compiler
    {
        private void LoadVariable(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldsfld, _variables[(int)code.Tag]);
        }

        private void StoreVariable(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Stsfld, _variables[(int)code.Tag]);
        }
    }
}
