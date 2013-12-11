using System.Collections.Generic;
using FALSE;

namespace FALSECompiler
{
    public partial class Translator
    {
        private static IEnumerable<ILCode> ILLoadNumber(Token token)
        {
            yield return new ILCode(ILCode.ILType.LoadNumber, token.Arg);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILReadChar()
        {
            yield return new ILCode(ILCode.ILType.ReadChar);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILWriteChar()
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.WriteChar);
        }

        private static IEnumerable<ILCode> ILWriteLine(Token token)
        {
            yield return new ILCode(ILCode.ILType.WriteLine, token.Arg);
        }
        private static IEnumerable<ILCode> ILWriteNumber()
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.WriteNumber);
        }
    }
}
