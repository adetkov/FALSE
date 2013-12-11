using System.Collections.Generic;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILDuplicate()
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Duplicate);
            yield return new ILCode(ILCode.ILType.PushStack);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILDrop()
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Drop);
        }

        private static IEnumerable<ILCode> ILSwap()
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Swap);
            yield return new ILCode(ILCode.ILType.PushStack);
            yield return new ILCode(ILCode.ILType.PushStack);
        }
    }
}
