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

        private void Swap(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Stloc_0, _tmpFields[g].Item1);
            g.Emit(OpCodes.Stloc_1, _tmpFields[g].Item2);
            g.Emit(OpCodes.Ldloc_0, _tmpFields[g].Item1);
            g.Emit(OpCodes.Ldloc_1, _tmpFields[g].Item2);
        }
    }
}
