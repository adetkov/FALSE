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
        private static void PushFunction(ILGenerator g, ILCode code)
        {
            g.Emit(OpCodes.Ldc_I4, (int)code.Tag);
        }

        private void CallFunction(ILGenerator g, ILCode code)
        {
            var invoke = Type
                .GetType("System.Action")
                .GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);

            g.Emit(OpCodes.Stloc, _tmpFields[g]);
            g.Emit(OpCodes.Ldsfld, _funcTable);
            g.Emit(OpCodes.Ldloc, _tmpFields[g]);
            g.Emit(OpCodes.Ldelem_Ref);
            g.Emit(OpCodes.Callvirt, invoke);
        }
    }
}
