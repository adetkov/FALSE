using System.Collections.Generic;
using FALSE;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILCallFunction()
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.CallFunction);
        }

        private static IEnumerable<ILCode> ILPushFunction(Token arg)
        {
            yield return new ILCode(ILCode.ILType.PushFunction, arg.Arg);
            yield return new ILCode(ILCode.ILType.PushStack);
        }
    }
}
