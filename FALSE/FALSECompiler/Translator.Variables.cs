using System.Collections.Generic;
using FALSE;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILLoadVariable(Token token)
        {
            yield return new ILCode(ILCode.ILType.LoadVariable, token.Arg);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILStoreVariable(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.StoreVariable, token.Arg);
        }
    }
}
