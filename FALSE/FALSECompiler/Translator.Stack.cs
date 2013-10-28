using System.Collections.Generic;
using FALSE;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILDuplicate(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Duplicate);
            yield return new ILCode(ILCode.ILType.PushStack);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILDrop(Token arg)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Drop);
        }
    }
}
