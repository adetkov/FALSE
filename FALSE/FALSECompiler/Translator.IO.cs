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

        private static IEnumerable<ILCode> ILReadChar(Token arg)
        {
            yield return new ILCode(ILCode.ILType.ReadChar);
            yield return new ILCode(ILCode.ILType.PushStack);
        }

        private static IEnumerable<ILCode> ILWriteChar(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.WriteChar);
        }

        private static IEnumerable<ILCode> ILWriteLine(Token token)
        {
            yield return new ILCode(ILCode.ILType.WriteLine, token.StringArg);
        }
        private static IEnumerable<ILCode> ILWriteNumber(Token token)
        {
            yield return new ILCode(ILCode.ILType.PopStack);
            yield return new ILCode(ILCode.ILType.WriteNumber);
        }
    }
}
