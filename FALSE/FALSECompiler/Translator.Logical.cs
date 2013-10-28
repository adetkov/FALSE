using System.Collections.Generic;
using FALSE;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILAnd(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.And);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILEqual(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Equal);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILNot(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Negate);
            yield return new ILCode(ILCode.ILType.Not);
            yield return new ILCode(ILCode.ILType.Negate);
            yield return new ILCode(ILCode.ILType.PushStack);
        }
        private static IEnumerable<ILCode> ILOr(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Or);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILGreater(Token arg)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.Greater);
            yield return new ILCode(ILCode.ILType.PushStack);    
        }
    }
}